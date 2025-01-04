namespace Obstacles.Bullet
{
    using Combat;
    //UnityEngine
    using UnityEngine;


    public class Bullet : MonoBehaviour
    {
        [Header("BulletInfo")]
        [SerializeField] private float bulletSpeed = default;
        [SerializeField] private float destroyTime = default;
        [SerializeField] private Caster _caster;

        private void Start()
        {

            Destroy(gameObject, destroyTime);
            _caster.OnCastSuccessEvent.AddListener(HandleDestroyEvent);
        }

        private void FixedUpdate()
        {
            _caster.Cast();
            transform.Translate(Vector2.right * bulletSpeed * Time.fixedDeltaTime);
        }

        public void HandleDestroyEvent()
        {
            Destroy(gameObject);
        }
    }
}




