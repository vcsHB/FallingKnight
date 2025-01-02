using System;
using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{
    public class PlayerFallState : PlayerAirState
    {

        public PlayerFallState(Player player, PlayerStateMachine stateMachine, AnimParamSO stateAnimParam) : base(player, stateMachine, stateAnimParam)
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