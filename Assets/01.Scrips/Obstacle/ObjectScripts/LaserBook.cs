namespace Obstacles.Laserbook
{
    using NUnit.Framework.Constraints;
    //System
    using System.Collections;

    //Project
    using UnityEngine;

    public class LaserBook : MonoBehaviour, IDestroyable
    {
        [Header("LaserBookInfo")]
        [SerializeField] private Vector2 laserDirection = Vector2.zero;
        [SerializeField] private LayerMask checkLayer = default;

        [SerializeField] private float laserYPos                = default;
        [SerializeField] private float laserDistance            = default;

        //레이저 쿨타임
        [SerializeField] private float laserCoolTime            = default;
        //레이저 유지시간
        [SerializeField] private float laserMaintenanceTime     = default;  

        private Animator anim = null;
        private float coolTimeTimer = default;

        private bool isShot = default;

        private void Start()
        {
            anim = GetComponent<Animator>();
        }

        private void Update()
        {
            coolTimeTimer += Time.deltaTime;

            if(coolTimeTimer >= laserCoolTime)
            {
                anim.SetBool("Attack", true);
                Debug.Log("발사");
            }

            if (coolTimeTimer >= laserCoolTime + laserMaintenanceTime + anim.GetCurrentAnimatorStateInfo(0).length)
            {
                Debug.Log("멈춰");
                anim.SetBool("Attack", false);
                isShot = false;

                coolTimeTimer = 0;
            }

            if (isShot)
            {
                ShotLaser();
            }
        }

        //레이저 발사
        private void ShotLaser()
        {
            RaycastHit2D lowerHit = Physics2D.Raycast(transform.position + (Vector3.up * laserYPos), laserDirection, laserDistance, checkLayer);
            RaycastHit2D upperHit = Physics2D.Raycast(transform.position + (Vector3.up * -laserYPos), laserDirection, laserDistance, checkLayer);

            Debug.DrawRay(transform.position + (Vector3.up * laserYPos), laserDirection * laserDistance, Color.red);
            Debug.DrawRay(transform.position + (Vector3.up * -laserYPos), laserDirection * laserDistance, Color.red);

            if(lowerHit.collider != null || upperHit.collider != null)
            {
                //플레이어 피해 처리
            }
        }

        public void ShootTrigger()
        {
            isShot = true;
        }



        public void Destroy()
        {
            Debug.Log("파괴");
        }
    }
}



