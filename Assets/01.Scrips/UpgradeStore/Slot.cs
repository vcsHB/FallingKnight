namespace UpgradeStore.Slots
{
    //Project
    using UnityEngine;

    //TMP
    using TMPro;
    using UnityEngine.UI;

    public class Slot : MonoBehaviour
    {
        [Header("UpgradeSlotInfo")]
        [SerializeField] private float upgradeLimit                   = default;
        //초기 업그래이드 가격
        [SerializeField] private float earlyUpgradeCost               = default;
        //업그래이드 가격 증가량
        [SerializeField] private float upgradeCostAmountOfIncrease    = default;
        [SerializeField] private TextMeshProUGUI upgradeCostText      = default;
        [SerializeField] private Image upgradeLevelSlider             = default;

        private float upgradeLevel      = default;
        private float upgradeCost       = default;

        private void Start()
        {
            upgradeLevel = 0;
            upgradeCost = earlyUpgradeCost;
        }

        private void Update()
        {
            LoadValue();
            
            upgradeLevelSlider.fillAmount = upgradeLevel / upgradeLimit;

            if(upgradeLevel == upgradeLimit)
            {
                upgradeCostText.text = "MAX";
            }
            else
            {
                
                upgradeCostText.text = upgradeCost.ToString();
            }
        }

        private void LoadValue()
        {
            if (PlayerPrefs.HasKey("UpgradeLevel"))
            {
                upgradeLevel = PlayerPrefs.GetFloat("UpgradeLevel");
                upgradeCost = PlayerPrefs.GetFloat("UpgradeCost");
            }
        }


        public void Upgrade(string upgradeType = null)
        {
            if(upgradeLevel >= upgradeLimit)
            {
                return;
            }

            upgradeCost += upgradeCostAmountOfIncrease;
            PlayerPrefs.SetFloat("UpgradeCost", upgradeCost);

            //딕셔너리 참조해서 값 변경하기
            upgradeLevel ++;
            PlayerPrefs.SetFloat("UpgradeLevel", upgradeLevel);
        }
    }
}


