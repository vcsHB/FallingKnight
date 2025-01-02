using UnityEngine;
namespace Combat
{

    public abstract class Caster : MonoBehaviour
    {
        [SerializeField] protected Vector2 _offset;
        [SerializeField] protected LayerMask _targetLayer;
        [SerializeField] protected int _targetMaxAmount;
        protected ICastable[] _casters;
        protected Collider2D[] _hits;
        public Vector2 CenterPosition => (Vector2)transform.position + _offset;  

        protected virtual void Awake()
        {
            _casters = GetComponentsInChildren<ICastable>();
        }

        public abstract void Cast();
    }
}