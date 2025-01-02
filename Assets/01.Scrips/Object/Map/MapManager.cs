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

        [Header("��Ÿ")]
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
            int nextPrefabNumber = 0;

            Camera mainCamera = Camera.main;
            Vector2 cameraBottomPos = new Vector2(mainCamera.transform.position.x, mainCamera.transform.position.y - mainCamera.orthographicSize);

            #region ��ũ�Ѹ� ������ ���� ó�� Ÿ�� ����
            GameObject pre = Instantiate(currentMapData.backgroundArray[nextPrefabNumber], cameraBottomPos, Quaternion.identity);
            scrolledBackground.Add(pre);

            nextPrefabNumber++;
            #endregion

            isScroll = true;
            while (isScroll)
            {
                Transform tileTransform = scrolledBackground[scrolledBackground.Count - 1].transform;

                if (cameraBottomPos.y - 1.0f < tileTransform.position.y)
                {
                    GameObject prefab = Instantiate(currentMapData.backgroundArray[nextPrefabNumber],
                                        scrolledBackground.Count > 0 ? new Vector2(0, tileTransform.position.y - tileTransform.GetComponent<SpriteRenderer>().bounds.size.y) : cameraBottomPos,
                                        Quaternion.identity);
                    scrolledBackground.Add(prefab);

                    nextPrefabNumber++;
                    if (nextPrefabNumber >= currentMapData.backgroundArray.Length) nextPrefabNumber = 0;
                }
                

                yield return null;
            }
        }
    }
}