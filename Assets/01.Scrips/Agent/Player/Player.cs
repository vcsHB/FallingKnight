using Agents.Animate;
using Agents.Players.FSM;
using InputSystem;
using StatSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Agents.Players
{

    public class Player : Agent
    {
        [Header("Animation Params")]
        [field: SerializeField] public AnimParamSO FallParam;
        [field: SerializeField] public AnimParamSO HoldWallParam;
        [field: SerializeField] public AnimParamSO AirRollingParam;
        [field: SerializeField] public AnimParamSO AttackParam;
        [field: SerializeField] public AnimParamSO DropAttackParam;
        [Header("Essentials")]

        [field: SerializeField] public PlayerInput PlayerInput { get; private set; }

        public PlayerStatusSO PlayerStatus { get; private set; }
        public Health HealthCompo { get; private set; }

        public PlayerStateMachine StateMachine;

        [Header("Events")]

        public UnityEvent OnHoldWallEvent;
        public UnityEvent OnHoldingWallEvent;
        public UnityEvent OnReleaseWallEvent;
        public UnityEvent OnAttackEvent;
        public UnityEvent OnDropAttackEvent;
        public UnityEvent OnStoneCollectEvent;


        protected override void Awake()
        {
            base.Awake();
            HealthCompo = GetComponent<Health>();
            PlayerStatus = Status as PlayerStatusSO;
            HealthCompo.Initialize(Status.health.GetValue());
            HealthCompo.OnHealthDecreaseEvent.AddListener(HandlePlayrHit);
            StateMachine = new PlayerStateMachine(this);
            StateMachine.Initialize("Fall");
        }

        private void Update()
        {
            StateMachine.UpdateState();
        }

        
        private void HandlePlayrHit()
        {
            StateMachine.ChangeState("AirRolling");
        }

    }
}