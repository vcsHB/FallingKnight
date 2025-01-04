using TMPro;
using UnityEngine;
namespace UIManage.InGame
{
    public class GameOverPanel : UIPanel
    {
        [SerializeField] private TextMeshProUGUI _stoneAmountText;
        [SerializeField] private TextMeshProUGUI _depthText;


        public void Initialize(int stoneAmount, int depthLevel, bool isNewScore)
        {
            _stoneAmountText.text = $"얻은 마석 : {stoneAmount.ToString()}";
            if (isNewScore)
            {
                _depthText.text = $"도달한 깊이 : {depthLevel.ToString()} <color=yellow>(NEW)</color>";
            }else{
                _depthText.text = $"도달한 깊이 : {depthLevel.ToString()}";
            }
        }
    }
}