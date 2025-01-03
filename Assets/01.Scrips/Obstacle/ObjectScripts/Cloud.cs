namespace Obstacles.Cloud
{
    
    //Project
    using UnityEngine;
    using Agents.Players;

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
                collision.gameObject.GetComponent<Player>().HandlePlayrHit();
            }
        }
    }
}


