using UnityEngine;

namespace Object.Map.MapData
{
    [System.Serializable]
    public class MapData
    {
        /// <summary>
        /// �� �̸�
        /// </summary>
        public string mapName;

        /// <summary>
        /// ��� �迭
        /// </summary>
        public GameObject[] backgroundArray;
        
        /// <summary>
        /// �� �迭
        /// </summary>
        public GameObject[] wallArray;

        /// <summary>
        /// ��ֹ� �迭
        /// </summary>
        public GameObject[] obstacleArray;
    }
}