namespace Obstacles.Laserbook
{
    using NUnit.Framework.Constraints;
    //System
    using System.Collections;

    //Project
    using UnityEngine;

    public enum LaserFSM
    {
        Idle,
        Shot,
    }

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

        private LaserFSM laserState = LaserFSM.Idle;

        private Animator anim = null;
        private float timer = default;

        private bool isShot = default;

        private void Start()
        {
            anim = GetComponent<Animator>();
            StartCoroutine(Co_ShootingCycle());
            isShot = false;
        }

        private void Update()
        {
            switch (laserState)
            {
                case LaserFSM.Idle:
                    {
                        break;
                    }
                case LaserFSM.Shot:
                    {
                        if (isShot)
                        {
                            ShotLaser();
                        }
                        break;
                    }
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

        private IEnumerator Co_ShootingCycle()
        {
            laserState = LaserFSM.Idle;
            yield return new WaitForSeconds(laserCoolTime);
            laserState = LaserFSM.Shot;
            isShot = true;
            yield return new WaitForSeconds(laserMaintenanceTime);
            isShot = false;
        }

        public void Destroy()
        {
            Debug.Log("파괴");
        }
    }
}



