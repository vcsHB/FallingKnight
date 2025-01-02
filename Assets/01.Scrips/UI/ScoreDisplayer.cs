using TMPro;
using UnityEngine;

namespace UIManage.InGame
{
    public class ScoreDisplayer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void HandleRefreshScoreText(int deepLevel)
        {
            _text.text =deepLevel.ToString("");''
        }
    }

}