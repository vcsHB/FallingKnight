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

        [Header("��ũ���ͺ� ������Ʈ")]
        [SerializeField] private MapDataScriptableObjectScript mapDatas;

        [Header("ī�޶�")]
        [SerializeField] private CinemachineCamera followCam;

        [Header("StageChangePanel")]
        [SerializeField] private StageChangePanel stageChangePanel;

        private const float offset = 10.0f;
        public const int    totalMapTileNumber = 10;
        public MapData      currentMapData;

        [SerializeField] private Transform lastMapTile = null;

        private Transform player;

        /// <summary>
        /// ��Ÿ�� ť�� ���� ����Ʈ
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
            int mapKind = (int)currentMapData.mapKind; // enum���� ���� ���� ���� ã��

            float cameraBottomPosY = followCam.transform.position.y - followCam.Lens.OrthographicSize;

            // ť�� ������Ʈ �Ҵ� �� ��ũ�Ѹ� ������ ���� ó�� Ÿ�� ����
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
                // nextMapTile Ȱ��ȭ
                startMapTile.GetComponent<MapTile>().SetActiveTile(true);
                poolingQueueList[mapKind].Enqueue(startMapTile);

                nextMapTileNumber++;

                lastMapTile = startMapTile.transform; // ���������� ������ �� Ÿ���� Ʈ������

                player.position = new Vector2(player.position.x, lastMapTile.position.y - offset);
            }

            
            player.position = new Vector2(player.position.x, lastMapTile.position.y - lastMapTile.GetComponent<SpriteRenderer>().bounds.size.y - offset);

            while (totalMapTileNumber > nextMapTileNumber)
            {
                cameraBottomPosY = followCam.transform.position.y - followCam.Lens.OrthographicSize; // ȭ���� �Ʒ� �κ��� y��ǥ
                if (cameraBottomPosY + offset < lastMapTile.position.y) // lastTileMap�� y���� ȭ���� �Ʒ� �κ� + offset ���� Ŀ���� ���� ��Ÿ�� ����
                {
                    GameObject nextMapTile = poolingQueueList[mapKind].Dequeue(); // poolingQueueList[mapKind]���� Dequeue�� ������Ʈ�� mapTile�� �Ҵ�
                    if (!nextMapTile.activeSelf) // nextMapTile�� Ȱ��ȭ�Ǿ� ���� ���� ���, 
                    {
                        // nextMapTile�� ���� ������ Ÿ�Ͽ� �� �ٰ� ��ġ ����
                        nextMapTile.transform.position = new Vector2(0, lastMapTile.position.y - lastMapTile.GetComponent<SpriteRenderer>().bounds.size.y);
                        // nextMapTile Ȱ��ȭ
                        nextMapTile.GetComponent<MapTile>().SetActiveTile(true);
                        //nextMapTile.SetActive(true);
                        // lastMapTile�� nextMapTile.transform���� �����ؼ� ������ ������ ��Ÿ�� �ùٸ� ��ġ�� ������ �� �ֵ��� ��.
                        lastMapTile = nextMapTile.transform;
                    }
                    else // nextMapTile�� Ȱ��ȭ�Ǿ� ���� ���
                    {
                        // ���������� �� ��Ÿ�� ���� ��, ��ġ
                        GameObject prefab = Instantiate(currentMapData.mapTileArray[nextMapTileNumber % currentMapData.mapTileArray.Length],
                                                                new Vector2(0f, lastMapTile.position.y - lastMapTile.GetComponent<SpriteRenderer>().bounds.size.y), 
                                                                Quaternion.identity);

                        prefab.GetComponent<MapTile>().followCam = followCam;
                        prefab.GetComponent<MapTile>().SetActiveTile(true);

                        // lastMapTile�� prefab.transform���� �����ؼ� ������ ������ ��Ÿ�� �ùٸ� ��ġ�� ������ �� �ֵ��� ��.
                        lastMapTile = prefab.transform;
                    }

                    poolingQueueList[mapKind].Enqueue(nextMapTile); // nextMapTile�� poolingQueueList[mapKind]�� Enqueue�ϸ鼭 Ǯ���� �ǵ��� ��.

                    nextMapTileNumber++; // �� ��Ÿ���� ������ ��츦 ���ؼ� ������ �� ��Ÿ���� �� ��° ��Ÿ������ �����.
                }

                yield return null;
            }

            while (nextMapTileNumber != 0 && nextMapTileNumber < currentMapData.mapTileArray.Length) // ���� �� ���������� �� ������ �� ó������ Ǯ���� �ϱ� ����.
            {
                poolingQueueList[mapKind].Enqueue(poolingQueueList[mapKind].Dequeue());
                nextMapTileNumber++;
            }

            ChangeStage();
        }
    }
}