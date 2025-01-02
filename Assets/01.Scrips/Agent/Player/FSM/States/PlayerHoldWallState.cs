using System;
using StatSystem;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerHoldWallState : PlayerState
    {
        private float _reducePower = 0.5f;
        private Stat _playerDashPower;
        private Stat _playerJumpPower;

        public PlayerHoldWallState(Player player, PlayerStateMachine stateMachine, int animationHash) : base(player, stateMachine, animationHash)
        {
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
            _mover.ReduceVerticalVelocity(Time.deltaTime * _reducePower);
        }

        public override void Exit()
        {
            base.Exit();
            _player.PlayerInput.OnJumpEvent -= HandleDash;
        }

        private void HandleDash()
        {
            _mover.AddForce(new Vector2(_player.PlayerInput.InputDirection.x * 4f, 2f));
            _stateMachine.ChangeState("Roll");
        }
    }
}