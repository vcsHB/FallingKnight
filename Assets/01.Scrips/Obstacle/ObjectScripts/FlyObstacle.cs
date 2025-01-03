namespace Obstacles.FlyObstacle
{
    using Combat;
    using Managers;
    //Project
    using UnityEngine;

    public class FlyObstacle : Obstacle, IDestroyable
    {
        [Header("FlyObstacleInfo")]
        [SerializeField] private float moveSpeed         = default;
        [SerializeField] private float moveDistance      = default;

        private Rigidbody2D rb = null;
        private SpriteRenderer sr = null;

        private Vector3 startPosition       = Vector3.zero;
        private Vector3 targetPosition      = Vector3.zero;

        private bool isTrun              = default;

        private Quaternion rot = Quaternion.identity;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            sr = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            isTrun = false;
            startPosition = transform.position;
            targetPosition = transform.position + (Vector3.right * moveDistance);
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            rb.linearVelocityX = moveSpeed;

            if (!isTrun && (targetPosition - transform.position).magnitude <= 1)
            {
                moveSpeed *= -1;
                isTrun = true;
                sr.flipX = true;
            }
            if (isTrun && (startPosition - transform.position).magnitude <= 1)
            {
                moveSpeed *= -1;
                isTrun = false;
                sr.flipX = false;
            }
        }

        public void Destroy()
        {
            gameObject.SetActive(false);
            GameManager.Instance.AddStone(5);

        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(targetPosition, 0.5f);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDamageable hit))
            {
                hit.ApplyDamage(damage);
            }
        }

        public override void SetPos()
        {
            base.SetPos();

            rot = transform.localRotation;
        }

        public override void ResetObstacle()
        {
            transform.localPosition = pos;
            transform.localRotation = rot;
            gameObject.SetActive(true);
        }
    }
}