using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{
    public class PlayerAirState : PlayerState
    {
        public PlayerAirState(Player player, PlayerStateMachine stateMachine, AnimParamSO stateAnimParam) : base(player, stateMachine, stateAnimParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _mover.SetGravity(true);

        }

        public override void UpdateState()
        {
            base.UpdateState();
            Vector2 wallPoint = _mover.DetectWall();
            if (wallPoint.magnitude > 0.1f)
            {
                HandleHoldWall();
            }

        }

        private void HandleHoldWall()
        {
            _stateMachine.ChangeState("HoldWall");
        }

        public override void Exit()
        {
            //_player.PlayerInput.OnHoldWallEvent -= HandleHoldWall;
            _mover.SetGravity(false);
            base.Exit();
        }

    }
}