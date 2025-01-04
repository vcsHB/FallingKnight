using Managers.Jsonmanager;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace UIManage.TitleScene
{

    public class BestScorePanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _highScoreText;



        private void Start()
        {
            int score = JsonManager.instance.gameData.bestSocre;
            SetScoreText(score);
        }

        public void SetScoreText(int score)
        {
            if (score <= 0)
                _highScoreText.text = "빨리 출발합시다";
            else
                _highScoreText.text = $"<size=64>{score}M</size>  에   도달함";

        }
    }

}