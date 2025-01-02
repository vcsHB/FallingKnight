using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerHoldWallState : PlayerState
    {
        public PlayerHoldWallState(Player player, PlayerStateMachine stateMachine, int animationHash) : base(player, stateMachine, animationHash)
        {
        }
        private float _reducePower = 1.5f;

        public override void UpdateState()
        {
            base.UpdateState();
            _mover.ReduceVerticalVelocity(Time.deltaTime * _reducePower);
        }






    }
}