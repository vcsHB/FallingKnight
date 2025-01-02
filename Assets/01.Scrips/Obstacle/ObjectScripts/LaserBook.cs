namespace Obstacles.Laserbook
{
    //System
    using System.Collections;

    //Project
    using UnityEngine;

    public enum laserFSM
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

        private laserFSM laserState = laserFSM.Idle;

        private void Start()
        {
            StartCoroutine(Co_CoolTimeCycle());
        }

        private void Update()
        {
            switch (laserState)
            {
                case laserFSM.Idle:
                    {
                        break;
                    }
                case laserFSM.Shot:
                    {
                        ShotLaser();
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

            if (upperHit.collider != null || lowerHit.collider != null)
            {
                //플레이어 피해 입히기
            }
        }

        //쿨타임 동안 대기후에 유지 시간 만큼 레이저를 발사하고 반복함
        private IEnumerator Co_CoolTimeCycle()
        {
            while (true)
            {
                laserState = laserFSM.Idle;
                yield return new WaitForSeconds(laserCoolTime);
                laserState = laserFSM.Shot;
                yield return new WaitForSeconds(laserMaintenanceTime);
            }
        }

        public void Destroy()
        {
            Debug.Log("파괴");
        }
    }
}



