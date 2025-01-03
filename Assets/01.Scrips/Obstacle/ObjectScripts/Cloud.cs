namespace Obstacles.Cloud
{
    
    //Project
    using UnityEngine;

    public class Cloud : Obstacle
    {
        [Header("CloudInfo")]

        [SerializeField] private LayerMask targetLayer = default;

        [SerializeField] private float JumpPower       = default;

        private Vector2 jumpDirection = Vector2.zero;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Debug.Log("½ÇÇà");
                Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
                rb.linearVelocity = Vector2.zero;
                rb.AddForce(jumpDirection * JumpPower, ForceMode2D.Impulse);
            }


        }
    }
}


