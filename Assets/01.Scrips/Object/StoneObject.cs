using UnityEditor.ShaderGraph;
using UnityEngine;
namespace ObjectManage
{

    public class StoneObject : ItemObject
    {
        [SerializeField] private int amount;
        [SerializeField] private float _detectRadius = 1f;
        [SerializeField] private LayerMask _collecterLayer;

        private void Update()
        {
            CheckCollector();
        }
        private void CheckCollector()
        {
            Collider2D target = Physics2D.OverlapCircle(transform.position, _detectRadius, _collecterLayer);
            if (target == null) return;

            if (target.TryGetComponent(out ICollectable collector))
            {
                collector.Collect(this);
                gameObject.SetActive(false);

            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _detectRadius);
        }
    }
}