namespace Obstacles.FlyObstacle
{
    //system

    //project
    using UnityEngine;

    public class FlyObstacle : Obstacle, IDestroyable
    {
        [Header("FlyObstacleInfo")]
        [SerializeField] private LayerMask checkLayer    = default;

        [SerializeField] private float checkRadius       = default;
        [SerializeField] private float moveSpeed         = default;
        [SerializeField] private float moveDistance      = default;

        private Rigidbody2D rb = null;

        private Vector3 startPosition       = Vector3.zero;
        private Vector3 targetPosition      = Vector3.zero;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        protected override void Start()
        {
            base.Start();
            startPosition = transform.position;
            targetPosition = transform.position + (Vector3.right * moveDistance);
        }

        private void Update()
        {
            if (CheckCollision())
            {
                //플레이어 데미지 입히기
                Destroy();
            }
        }

        private void FixedUpdate()
        {
            Move();
        }

        private bool CheckCollision()
        {
            return Physics2D.OverlapCircle(transform.position, checkRadius, checkLayer);
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
            Debug.Log("파괴");
        }
    }
}