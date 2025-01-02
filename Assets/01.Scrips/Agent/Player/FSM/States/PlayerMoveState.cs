using UnityEngine;
namespace Agents.Players.FSM
{
    public class PlayerMoveState : PlayerAirState
    {

        public PlayerMoveState(Player player, PlayerStateMachine stateMachine, int animationHash) : base(player, stateMachine, animationHash)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }
        public override void UpdateState()
        {
            Debug.Log("Move Update");
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