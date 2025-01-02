using Agents.Animate;
using ObjectManage;
using StatSystem;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerHoldWallState : PlayerState
    {
        private Stat _playerSpeedReducePower;


        public PlayerHoldWallState(Player player, PlayerStateMachine stateMachine, AnimParamSO stateAnimParam) : base(player, stateMachine, stateAnimParam)
        {
            _playerSpeedReducePower = player.PlayerStatus.speedReducePower;

        }

        public override void Enter()
        {
            base.Enter();
            _player.PlayerInput.OnJumpEvent += HandleDash;
            _player.OnHoldWallEvent?.Invoke();
            

        }


        public override void UpdateState()
        {
            Debug.Log("HoldWall");
            base.UpdateState();
            Vector2 wallPoint = _mover.DetectWall();
            if (wallPoint.magnitude < 0.01f)
            {
                _stateMachine.ChangeState("Fall");
            }
            _mover.ReduceVerticalVelocity(Time.deltaTime * _playerSpeedReducePower.GetValue());

            if (Mathf.Abs(_mover.YVelocity) < 0.1f)
            {
                _player.OnReleaseWallEvent?.Invoke();

            }
        }

        public override void Exit()
        {
            _player.OnReleaseWallEvent?.Invoke();
            VFXPlayer vfx = PoolManager.Instance.Pop(ObjectPooling.PoolingType.DashVFX, _player.transform.position, Quaternion.identity) as VFXPlayer;
            vfx.Play();
            base.Exit();
            _player.PlayerInput.OnJumpEvent -= HandleDash;

        }

        private void HandleDash()
        {
            _mover.StopImmediately();
            _stateMachine.ChangeState("AirRolling");
        }
    }
}