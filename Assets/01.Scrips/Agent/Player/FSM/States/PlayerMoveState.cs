using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{
    public class PlayerMoveState : PlayerAttackableAirState
    {

        public PlayerMoveState(Player player, PlayerStateMachine stateMachine, AnimParamSO stateAnimParam) : base(player, stateMachine, stateAnimParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }
        public override void UpdateState()
        {
            base.UpdateState();
            float xInput = _player.PlayerInput.InputDirection.x;
            _mover.SetMovement(xInput);

            if (Mathf.Approximately(xInput, 0))
            {
                _stateMachine.ChangeState("Fall");
            }
        }




    }
}