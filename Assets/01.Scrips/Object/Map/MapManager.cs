namespace Object.Map.MapSpawner
{
    using UnityEngine;
    using Object.Map.MapData;
    using Object.Map.MapData.ScriptableObject;
    using System.Collections.Generic;
    using System.Collections;

    public class MapManager : MonoBehaviour
    {
        public static MapManager Instance;

        [Header("��ũ���ͺ� ������Ʈ")]
        [SerializeField] private MapDataScriptableObjectScript  mapDatas;

        [Header("��ũ�Ѹ��ǰ� �ִ� ������Ʈ")]
        public List<GameObject> scrolledBackground = new List<GameObject>();
        public List<GameObject> scrolledWall = new List<GameObject>();
        public List<GameObject> scrolledObstacle = new List<GameObject>();

        [Header("��Ÿ")]
        [SerializeField] private const float offset = 1.0f;
        public float    fallingSpeed = 5.0f;
        public bool     isScroll;
        public MapData  currentMapData;

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
            ChangeMap();
        }

        void ChangeMap()
        {
            // �ӽ÷� �׳� �������� �۾�, �Ŀ� ����ġ�� �δ� ���� ������ ���� �� ����.
            int currentMapId = 0;/*Random.Range(0, mapDatas.mapDataList.Length);*/
            
            currentMapData = mapDatas.mapDataList[currentMapId];

            SpawnMapPrefab();
        }

        void SpawnMapPrefab()
        {
            StartCoroutine(CoSpawnMapPrefab());
        }

        IEnumerator CoSpawnMapPrefab()
        {
            int nextBackgroundNumber = 0;
            int nextWallNumber = 0;

            Camera mainCamera = Camera.main;
            Vector2 cameraBottomPos = new Vector2(mainCamera.transform.position.x, mainCamera.transform.position.y - mainCamera.orthographicSize);

            #region ��ũ�Ѹ� ������ ���� ó�� Ÿ�� ����
            GameObject startBackground = Instantiate(currentMapData.backgroundArray[nextBackgroundNumber], cameraBottomPos, Quaternion.identity);
            scrolledBackground.Add(startBackground);

            nextBackgroundNumber++;
            if (nextBackgroundNumber >= currentMapData.backgroundArray.Length) nextBackgroundNumber = 0;

            GameObject startWall = Instantiate(currentMapData.wallArray[nextWallNumber], cameraBottomPos, Quaternion.identity);
            scrolledWall.Add(startWall);

            nextWallNumber++;
            if (nextWallNumber >= currentMapData.wallArray.Length) nextWallNumber = 0;
            #endregion

            isScroll = true;
            while (isScroll)
            {
                #region ��� ����
                Transform backgroundTransform = scrolledBackground[scrolledBackground.Count - 1].transform;

                if (cameraBottomPos.y - offset < backgroundTransform.position.y)
                {
                    GameObject prefab = Instantiate(currentMapData.backgroundArray[nextBackgroundNumber],
                                        scrolledBackground.Count > 0 ? new Vector2(0, backgroundTransform.position.y - backgroundTransform.GetComponent<SpriteRenderer>().bounds.size.y) : cameraBottomPos,
                                        Quaternion.identity);
                    scrolledBackground.Add(prefab);

                    nextBackgroundNumber++;
                    if (nextBackgroundNumber >= currentMapData.backgroundArray.Length) nextBackgroundNumber = 0;
                }
                #endregion

                #region �� ����
                Transform wallTransform = scrolledWall[scrolledWall.Count - 1].transform;

                if (cameraBottomPos.y - offset < wallTransform.position.y)
                {
                    GameObject prefab = Instantiate(currentMapData.wallArray[nextWallNumber],
                                        scrolledWall.Count > 0 ? new Vector2(0, wallTransform.position.y - wallTransform.GetComponentInChildren<SpriteRenderer>().bounds.size.y) : cameraBottomPos,
                                        Quaternion.identity);
                    scrolledWall.Add(prefab);

                    nextWallNumber++;
                    if (nextWallNumber >= currentMapData.wallArray.Length) nextWallNumber = 0;
                }
                #endregion

                yield return null;
            }
        }
    }
}