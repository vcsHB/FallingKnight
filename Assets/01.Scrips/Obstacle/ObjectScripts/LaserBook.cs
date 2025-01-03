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

        //������ ��Ÿ��
        [SerializeField] private float laserCoolTime            = default;
        //������ �����ð�
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
                Debug.Log("�߻�");
            }

            if (coolTimeTimer >= laserCoolTime + laserMaintenanceTime + anim.GetCurrentAnimatorStateInfo(0).length)
            {
                Debug.Log("����");
                anim.SetBool("Attack", false);
                isShot = false;

                coolTimeTimer = 0;
            }

            if (isShot)
            {
                ShotLaser();
            }
        }

        //������ �߻�
        private void ShotLaser()
        {
            RaycastHit2D lowerHit = Physics2D.Raycast(transform.position + (Vector3.up * laserYPos), laserDirection, laserDistance, checkLayer);
            RaycastHit2D upperHit = Physics2D.Raycast(transform.position + (Vector3.up * -laserYPos), laserDirection, laserDistance, checkLayer);

            Debug.DrawRay(transform.position + (Vector3.up * laserYPos), laserDirection * laserDistance, Color.red);
            Debug.DrawRay(transform.position + (Vector3.up * -laserYPos), laserDirection * laserDistance, Color.red);

            if(lowerHit.collider != null || upperHit.collider != null)
            {
                //�÷��̾� ���� ó��
            }
        }

        public void ShootTrigger()
        {
            isShot = true;
        }



        public void Destroy()
        {
            Debug.Log("�ı�");
        }
    }
}



