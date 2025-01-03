namespace Obstacles.HorizontalLadder
{
    using Combat;
    using UnityEngine;

    public class HorizontalLadder : Obstacle, IDestroyable
    {
        public void Destroy()
        {
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDamageable hit))
            {
                hit.ApplyDamage(damage);
            }
        }
    }
}
