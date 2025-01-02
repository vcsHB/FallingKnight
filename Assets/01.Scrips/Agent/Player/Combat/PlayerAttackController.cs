using UnityEngine;
namespace Agents.Players
{
    public class PlayerAttackController : MonoBehaviour, IAgentComponent
    {
        private Player _player;
        private AgentRenderer _agentRenderer;

        public void Initialize(Agent agent)
        {
            _player = agent as Player;

        }
        public void AfterInit()
        {
        }

        public void Dispose()
        {
        }

        private void Attack()
        {
            Vector2 attackDirection = new Vector2(_agentRenderer.FacingDirection, 0);

            
        }



        

    }
}