namespace Map.MapManager
{
    using UnityEngine;
    using Map.MapData;
    using Map.MapData.ScriptableObject;
    using System.Collections.Generic;
    using System.Collections;
    using Unity.Cinemachine;

    public class MapManager : MonoBehaviour
    {
        public static MapManager Instance;

        [Header("��ũ���ͺ� ������Ʈ")]
        [SerializeField] private MapDataScriptableObjectScript mapDatas;

        [Header("ī�޶�")]
        [SerializeField] private CinemachineCamera followCam;

        [Header("��Ÿ")]
        [SerializeField] private const float offset = 1.0f;
        public float    fallingSpeed = 5.0f;
        public bool     isScroll;
        public MapData  currentMapData;

        /// <summary>
        /// ��Ÿ�� ť�� ���� ����Ʈ
        /// </summary>
        private List<Queue<GameObject>> poolingQueueList =new List<Queue<GameObject>>();

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
            for (int i = 0; i < mapDatas.mapDataList.Length; i++)
            {
                poolingQueueList.Add(new Queue<GameObject>());
            }

            ChangeStage();
        }

        void ChangeStage()
        {
            // �ӽ÷� �׳� �������� �۾�, �Ŀ� ����ġ�� �δ� ���� ������ ���� �� ����.
            int currentMapId = 1;/*Random.Range(0, mapDatas.mapDataList.Length);*/
            
            currentMapData = mapDatas.mapDataList[currentMapId];

            SpawnMapPrefab();
        }

        void SpawnMapPrefab()
        {
            StartCoroutine(CoSpawnMapPrefab());
        }

        IEnumerator CoSpawnMapPrefab()
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
                    newMapTile.SetActive(false);
                }
            }
            GameObject startMapTile = poolingQueueList[mapKind].Dequeue();
            startMapTile.transform.position = new Vector2(0f, cameraBottomPosY - 10.0f);
            // nextMapTile Ȱ��ȭ
            startMapTile.SetActive(true);
            poolingQueueList[mapKind].Enqueue(startMapTile);

            nextMapTileNumber++;
            if (nextMapTileNumber >= currentMapData.mapTileArray.Length) nextMapTileNumber = 0;
            #endregion

            Transform lastMapTile = startMapTile.transform; // ���������� ������ �� Ÿ���� Ʈ������
            isScroll = true;
            while (isScroll)
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
                        GameObject prefab = Instantiate(currentMapData.mapTileArray[nextMapTileNumber],
                                                                new Vector2(0f, lastMapTile.position.y - lastMapTile.GetComponent<SpriteRenderer>().bounds.size.y), 
                                                                Quaternion.identity);
                        // lastMapTile�� prefab.transform���� �����ؼ� ������ ������ ��Ÿ�� �ùٸ� ��ġ�� ������ �� �ֵ��� ��.
                        lastMapTile = prefab.transform;
                    }

                    poolingQueueList[mapKind].Enqueue(nextMapTile); // nextMapTile�� poolingQueueList[mapKind]�� Enqueue�ϸ鼭 Ǯ���� �ǵ��� ��.

                    nextMapTileNumber++; // �� ��Ÿ���� ������ ��츦 ���ؼ� ������ �� ��Ÿ���� �� ��° ��Ÿ������ �����.
                    if (nextMapTileNumber >= currentMapData.mapTileArray.Length) nextMapTileNumber = 0;
                }

                yield return null;
            }

            while(nextMapTileNumber != 0 && nextMapTileNumber < currentMapData.mapTileArray.Length) // ���� �� ���������� �� ������ �� ó������ Ǯ���� �ϱ� ����.
            {
                poolingQueueList[mapKind].Enqueue(poolingQueueList[mapKind].Dequeue());
                nextMapTileNumber++;
            }
        }
    }
}