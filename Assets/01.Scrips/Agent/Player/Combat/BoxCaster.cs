using UnityEngine;
namespace Combat
{

    public class BoxCaster : Caster
    {
        [SerializeField] private Vector2 _boxSize;
        public override void Cast()
        {
            _hits = Physics2D.OverlapBoxAll(CenterPosition, _boxSize, _targetLayer);
            for (int i = 0; i < _hits.Length; i++)
            {
                for (int j = 0; j < _casters.Length; j++)
                {
                    _casters[j].Cast(_hits[i]);
                }
            }
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(CenterPosition, _boxSize);
        }
    }
}