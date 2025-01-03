using Agents.Animate;
using ObjectManage;
using StatSystem;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerHoldWallState : PlayerState
    {
        private Stat _playerSpeedReducePower;
        private PlayerHeatController _heatController;
        private bool _isStopped;

        public PlayerHoldWallState(Player player, PlayerStateMachine stateMachine, AnimParamSO stateAnimParam) : base(player, stateMachine, stateAnimParam)
        {
            _playerSpeedReducePower = player.PlayerStatus.speedReducePower;

            _heatController = player.GetCompo<PlayerHeatController>();

        }

        public override void Enter()
        {
            _mover.AddForce(new Vector2(0, -1f));
            base.Enter();
            _player.PlayerInput.OnJumpEvent += HandleDash;
            _player.OnHoldWallEvent?.Invoke();
            _heatController.GainHeat(0.2f);
            _isStopped = false;
        }


        public override void UpdateState()
        {
            base.UpdateState();
            Vector2 wallPoint = _mover.DetectWall();
            if (wallPoint.magnitude < 0.01f)
            {
                _stateMachine.ChangeState("Fall");
            }
            if(_isStopped) return;
            _mover.ReduceVerticalVelocity(Time.deltaTime * _playerSpeedReducePower.GetValue());

            if (Mathf.Abs(_mover.YVelocity) < 0.05f)
            {
                _player.OnReleaseWallEvent?.Invoke();
                _mover.StopImmediately(true);
                _isStopped = true;

            }
            else
            {
                _heatController.GainHeat(Time.deltaTime);
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