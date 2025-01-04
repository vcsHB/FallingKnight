using UnityEngine;

namespace Map.MapData
{
    public enum MapKind
    {
        ExteriorWall_Day,
        ExteriorWall_Night,
        Library,
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