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
        [SerializeField] private LineRenderer _laserRenderer;
        [SerializeField] private ParticleSystem _fireVFX;
        [SerializeField] private ParticleSystem _hitVFX;

        [SerializeField] private float laserYPos = default;
        [SerializeField] private float laserDistance = default;
        [SerializeField] private Vector2 _laserWidth;

        //������ ��Ÿ��
        [SerializeField] private float laserCoolTime = default;
        //������ �����ð�
        [SerializeField] private float laserMaintenanceTime = default;

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

            if (coolTimeTimer >= laserCoolTime)
            {
                anim.SetBool("Attack", true);
                Debug.Log("�߻�");
            }

            if (coolTimeTimer >= laserCoolTime + laserMaintenanceTime + anim.GetCurrentAnimatorStateInfo(0).length)
            {
                Debug.Log("����");
                anim.SetBool("Attack", false);
                isShot = false;

                SetActiveVFXs(false);
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
            RaycastHit2D hit = Physics2D.BoxCast(transform.position + (Vector3.up * laserYPos), _laserWidth, 0, laserDirection, laserDistance, checkLayer);

            Debug.DrawRay(transform.position + (Vector3.up * laserYPos), laserDirection * laserDistance, Color.red);

            if (hit.collider != null)
            {
                //�÷��̾� ���� ó��
                Vector2 hitPos = hit.point;
                float _minLen = Mathf.Abs(transform.position.x - hitPos.x);
                Vector2 hitPosition = new Vector3(_minLen, 0, 0);
                _laserRenderer.SetPosition(1, hitPosition);
                _hitVFX.transform.localPosition = hitPosition;
            }
        }

        public void ShootTrigger()
        {
            isShot = true;
            SetActiveVFXs(true);

        }

        public void Destroy()
        {
            gameObject.SetActive(false);
        }

        private void SetActiveVFXs(bool value)
        {
            _laserRenderer.enabled = value;
            if (value)
            {
                _fireVFX.Play();
                _hitVFX.Play();
            }
            else
            {
                _fireVFX.Stop();
                _hitVFX.Stop();
            }
        }
    }
}



