using System;
using System.Collections.Generic;
using System.Linq;
using StatSystem;
using UnityEngine;


namespace Agents
{

    public class Agent : MonoBehaviour
    {
        [field: SerializeField] public StatusSO Status { get; private set; }
        private Dictionary<Type, IAgentComponent> _components = new Dictionary<Type, IAgentComponent>();

        protected virtual void Awake()
        {
            Status = Instantiate(Status);
            AddComponentToDictionary();
            ComponentInitialize();
            AfterInit();



        }

        private void AddComponentToDictionary()
        {
            GetComponentsInChildren<IAgentComponent>(true)
                .ToList().ForEach(compo => _components.Add(compo.GetType(), compo));
        }

        private void ComponentInitialize()
        {
            _components.Values.ToList().ForEach(compo => compo.Initialize(this));
        }

        private void AfterInit()
        {
            _components.Values.ToList().ForEach(compo => compo.AfterInit());
        }

        public T GetCompo<T>(bool isDerived = false) where T : class
        {
            if (_components.TryGetValue(typeof(T), out IAgentComponent compo))
            {
                return compo as T;
            }

            if (!isDerived) return default;

            Type findType = _components.Keys.FirstOrDefault(x => x.IsSubclassOf(typeof(T)));
            if (findType != null)
                return _components[findType] as T;

            return default(T);
        }

    }

}