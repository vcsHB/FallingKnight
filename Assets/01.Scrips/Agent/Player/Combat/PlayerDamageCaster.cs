using ObjectManage;
using UnityEngine;
namespace Combat
{

    public class PlayerDamageCaster : DamageCaster
    {

        public override void Cast(Collider2D target)
        {
            if(target.TryGetComponent(out IDamageable hit))
            {
                hit.ApplyDamage(_damage);
                VFXPlayer vfx = PoolManager.Instance.Pop(
                    ObjectPooling.PoolingType.AttackTargetHitVFX, 
                    target.transform.position,
                    Quaternion.identity) as VFXPlayer;
                vfx.Play();
            }
        }

    }
}