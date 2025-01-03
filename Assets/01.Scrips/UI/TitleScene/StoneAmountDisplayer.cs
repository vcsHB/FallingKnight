using Managers.Jsonmanager;
using TMPro;
using UnityEngine;

namespace UIManage.TitleScene
{

    public class StoneAmountDisplayer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _stoneAmountText;


        private void Start()
        {
            JsonManager.instance.OnMoneyChangedEvent += SetAmountText;
            JsonManager.instance.Load();
            SetAmountText(JsonManager.instance.gameData.money);
        }

        private void OnDestroy()
        {
            JsonManager.instance.OnMoneyChangedEvent -= SetAmountText;

        }

        public void SetAmountText(int amount)
        {
            _stoneAmountText.text = amount.ToString();
        }
    }

}