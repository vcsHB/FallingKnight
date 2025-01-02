using Agents.Players.FSM;
using InputSystem;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Agents.Players
{

    public class Player : Agent
    {
        [field: SerializeField] public PlayerInput PlayerInput { get; private set; }

        public PlayerStateMachine StateMachine;

        protected override void Awake()
        {
            base.Awake();

            StateMachine = new PlayerStateMachine(this);
            StateMachine.Initialize("Fall");
        }

        private void Update()
        {
            StateMachine.UpdateState();
        }

    }
}