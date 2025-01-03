namespace Obstacles.Bone
{
    using Combat;
    //UnityEngine;
    using UnityEngine;

    public class Bone : Obstacle
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDamageable hit))
            {
                hit.ApplyDamage(damage);
            }
        }
    }
}



