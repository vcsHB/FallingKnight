using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{
    public class PlayerAirAttackState : PlayerAirState
    {

        private PlayerAttackController _attackController;
        public PlayerAirAttackState(Player player, PlayerStateMachine stateMachine, AnimParamSO stateAnimParam) : base(player, stateMachine, stateAnimParam)
        {
            _attackController  =player.GetCompo<PlayerAttackController>();
        }

        public override void Enter()
        {
            base.Enter();


        }


        public override void Exit()
        {
            base.Exit();

        }



    }
}