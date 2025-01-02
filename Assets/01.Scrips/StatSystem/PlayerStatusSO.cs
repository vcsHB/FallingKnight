using System.Collections.Generic;
using UnityEngine;
namespace StatSystem
{
    public enum StatType
    {
        Health,
        AttackDamage,
        MoveSpeed,
        Defense,
        AttackSpeed,
        DashPower,
        JumpPower
    }


    [CreateAssetMenu(menuName = "SO/Status/PlayerStatus")]
    public class PlayerStatusSO : StatusSO
    {


        public Dictionary<StatType, Stat> statDictionary = new Dictionary<StatType, Stat>();
        public Stat dashPower;
        public Stat jumpPower;

        public void AddModifier(StatType targetStat, int increaseValue)
        {
            if(statDictionary.TryGetValue(targetStat, out Stat stat))
            {
                stat.AddModifier(increaseValue);
            }
        }

        public void RemoveModifier(StatType targetStat, int increaseValue)
        {
            if(statDictionary.TryGetValue(targetStat, out Stat stat))
            {
                stat.RemoveModifier(increaseValue);
            }
        }

        private void OnEnable()
        {
            statDictionary.Add(StatType.Health, health);
            statDictionary.Add(StatType.AttackDamage, attackDamage);
            statDictionary.Add(StatType.MoveSpeed, moveSpeed);
            statDictionary.Add(StatType.Defense, defense);
            statDictionary.Add(StatType.AttackSpeed, attackSpeed);
            statDictionary.Add(StatType.DashPower, dashPower);
            statDictionary.Add(StatType.JumpPower, jumpPower);
        }
    }
}