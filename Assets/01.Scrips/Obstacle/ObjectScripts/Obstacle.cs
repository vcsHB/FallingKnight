using System.Collections.Generic;
using UnityEngine;

namespace Obstacles
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] protected Vector3 pos;
        [SerializeField] protected float damage;

        public virtual void SetPos()
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

