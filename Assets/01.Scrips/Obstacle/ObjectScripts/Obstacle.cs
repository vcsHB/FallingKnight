using System.Collections.Generic;
using UnityEngine;

namespace Obstacles
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] protected List<float> SpawnPosXList;

        public virtual float GetSpawnPosX()
        {
            return SpawnPosXList[Random.Range(0, SpawnPosXList.Count)];
        }
    }
}

