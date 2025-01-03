using UnityEngine;

namespace Map.MapData
{
    public enum MapKind
    {
        ExteriorWall01,
        ExteriorWall02,
        Library01,
        Library02,
    }

    [System.Serializable]
    public class MapData
    {
        /// <summary>
        /// ∏  ¿Ã∏ß
        /// </summary>
        public MapKind mapKind;

        public GameObject[] mapTileArray;
    }
}