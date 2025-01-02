namespace Obstacles.Shooter
{
    //System
    using System.Collections;

    //Project
    using UnityEngine;

    public class Shooter : MonoBehaviour, IDestroyable
    {
        [Header("ShooterInfo")]
        [SerializeField] private Transform firePosition  = default;
        [SerializeField] private LayerMask checkTarget   = default;
        [SerializeField] private float shotCoolTime      = default;
        [SerializeField] private float checkRadius       = default;
        [SerializeField] private GameObject bulletObject = default;

        private float timer     = default;
        private float rotZ      = default;
        private Animator anim;
        private Transform playerTrasnform = null;

        private void Awake()
        {
            anim = GetComponent<Animator>();
        }

        private void Update()
        {
            transform.rotation = Quaternion.Euler(0,0,rotZ);
            if(CheckPlayer())
            {
                GetFireRot(playerTrasnform);
                timer += Time.deltaTime;

                if (timer > shotCoolTime)
                {
                    anim.SetTrigger("Attack");
                    timer = 0;
                }
            }
            else
            {
                anim.ResetTrigger("Attack");
                timer = 0;
            }
        }

        private void ShotBullet()
        {
            Instantiate(bulletObject, firePosition.position, Quaternion.Euler(0, 0, rotZ));
        }

        private bool CheckPlayer()
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, checkRadius, checkTarget);

            if (collider != null)
            {
                playerTrasnform = collider.transform;
                return true;
            }

            return false;
        }

        private void GetFireRot(Transform targetPos)
        {
            Vector3 rotation = targetPos.position - transform.position;

            rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        }

        public void Destroy()
        {
            Debug.Log("ÆÄ±«");
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, checkRadius);
        }
    }
}



