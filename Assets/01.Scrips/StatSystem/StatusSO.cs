using UnityEngine;
namespace StatSystem
{
    [CreateAssetMenu(menuName ="SO/Status/Status")]
    public class StatusSO : ScriptableObject
    {
        public Stat health;
        public Stat attackDamage;
        public Stat moveSpeed;
        public Stat defense;
        public Stat attackSpeed;
    }
}