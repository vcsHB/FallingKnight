using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerState
    {
        protected Player _player;
        protected PlayerStateMachine _stateMachine;
        protected PlayerMover _mover;


        public PlayerState(Player player, PlayerStateMachine stateMachine, int animationHash)
        {
            _player = player;
            _stateMachine= stateMachine;
            _mover = player.GetCompo<PlayerMover>();
        }


        public virtual void Enter()
        {

        }
        public virtual void UpdateState()
        {
            
        }

        public virtual void Exit()
        {

        }
    }
}