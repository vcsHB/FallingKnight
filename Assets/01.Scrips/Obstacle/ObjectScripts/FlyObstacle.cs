namespace Obstacles.FlyObstacle
{
    //Project
    using UnityEngine;

    public class FlyObstacle : MonoBehaviour, IDestroyable
    {
        [Header("FlyObstacleInfo")]
        [SerializeField] private float moveSpeed         = default;
        [SerializeField] private float moveDistance      = default;

        private Rigidbody2D rb = null;

        private Vector3 startPosition       = Vector3.zero;
        private Vector3 targetPosition      = Vector3.zero;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            startPosition = transform.position;
            targetPosition = transform.position + (Vector3.right * moveDistance);
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            if((targetPosition - transform.position).magnitude >= 1)
            {
                rb.linearVelocityX = moveSpeed;
            }
            else
            {
                transform.position = startPosition;
            }
        }

        public void Destroy()
        {
            gameObject.SetActive(false);
        }
    }
}