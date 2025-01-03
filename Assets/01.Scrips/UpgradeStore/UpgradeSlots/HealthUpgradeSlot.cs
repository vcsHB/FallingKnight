namespace UpgradeStore.Slots.HealthUpgrade
{
    //UnityEngine;
    using UnityEngine;

    //Project
    using Managers.Jsonmanager;

    public class HealthUpgradeSlot : Slot
    {
        private float healthLevel = default;

        protected override void Start()
        {
            JsonManager.instance.Load();
            healthLevel = JsonManager.instance.gameData.healthLevel;

            base.Start();
            upgradeCost += upgradeCostAmountOfIncrease * healthLevel;
        }

        private void Update()
        {
            LevelUpdate(healthLevel);
        }


        public override void Upgrade()
        {
            if (JsonManager.instance.gameData.money < upgradeCost || healthLevel >= upgradeLimit)
            {
                return;
            }

            JsonManager.instance.gameData.money -= (int)upgradeCost;
            upgradeCost += upgradeCostAmountOfIncrease;

            //딕셔너리 참조해서 값 변경하기
            healthLevel++;
            JsonManager.instance.gameData.healthLevel = (int)healthLevel;
            JsonManager.instance.Save();
        }
    }
}


