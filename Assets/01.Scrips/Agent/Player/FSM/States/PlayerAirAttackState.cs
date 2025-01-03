using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{
    public class PlayerAirAttackState : PlayerAirState
    {
        public PlayerAirAttackState(Player player, PlayerStateMachine stateMachine, AnimParamSO stateAnimParam) : base(player, stateMachine, stateAnimParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _mover.CanManualMove = false;
            _mover.StopImmediately(true);
            _mover.SetGravity(false);
            _mover.AddForce(new Vector2(_renderer.FacingDirection * 3f, 0));
            _player.OnAttackEvent?.Invoke();

        }


        public override void Exit()
        {
            base.Exit();
            _mover.CanManualMove = true;
            _mover.SetGravity(true);

        }

        public override void AnimationEndTrigger()
        {
            base.AnimationEndTrigger();
            _mover.SetGravity(true);
            _stateMachine.ChangeState("AirRolling");

        }



    }
}