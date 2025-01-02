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
        [field:SerializeField] public AnimParamSO FallParam;
        [field:SerializeField] public AnimParamSO HoldWallParam;
        [field:SerializeField] public AnimParamSO AirRollingParam;
        [field:SerializeField] public AnimParamSO AttackParam;
        [Header("Essentials")]

        [field: SerializeField] public PlayerInput PlayerInput { get; private set; }

        public PlayerStatusSO PlayerStatus { get; private set; }

        public PlayerStateMachine StateMachine;

        [Header("Events")]

        public UnityEvent OnHoldWallEvent;
        public UnityEvent OnReleaseWallEvent;
        public UnityEvent OnAttackEvent;
        

        protected override void Awake()
        {
            base.Awake();
            PlayerStatus = Status as PlayerStatusSO;
            StateMachine = new PlayerStateMachine(this);
            StateMachine.Initialize("Fall");
        }

        private void Update()
        {
            StateMachine.UpdateState();
        }

    }
}