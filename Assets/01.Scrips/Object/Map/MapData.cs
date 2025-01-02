using UnityEngine;

namespace Object.Map.MapData
{
    [System.Serializable]
    public class MapData
    {
        /// <summary>
        /// 맵 이름
        /// </summary>
        public string mapName;

        /// <summary>
        /// 배경 배열
        /// </summary>
        public GameObject[] backgroundArray;
        
        /// <summary>
        /// 벽 배열
        /// </summary>
        public GameObject[] wallArray;

        /// <summary>
        /// 장애물 배열
        /// </summary>
        public GameObject[] obstacleArray;
    }
}