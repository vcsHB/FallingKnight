namespace Obstacles.Bullet
{
    //UnityEngine
    using UnityEngine;


    public class Bullet : MonoBehaviour
    {
        [Header("BulletInfo")]
        [SerializeField] private float bulletSpeed = default;
        [SerializeField] private float destroyTime = default;

        private void Start()
        {
            Destroy(gameObject, destroyTime);
        }

        private void FixedUpdate()
        {
            transform.Translate(Vector2.right * 5f * Time.fixedDeltaTime);
        }
    }
}




