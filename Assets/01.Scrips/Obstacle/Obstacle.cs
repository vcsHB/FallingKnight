namespace Obstacles
{
    //Unity Engine
    using UnityEngine;

    //��ֹ� �θ� Ŭ����
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



