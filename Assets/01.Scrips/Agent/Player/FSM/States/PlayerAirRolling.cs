using UnityEngine;
namespace Agents.Players.FSM
{
    public class PlayerAirRolling : PlayerAirState
    {
        private float _duration;

        private float _currentRollingTime;
        public PlayerAirRolling(Player player, PlayerStateMachine stateMachine, int animationHash) : base(player, stateMachine, animationHash)
        {
        }



        public override void Enter()
        {
            base.Enter();
        }

        public override void UpdateState()
        {
            base.UpdateState();
            _currentRollingTime += Time.deltaTime;
            if(_currentRollingTime < _duration)
            {
                _stateMachine.ChangeState("Fall");
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}