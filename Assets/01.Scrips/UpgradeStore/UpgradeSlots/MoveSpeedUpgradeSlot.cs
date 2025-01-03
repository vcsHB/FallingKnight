namespace UpgradeStore.Slots.MoveSpeedUpgrade
{
    //UnityEngine;
    using UnityEngine;

    //Project
    using Managers.Jsonmanager;

    public class MoveSpeedUpgradeSlot : Slot
    {
        private float moveSpeedLevel = default;

        protected override void Start()
        {
            JsonManager.instance.Load();
            moveSpeedLevel = JsonManager.instance.gameData.moveSpeedLevel;

            base.Start();
            upgradeCost += upgradeCostAmountOfIncrease * moveSpeedLevel;
        }

        private void Update()
        {
            LevelUpdate(moveSpeedLevel);
        }


        public override void Upgrade()
        {
            if (JsonManager.instance.gameData.money < upgradeCost || moveSpeedLevel >= upgradeLimit)
            {
                return;
            }

            JsonManager.instance.gameData.money -= (int)upgradeCost;
            upgradeCost += upgradeCostAmountOfIncrease;

            //딕셔너리 참조해서 값 변경하기
            moveSpeedLevel++;
            JsonManager.instance.gameData.moveSpeedLevel = (int)moveSpeedLevel;
            JsonManager.instance.Save();
        }
    }
}
