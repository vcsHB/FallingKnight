namespace Map.MapManager
{
    using UnityEngine;
    using Map.MapData;
    using Map.MapData.ScriptableObject;
    using System.Collections.Generic;
    using System.Collections;
    using Unity.Cinemachine;
    using UIManage.InGame;

    public class MapManager : MonoBehaviour
    {
        public static MapManager Instance;

        [Header("스크립터블 오브젝트")]
        [SerializeField] private MapDataScriptableObjectScript mapDatas;

        [Header("카메라")]
        [SerializeField] private CinemachineCamera followCam;

        [Header("StageChangePanel")]
        [SerializeField] private StageChangePanel stageChangePanel;

        private const float offset = 10.0f;
        public const int    totalMapTileNumber = 10;
        public MapData      currentMapData;

        [SerializeField] private Transform lastMapTile = null;

        private Transform player;

        /// <summary>
        /// 맵타일 큐를 담은 리스트
        /// </summary>
        private List<Queue<GameObject>> poolingQueueList = new List<Queue<GameObject>>();

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            for (int i = 0; i < mapDatas.mapDataArray.Length; i++)
            {
                poolingQueueList.Add(new Queue<GameObject>());
            }
            
            player = followCam.Target.TrackingTarget;

            ChangeStage();

            // player.position = new Vector2(player.position.x, player.position.y - followCam.Lens.OrthographicSize * 2.0f);
        }

        void ChangeStage()
        {
            int currentMapId = 0;

            do
            {
                currentMapId = Random.Range(0, mapDatas.mapDataArray.Length);
            } while (currentMapData == mapDatas.mapDataArray[currentMapId]);
            
            currentMapData = mapDatas.mapDataArray[currentMapId];

            stageChangePanel.Open();

            StartCoroutine(CoSpawnMap());
        }

        IEnumerator CoSpawnMap()
        {
            yield return new WaitForSeconds(0.5f);

            int nextMapTileNumber = 0;
            int mapKind = (int)currentMapData.mapKind; // enum으로 현재 맵의 종류 찾기

            float cameraBottomPosY = followCam.transform.position.y - followCam.Lens.OrthographicSize;

            // 큐에 오브젝트 할당 및 스크롤링 시작을 위한 처음 타일 생성
            if (poolingQueueList[mapKind].Count < currentMapData.mapTileArray.Length)
            {
                poolingQueueList[mapKind].Clear();
                for (int i = 0; i < currentMapData.mapTileArray.Length; i++)
                {
                    GameObject newMapTile = Instantiate(currentMapData.mapTileArray[i], new Vector2(0f, cameraBottomPosY - 10.0f), Quaternion.identity);
                    poolingQueueList[mapKind].Enqueue(newMapTile);

                    newMapTile.GetComponent<MapTile>().followCam = followCam;
                    newMapTile.SetActive(false);
                }
            }

            if (lastMapTile == null)
            {
                GameObject startMapTile = poolingQueueList[mapKind].Dequeue();
                startMapTile.transform.position = new Vector2(0f, cameraBottomPosY - 10.0f);
                // nextMapTile 활성화
                startMapTile.GetComponent<MapTile>().SetActiveTile(true);
                poolingQueueList[mapKind].Enqueue(startMapTile);

                nextMapTileNumber++;

                lastMapTile = startMapTile.transform; // 마지막으로 생성된 맵 타일의 트랜스폼

                player.position = new Vector2(player.position.x, lastMapTile.position.y - offset);
            }

            
            player.position = new Vector2(player.position.x, lastMapTile.position.y - lastMapTile.GetComponent<SpriteRenderer>().bounds.size.y - offset);

            while (totalMapTileNumber > nextMapTileNumber)
            {
                cameraBottomPosY = followCam.transform.position.y - followCam.Lens.OrthographicSize; // 화면의 아래 부분의 y좌표
                if (cameraBottomPosY + offset < lastMapTile.position.y) // lastTileMap의 y값이 화면의 아래 부분 + offset 보다 커지면 다음 맵타일 생성
                {
                    GameObject nextMapTile = poolingQueueList[mapKind].Dequeue(); // poolingQueueList[mapKind]에서 Dequeue한 오브젝트를 mapTile에 할당
                    if (!nextMapTile.activeSelf) // nextMapTile이 활성화되어 있지 않을 경우, 
                    {
                        // nextMapTile을 전에 생성된 타일에 딱 붙게 위치 변경
                        nextMapTile.transform.position = new Vector2(0, lastMapTile.position.y - lastMapTile.GetComponent<SpriteRenderer>().bounds.size.y);
                        // nextMapTile 활성화
                        nextMapTile.GetComponent<MapTile>().SetActiveTile(true);
                        //nextMapTile.SetActive(true);
                        // lastMapTile을 nextMapTile.transform으로 변경해서 다음에 생성될 맵타일 올바른 위치에 생성될 수 있도록 함.
                        lastMapTile = nextMapTile.transform;
                    }
                    else // nextMapTile이 활성화되어 있을 경우
                    {
                        // 프리팹으로 새 맵타일 생성 후, 배치
                        GameObject prefab = Instantiate(currentMapData.mapTileArray[nextMapTileNumber % currentMapData.mapTileArray.Length],
                                                                new Vector2(0f, lastMapTile.position.y - lastMapTile.GetComponent<SpriteRenderer>().bounds.size.y), 
                                                                Quaternion.identity);

                        prefab.GetComponent<MapTile>().followCam = followCam;
                        prefab.GetComponent<MapTile>().SetActiveTile(true);

                        // lastMapTile을 prefab.transform으로 변경해서 다음에 생성될 맵타일 올바른 위치에 생성될 수 있도록 함.
                        lastMapTile = prefab.transform;
                    }

                    poolingQueueList[mapKind].Enqueue(nextMapTile); // nextMapTile을 poolingQueueList[mapKind]에 Enqueue하면서 풀링이 되도록 함.

                    nextMapTileNumber++; // 새 맵타일을 생성할 경우를 위해서 다음에 올 맵타일이 몇 번째 맵타일인지 계산함.
                }

                yield return null;
            }

            while (nextMapTileNumber != 0 && nextMapTileNumber < currentMapData.mapTileArray.Length) // 다음 이 스테이지가 또 나왔을 때 처음부터 풀링을 하기 위함.
            {
                poolingQueueList[mapKind].Enqueue(poolingQueueList[mapKind].Dequeue());
                nextMapTileNumber++;
            }

            ChangeStage();
        }
    }
}