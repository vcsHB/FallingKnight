using Agents.Animate;
using StatSystem;
using UnityEngine;
namespace Agents.Players.FSM
{
    public class PlayerAirRollingState : PlayerAirState
    {
        private float _duration = 0.6f;
        private Stat _playerDashPower;
        private Stat _playerJumpPower;
        private float _currentRollingTime;
        public PlayerAirRollingState(Player player, PlayerStateMachine stateMachine,  AnimParamSO stateAnimParam) : base(player, stateMachine, stateAnimParam)
        {
            _playerDashPower = player.PlayerStatus.dashPower;
            _playerJumpPower = player.PlayerStatus.jumpPower;
        }



        public override void Enter()
        {
            base.Enter();
            _mover.CanManualMove = false;
            _mover.AddForceToOuterWall(_playerDashPower.GetValue(), _playerJumpPower.GetValue());

            _currentRollingTime = 0f;
        }

        public override void UpdateState()
        {
            _currentRollingTime += Time.deltaTime;
            if (_currentRollingTime > _duration)
            {
                _stateMachine.ChangeState("Fall");
            }
            //base.UpdateState();
        }

        public override void Exit()
        {
            _mover.StopImmediately();   
            _mover.CanManualMove = true;
            base.Exit();
        }
    }
}