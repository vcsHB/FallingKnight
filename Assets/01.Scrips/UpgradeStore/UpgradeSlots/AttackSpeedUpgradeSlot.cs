namespace UpgradeStore.Slots.AttackSpeedUpgrade
{
    //UnityEngine;
    using UnityEngine;

    //Project
    using Managers.Jsonmanager;

    public class AttackSpeedUpgradeSlot : Slot
    {
        private float attackSpeedLevel = default;

        protected override void Start()
        {
            JsonManager.instance.Load();
            attackSpeedLevel = JsonManager.instance.gameData.attackSpeedLevel;

            base.Start();
            upgradeCost += upgradeCostAmountOfIncrease * attackSpeedLevel;
        }

        private void Update()
        {
            LevelUpdate(attackSpeedLevel);
        }


        public override void Upgrade()
        {
            if (JsonManager.instance.gameData.money < upgradeCost || attackSpeedLevel >= upgradeLimit)
            {
                return;
            }

            JsonManager.instance.gameData.money -= (int)upgradeCost;
            upgradeCost += upgradeCostAmountOfIncrease;

            attackSpeedLevel++;
            JsonManager.instance.gameData.attackSpeedLevel = (int)attackSpeedLevel;
            JsonManager.instance.Save();
        }
    }
}

