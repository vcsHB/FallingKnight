namespace UpgradeStore.Slots.DefenseUpgrade
{
    //UnityEngine;
    using UnityEngine;

    //Project
    using Managers.Jsonmanager;

    public class DefenseUpgradeSlot : Slot
    {
        private float defenseLevel = default;

        protected override void Start()
        {
            JsonManager.instance.Load();
            defenseLevel = JsonManager.instance.gameData.defenseLevel;

            base.Start();
            upgradeCost += upgradeCostAmountOfIncrease * defenseLevel;
        }

        private void Update()
        {
            LevelUpdate(defenseLevel);
        }


        public override void Upgrade()
        {
            if (JsonManager.instance.gameData.money < upgradeCost || defenseLevel >= upgradeLimit)
            {
                return;
            }

            JsonManager.instance.gameData.money -= (int)upgradeCost;
            upgradeCost += upgradeCostAmountOfIncrease;

            //딕셔너리 참조해서 값 변경하기
            defenseLevel++;
            JsonManager.instance.gameData.defenseLevel = (int)defenseLevel;
            JsonManager.instance.Save();
        }
    }
}

