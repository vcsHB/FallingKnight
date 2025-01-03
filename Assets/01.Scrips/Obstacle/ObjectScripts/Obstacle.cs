using System.Collections.Generic;
using UnityEngine;

namespace Obstacles
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private Vector3 pos;

        private void Awake()
        {
            pos = transform.localPosition;
        }

        public virtual void ResetObstacle()
        {
            transform.localPosition = pos;
            gameObject.SetActive(true);
        }
    }
}

