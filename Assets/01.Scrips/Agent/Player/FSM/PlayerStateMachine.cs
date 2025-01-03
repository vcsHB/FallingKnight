using System;
using System.Collections.Generic;
using Agents.Animate;


namespace Agents.Players.FSM
{

    public class PlayerStateMachine
    {
        private Dictionary<string, PlayerState> _stateDictionary = new ();
        public PlayerState CurrentState { get; private set; }
        private Player _player;
        public PlayerStateMachine(Player player)
        {
            _player = player;
        }



        public void Initialize(string firstState)
        {
            AddState("Fall", _player.FallParam);
            AddState("HoldWall", _player.HoldWallParam);
            AddState("Move", _player.FallParam);
            AddState("AirRolling", _player.AirRollingParam);
            AddState("AirAttack", _player.AttackParam);
            AddState("DropAttack", _player.DropAttackParam);

            if (_stateDictionary.TryGetValue(firstState, out PlayerState state))
            {
                CurrentState = state;
                CurrentState.Enter();
            }
        }

        public void AddState(string name, AnimParamSO param)
        {
            Type t = Type.GetType($"Agents.Players.FSM.Player{name}State");
            PlayerState state = Activator.CreateInstance(t, _player, this, param) as PlayerState;
            _stateDictionary.Add(name, state);
        }

        public void UpdateState()
        {
            CurrentState.UpdateState();
        }

        public void ChangeState(string name)
        {
            if (_stateDictionary.TryGetValue(name, out PlayerState state))
            {
                CurrentState.Exit();
                CurrentState = state;
                CurrentState.Enter();
            }
        }


    }
}