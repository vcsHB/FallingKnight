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

        //������ ��Ÿ��
        [SerializeField] private float laserCoolTime            = default;
        //������ �����ð�
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

        //������ �߻�
        private void ShotLaser()
        {
            RaycastHit2D lowerHit = Physics2D.Raycast(transform.position + (Vector3.up * laserYPos), laserDirection, laserDistance, checkLayer);
            RaycastHit2D upperHit = Physics2D.Raycast(transform.position + (Vector3.up * -laserYPos), laserDirection, laserDistance, checkLayer);

            Debug.DrawRay(transform.position + (Vector3.up * laserYPos), laserDirection * laserDistance, Color.red);
            Debug.DrawRay(transform.position + (Vector3.up * -laserYPos), laserDirection * laserDistance, Color.red);

            if (upperHit.collider != null || lowerHit.collider != null)
            {
                //�÷��̾� ���� ������
            }
        }

        //��Ÿ�� ���� ����Ŀ� ���� �ð� ��ŭ �������� �߻��ϰ� �ݺ���
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
            Debug.Log("�ı�");
        }
    }
}



