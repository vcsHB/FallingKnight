using TMPro;
using UnityEngine;
namespace UIManage.InGame
{
    public class GameOverPanel : UIPanel
    {
        [SerializeField] private TextMeshProUGUI _stoneAmountText;
        [SerializeField] private TextMeshProUGUI _depthText;


        public void Initialize(int stoneAmount, int depthLevel)
        {
            _stoneAmountText.text = stoneAmount.ToString();
            _depthText.text = depthLevel.ToString();
        }
    }
}