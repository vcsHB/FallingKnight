namespace Obstacles
{
    //Unity Engine
    using UnityEngine;

    //장애물 부모 클래스
    public class Obstacle : MonoBehaviour
    {
        [Header("ObstacleInfo")]
        [SerializeField] private ObstacleSO obstacleData;

        protected virtual void Start()
        {
            SetData(obstacleData);
        }

        private void SetData(ObstacleSO obstacleData)
        {

        }
    }
}



