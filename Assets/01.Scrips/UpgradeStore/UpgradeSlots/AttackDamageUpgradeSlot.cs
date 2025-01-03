namespace UpgradeStore.Slots.AttackDamageUpgrade
{
    //UnityEngine;
    using UnityEngine;

    //Project
    using Managers.Jsonmanager;

    public class AttackDamageUpgradeSlot : Slot
    {
        private float attackDamageLevel = default;

        protected override void Start()
        {
            JsonManager.instance.Load();
            attackDamageLevel = JsonManager.instance.gameData.attackDamageLevel;

            base.Start();
            upgradeCost += upgradeCostAmountOfIncrease * attackDamageLevel;
        }

        private void Update()
        {
            LevelUpdate(attackDamageLevel);
        }


        public override void Upgrade()
        {
            if (JsonManager.instance.gameData.money < upgradeCost || attackDamageLevel >= upgradeLimit)
            {
                return;
            }

            JsonManager.instance.gameData.money -= (int)upgradeCost;
            upgradeCost += upgradeCostAmountOfIncrease;

            attackDamageLevel++;
            JsonManager.instance.gameData.attackDamageLevel = (int)attackDamageLevel;
            JsonManager.instance.Save();
        }
    }
}
