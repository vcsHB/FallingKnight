using System;
using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerDropAttackState : PlayerAirState
    {
        private PlayerAttackController _attackController;
        public PlayerDropAttackState(Player player, PlayerStateMachine stateMachine, AnimParamSO stateAnimParam) : base(player, stateMachine, stateAnimParam)
        {
            _attackController = player.GetCompo<PlayerAttackController>();
            _attackController.OnDropAttackSuccessed += HandleDropAttackOver;
        }


        public override void Enter()
        {
            base.Enter();
            _mover.CanManualMove = false;
            _mover.StopImmediately(true);
            _mover.AddForce(new Vector2(0, -12f));
            _player.OnDropAttackEvent?.Invoke();
            _attackController.OnDropAttackSuccessed += HandleDropAttackOver;
        }

        public override void UpdateState()
        {
            base.UpdateState();
            _attackController.HandleUpdateDropAttackCaster();
        }


        public override void Exit()
        {
            base.Exit();
            _attackController.OnDropAttackSuccessed -= HandleDropAttackOver;


        }
        private void HandleDropAttackOver()
        {
            //Debug.Log("Exit DroAttack");
            _mover.StopImmediately(true);
            _stateMachine.ChangeState("AirRolling");
        }


    }
}