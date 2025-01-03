using UnityEngine;

namespace Map.MapData
{
    public enum MapKind
    {
        ExteriorWall01,
        Library01,
    }

    [System.Serializable]
    public class MapData
    {
        /// <summary>
        /// �� �̸�
        /// </summary>
        public MapKind mapKind;

        public GameObject[] mapTileArray;
    }
}