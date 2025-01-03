namespace UpgradeStore.Slots
{
    //Project
    using UnityEngine;

    //TMP
    using TMPro;
    using UnityEngine.UI;

    public abstract class Slot : MonoBehaviour
    {
        [Header("UpgradeSlotInfo")]
        [SerializeField] protected float upgradeLimit                   = default;
        //초기 업그래이드 가격
        [SerializeField] protected float earlyUpgradeCost               = default;
        //업그래이드 가격 증가량
        [SerializeField] protected float upgradeCostAmountOfIncrease    = default;
        [SerializeField] protected TextMeshProUGUI upgradeCostText      = default;
        [SerializeField] protected Image upgradeLevelSlider             = default;

        protected float upgradeCost                                     = default;

        protected virtual void Start()
        {
            upgradeCost = earlyUpgradeCost;
        }

        protected void LevelUpdate(float upgradeLevel)
        {
            upgradeLevelSlider.fillAmount = upgradeLevel / upgradeLimit;

            if (upgradeLevel == upgradeLimit)
            {
                upgradeCostText.text = "MAX";
            }
            else
            {

                upgradeCostText.text = upgradeCost.ToString();
            }
        }

        public abstract void Upgrade();
    }
}


