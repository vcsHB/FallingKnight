using UnityEngine;
namespace Agents.Players
{
    public class PlayerAttackController : MonoBehaviour, IAgentComponent
    {
        private Player _player;

        public void AfterInit()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public void Initialize(Agent agent)
        {
            _player = agent as Player;
    
        }
    }
}