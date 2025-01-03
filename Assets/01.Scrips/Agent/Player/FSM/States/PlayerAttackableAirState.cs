using Agents.Animate;

namespace Agents.Players.FSM
{

    public class PlayerAttackableAirState : PlayerAirState
    {
        private PlayerHeatController _heatController;
        public PlayerAttackableAirState(Player player, PlayerStateMachine stateMachine, AnimParamSO stateAnimParam) : base(player, stateMachine, stateAnimParam)
        {
            _heatController = player.GetCompo<PlayerHeatController>();
        }

        public override void Enter()
        {
            base.Enter();
            _player.PlayerInput.OnAttackEvent += HandleAttackEvent;
            _player.PlayerInput.OnDropAttackEvent += HandleDropAttackEvent;
        }

        public override void Exit()
        {
            base.Exit();
            _player.PlayerInput.OnAttackEvent -= HandleAttackEvent;
            _player.PlayerInput.OnDropAttackEvent -= HandleDropAttackEvent;

        }

        private void HandleAttackEvent()
        {
            _stateMachine.ChangeState("AirAttack");  
        }

        private void HandleDropAttackEvent()
        {
            // 발열 레벨 체크 필요
            if(_heatController.CanDropAttack)
                _stateMachine.ChangeState("DropAttack");
        }
    }
}