namespace Obstacles.Bigbook
{
    //UnityEngine
    using UnityEngine;

    public class BigBook : Obstacle
    {
        [Header("BigBookInfo")]
        [SerializeField] private float oppenBookDistance    = default;
        [SerializeField] private LayerMask checkLayer       = default;
        [SerializeField] private float checkRadius          = default;

        private Transform playerTrasnform = null;
        private Animator anim = null;

        private void Awake()
        {
            anim = GetComponent<Animator>();
        }

        private void Update()
        {
            if (CheckPlayer())
            {
                if((playerTrasnform.position - transform.position).magnitude <= oppenBookDistance)
                {
                    anim.SetTrigger("Oppen");
                }
            }
        }


        private bool CheckPlayer()
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, checkRadius, checkLayer);

            if (collider != null)
            {
                playerTrasnform = collider.transform;
                return true;
            }

            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            
            Gizmos.DrawWireSphere(transform.position, checkRadius);
        }
    }
}



