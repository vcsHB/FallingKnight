using System;
using Combat;
using UnityEngine;
namespace Agents.Players
{
    public class PlayerAttackController : MonoBehaviour, IAgentComponent
    {
        [SerializeField] private Caster _dropAttackCaster;
        public event Action OnDropAttackSuccessed;
        private Player _player;
        private AgentRenderer _agentRenderer;

        public void Initialize(Agent agent)
        {
            _player = agent as Player;

            _dropAttackCaster.OnCastSuccessEvent.AddListener(HandleDropAttackOver);
        }

        public void HandleUpdateDropAttackCaster()
        {
            _dropAttackCaster.Cast();
        }

        private void HandleDropAttackOver()
        {
            OnDropAttackSuccessed?.Invoke();
            
        }

        public void AfterInit()
        {
        }

        public void Dispose()
        {
        }


        




        

    }
}