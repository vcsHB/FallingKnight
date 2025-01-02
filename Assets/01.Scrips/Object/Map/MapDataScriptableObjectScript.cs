using UnityEngine;
using Object.Map.MapData;

[CreateAssetMenu(fileName = "MapDataScriptableObjectScript", menuName = "Scriptable Objects/MapDataScriptableObjectScript")]
public class MapDataScriptableObjectScript : ScriptableObject
{
    public MapData[] mapDataList;
}
