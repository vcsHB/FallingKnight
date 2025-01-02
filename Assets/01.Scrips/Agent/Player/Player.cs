using Agents.Players.FSM;
using InputSystem;
using StatSystem;
using UnityEngine;

namespace Agents.Players
{

    public class Player : Agent
    {
        [field: SerializeField] public PlayerInput PlayerInput { get; private set; }

        public PlayerStatusSO PlayerStatus { get; private set; }

        public PlayerStateMachine StateMachine;

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