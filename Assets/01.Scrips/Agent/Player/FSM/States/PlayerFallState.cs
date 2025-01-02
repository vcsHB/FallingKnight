using System;
using UnityEngine;
namespace Agents.Players.FSM
{
    public class PlayerFallState : PlayerAirState
    {

        public PlayerFallState(Player player, PlayerStateMachine stateMachine, int animationHash) : base(player, stateMachine, animationHash)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _mover.StopImmediately();
        }

        public override void UpdateState()
        {
            base.UpdateState();
            Debug.Log("Fall Update");
            float xInput = _player.PlayerInput.InputDirection.x;
            if(Mathf.Abs(xInput) > 0)
            {
                _stateMachine.ChangeState("Move");
            }
        }






    }
}