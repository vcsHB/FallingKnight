using Agents.Animate;

namespace Agents.Players.FSM
{

    public class PlayerAttackableAirState : PlayerAirState
    {
        
        public PlayerAttackableAirState(Player player, PlayerStateMachine stateMachine, AnimParamSO stateAnimParam) : base(player, stateMachine, stateAnimParam)
        {
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
            _stateMachine.ChangeState("DropAttack");
        }
    }
}