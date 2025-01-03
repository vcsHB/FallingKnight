namespace Map.MapData.ScriptableObject
{
    using UnityEngine;
    using Map.MapData;

    [CreateAssetMenu(fileName = "MapDataScriptableObjectScript", menuName = "Scriptable Objects/MapDataScriptableObjectScript")]
    public class MapDataScriptableObjectScript : ScriptableObject
    {
        public MapData[] mapDataList;
    }
}
