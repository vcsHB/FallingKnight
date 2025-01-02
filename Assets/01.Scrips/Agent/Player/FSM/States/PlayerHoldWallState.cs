using StatSystem;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerHoldWallState : PlayerState
    {
        private Stat _playerSpeedReducePower;
       

        public PlayerHoldWallState(Player player, PlayerStateMachine stateMachine, int animationHash) : base(player, stateMachine, animationHash)
        {
            _playerSpeedReducePower = player.PlayerStatus.speedReducePower;
            
        }

        public override void Enter()
        {
            base.Enter();
            _player.PlayerInput.OnJumpEvent += HandleDash;

        }


        public override void UpdateState()
        {
            Debug.Log("HoldWall");
            base.UpdateState();
            Vector2 wallPoint = _mover.DetectWall();
            if(wallPoint.magnitude < 0.01f)
            {
                _stateMachine.ChangeState("Fall");                
            }
            _mover.ReduceVerticalVelocity(Time.deltaTime * _playerSpeedReducePower.GetValue());
        }

        public override void Exit()
        {
            base.Exit();
            _player.PlayerInput.OnJumpEvent -= HandleDash;

        }

        private void HandleDash()
        {
            _mover.StopImmediately();
            _stateMachine.ChangeState("AirRolling");
        }
    }
}