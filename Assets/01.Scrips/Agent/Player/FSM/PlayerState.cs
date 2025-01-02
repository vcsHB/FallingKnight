using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerState
    {
        protected Player _player;
        protected PlayerStateMachine _stateMachine;

        protected AnimParamSO _stateAnimParam;
        protected PlayerMover _mover;
        protected AgentRenderer _renderer;
        protected bool _isTriggered;

        public PlayerState(Player player, PlayerStateMachine stateMachine, AnimParamSO stateAnimParam)
        {
            _player = player;
            _renderer = player.GetCompo<AgentRenderer>();
            _stateMachine = stateMachine;
            _stateAnimParam = stateAnimParam;
            _mover = player.GetCompo<PlayerMover>();
        }


        public virtual void Enter()
        {
            _renderer.SetParam(_stateAnimParam, true);
            _isTriggered = false;
        }
        public virtual void UpdateState() { }

        public virtual void Exit()
        {
            _renderer.SetParam(_stateAnimParam, false);
        }
    }
}