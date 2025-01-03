namespace Obstacles.HorizontalLadder
{
    using UnityEngine;

    public class HorizontalLadder : Obstacle, IDestroyable
    {
        public void Destroy()
        {
            gameObject.SetActive(false);
        }

        public override float GetSpawnPosX()
        {
            return SpawnPosXList[0];
        }
    }
}
