using ObjectManage;
using TMPro;
using UnityEngine;

namespace Agents.Players
{

    public class StoneCollector : MonoBehaviour, ICollectable, IAgentComponent
    {
        [SerializeField] private int _collectAmount = 0;
        [SerializeField] private TextMeshProUGUI _stoneCollectText;
        public int CollectedAmount => _collectAmount;


        private Player _player;

        private void Awake()
        {
            RefreshStoneAmountText();
        }

        public void Collect(ItemObject collectObject)
        {
            _collectAmount++;
            RefreshStoneAmountText();
            _player.OnStoneCollectEvent?.Invoke();
        }


        private void RefreshStoneAmountText()
        {
            _stoneCollectText.text = _collectAmount.ToString();
        }

        public void Initialize(Agent agent)
        {
            _player = agent as Player;
        }

        public void AfterInit() { }

        public void Dispose() { }
    }

}