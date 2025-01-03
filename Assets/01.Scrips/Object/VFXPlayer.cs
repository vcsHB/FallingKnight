using System.Collections;
using ObjectPooling;
using UnityEngine;

namespace ObjectManage
{

    public class VFXPlayer : MonoBehaviour, IPoolable
    {
        [field: SerializeField] public PoolingType type { get; set; }

        public GameObject ObjectPrefab => gameObject;

        [SerializeField] private ParticleSystem _vfx;
        [SerializeField] protected float _lifeTime = 2f;

        public virtual void Play()
        {
            _vfx.Play();

            StartCoroutine(PlayCoroutine());
        }

        private IEnumerator PlayCoroutine()
        {
            yield return new WaitForSeconds(_lifeTime);
            PoolManager.Instance.Push(this);
        }

        public void ResetItem()
        {
            _vfx.Clear();
        }
    }

}