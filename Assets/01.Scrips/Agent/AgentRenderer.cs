using System;
using Agents.Animate;
using UnityEngine;

namespace Agents.Players
{

    public class AgentRenderer : AnimateRenderer, IAgentComponent
    {
        [field: SerializeField] public float FacingDirection { get; private set; } = 1;
        protected Agent _entity;

        public void Initialize(Agent entity)
        {
            _entity = entity;
        }
        public void AfterInit()
        {
        }

        public void Dispose()
        {
        }


        public void Flip()
        {
            FacingDirection *= -1;
            _entity.transform.Rotate(0, 180f, 0);
        }

        public void FlipController(float normalizeXMove)
        {
            if (Mathf.Abs(FacingDirection + normalizeXMove) < 0.5f)
                Flip();
        }


    }

}