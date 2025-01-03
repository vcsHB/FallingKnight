namespace Map.MapManager
{
    using UnityEngine;
    using Map.MapData;
    using Map.MapData.ScriptableObject;
    using System.Collections.Generic;
    using System.Collections;
    using Unity.Cinemachine;
    using Obstacles;
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

        [SerializeField] private const float offset = 1.0f;
        public const int    totalMapTileNumber = 20;
        public MapData      currentMapData;

        /// <summary>
        /// ��Ÿ�� ť�� ���� ����Ʈ
        /// </summary>
        private List<Queue<GameObject>> poolingQueueList = new List<Queue<GameObject>>();
        private List<List<GameObject>>  poolingObstacleList = new List<List<GameObject>>();

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
                poolingObstacleList.Add(new List<GameObject>());
            }

            ChangeStage();
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
            int nextMapTileNumber = 0;
            int mapKind = (int)currentMapData.mapKind; // enum���� ���� ���� ���� ã��

            float cameraBottomPosY = followCam.transform.position.y - followCam.Lens.OrthographicSize;

            #region ť�� ������Ʈ �Ҵ� �� ��ũ�Ѹ� ������ ���� ó�� Ÿ�� ����
            if (poolingQueueList[mapKind].Count < currentMapData.mapTileArray.Length)
            {
                poolingQueueList[mapKind].Clear();
                for (int i = 0; i < currentMapData.mapTileArray.Length; i++)
                {
                    GameObject newMapTile = Instantiate(currentMapData.mapTileArray[i], new Vector2(0f, cameraBottomPosY - 10.0f), Quaternion.identity);
                    poolingQueueList[mapKind].Enqueue(newMapTile);

                    newMapTile.GetComponent<MapTileNonactivation>().followCam = followCam;
                    newMapTile.SetActive(false);
                }
            }
            GameObject startMapTile = poolingQueueList[mapKind].Dequeue();
            startMapTile.transform.position = new Vector2(0f, cameraBottomPosY - 10.0f);
            // nextMapTile Ȱ��ȭ
            startMapTile.SetActive(true);
            poolingQueueList[mapKind].Enqueue(startMapTile);

            nextMapTileNumber++;
            #endregion

            #region Ǯ���� ���� ��ֹ� ����
            if (poolingObstacleList[mapKind].Count == 0)
            {
                for(int i = 0; i < currentMapData.ObstacleArray.Length; i++)
                {
                    GameObject obstacle = Instantiate(currentMapData.ObstacleArray[i], new Vector2(0, cameraBottomPosY - offset), Quaternion.identity);
                    poolingObstacleList[mapKind].Add(obstacle);
                    obstacle.SetActive(false);
                }
            }
            #endregion

            Transform lastMapTile = startMapTile.transform; // ���������� ������ �� Ÿ���� Ʈ������
            while (totalMapTileNumber > nextMapTileNumber)
            {
                cameraBottomPosY = followCam.transform.position.y - followCam.Lens.OrthographicSize; // ȭ���� �Ʒ� �κ��� y��ǥ
                if (cameraBottomPosY - offset < lastMapTile.position.y) // lastTileMap�� y���� ȭ���� �Ʒ� �κ� - offset ���� Ŀ���� ���� ��Ÿ�� ����
                {
                    GameObject nextMapTile = poolingQueueList[mapKind].Dequeue(); // poolingQueueList[mapKind]���� Dequeue�� ������Ʈ�� mapTile�� �Ҵ�
                    if (!nextMapTile.activeSelf) // nextMapTile�� Ȱ��ȭ�Ǿ� ���� ���� ���, 
                    {
                        // nextMapTile�� ���� ������ Ÿ�Ͽ� �� �ٰ� ��ġ ����
                        nextMapTile.transform.position = new Vector2(0, lastMapTile.position.y - lastMapTile.GetComponent<SpriteRenderer>().bounds.size.y);
                        // nextMapTile Ȱ��ȭ
                        nextMapTile.SetActive(true);
                        // lastMapTile�� nextMapTile.transform���� �����ؼ� ������ ������ ��Ÿ�� �ùٸ� ��ġ�� ������ �� �ֵ��� ��.
                        lastMapTile = nextMapTile.transform;
                    }
                    else // nextMapTile�� Ȱ��ȭ�Ǿ� ���� ���
                    {
                        // ���������� �� ��Ÿ�� ���� ��, ��ġ
                        GameObject prefab = Instantiate(currentMapData.mapTileArray[nextMapTileNumber % currentMapData.mapTileArray.Length],
                                                                new Vector2(0f, lastMapTile.position.y - lastMapTile.GetComponent<SpriteRenderer>().bounds.size.y), 
                                                                Quaternion.identity);

                        prefab.GetComponent<MapTileNonactivation>().followCam = followCam;

                        // lastMapTile�� prefab.transform���� �����ؼ� ������ ������ ��Ÿ�� �ùٸ� ��ġ�� ������ �� �ֵ��� ��.
                        lastMapTile = prefab.transform;
                    }

                    poolingQueueList[mapKind].Enqueue(nextMapTile); // nextMapTile�� poolingQueueList[mapKind]�� Enqueue�ϸ鼭 Ǯ���� �ǵ��� ��.

                    if(nextMapTileNumber != 0 && nextMapTileNumber % 2 == 0) // �� ���� �� ���� Obstacle�� ����
                    {
                        GameObject obstacle = poolingObstacleList[mapKind][Random.Range(0, poolingObstacleList[mapKind].Count)];
                        obstacle.transform.position = new Vector2(obstacle.GetComponent<Obstacle>().GetSpawnPosX(), cameraBottomPosY - offset);
                        obstacle.SetActive(true);
                    }

                    nextMapTileNumber++; // �� ��Ÿ���� ������ ��츦 ���ؼ� ������ �� ��Ÿ���� �� ��° ��Ÿ������ �����.
                }

                yield return null;
            }

            while(nextMapTileNumber != 0 && nextMapTileNumber < currentMapData.mapTileArray.Length) // ���� �� ���������� �� ������ �� ó������ Ǯ���� �ϱ� ����.
            {
                poolingQueueList[mapKind].Enqueue(poolingQueueList[mapKind].Dequeue());
                nextMapTileNumber++;
            }

            for (int i = 0; i < poolingObstacleList[mapKind].Count; i++)
            {
                poolingObstacleList[mapKind][i].SetActive(false); // ���������� ������ ��� ��ֹ� ��Ȱ��ȭ
            }

            ChangeStage();
        }
    }
}