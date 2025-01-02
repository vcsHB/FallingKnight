using System;
using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerAttackableAirState : PlayerAirState
    {
        
        public PlayerAttackableAirState(Player player, PlayerStateMachine stateMachine, AnimParamSO stateAnimParam) : base(player, stateMachine, stateAnimParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _player.PlayerInput.OnAttackEvent += HandleAttackEvent;
        }

        public override void Exit()
        {
            base.Exit();
            _player.PlayerInput.OnAttackEvent -= HandleAttackEvent;

        }

        private void HandleAttackEvent()
        {
            _stateMachine.ChangeState("AirAttack");   
        }
    }
}