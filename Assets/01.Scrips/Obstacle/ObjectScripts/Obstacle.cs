using Combat;
using System.Collections.Generic;
using UnityEngine;

namespace Obstacles
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] Vector3 pos;
        [SerializeField] protected float damage;

        private void Awake()
        {
            pos = transform.position;
        }

        private void OnEnable()
        {
            transform.position = pos;
        }
    }
}

