namespace Object.Map.MapData
{
    public enum MapIds
    {
        Outside = 0,
        Library = 1,
        Jail = 2,
    }

    [System.Serializable]
    public class MapData
    {
        public MapIds mapId;
    }
}