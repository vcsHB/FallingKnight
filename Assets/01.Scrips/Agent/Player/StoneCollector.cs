using ObjectManage;
using TMPro;
using UnityEngine;

namespace Agents.Players
{

    public class StoneCollector : MonoBehaviour, ICollectable
    {
        [SerializeField] private int _collectAmount = 0;
        [SerializeField] private TextMeshProUGUI _stoneCollectText;
        // UI 추가


        private void Awake()
        {
            RefreshStoneAmountText();
        }

        public void Collect(ItemObject collectObject)
        {
            _collectAmount++;
            RefreshStoneAmountText();

        }


        private void RefreshStoneAmountText()
        {
            _stoneCollectText.text = _collectAmount.ToString();
        }


    }

}