using UnityEngine;

namespace Map.MapData
{
    public enum MapKind
    {
        ExteriorWall,
        Library,
        Jail,
    }

    [System.Serializable]
    public class MapData
    {
        /// <summary>
        /// ∏  ¿Ã∏ß
        /// </summary>
        public MapKind mapKind;

        public GameObject[] mapTileArray;

        public GameObject[] ObstacleArray;
    }
}