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
        //�ʱ� ���׷��̵� ����
        [SerializeField] private float earlyUpgradeCost               = default;
        //���׷��̵� ���� ������
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


        public void Upgrade(string upgradeType = null)
        {
            if(upgradeLevel >= upgradeLimit)
            {
                return;
            }

            upgradeCost += upgradeCostAmountOfIncrease;

            //��ųʸ� �����ؼ� �� �����ϱ�
            upgradeLevel ++;
        }
    }
}


