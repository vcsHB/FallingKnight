using System.Collections.Generic;

namespace StatSystem
{

    [System.Serializable]
    public class Stat
    {
        public float baseValue;
        public List<float> modifier;

        private bool _isValueChanged = true;
        private float _cashedValue;
        public float GetValue()
        {
            if (!_isValueChanged) return _cashedValue;


            float result = baseValue;
            for (int i = 0; i < modifier.Count; i++)
            {
                result += modifier[i];
            }
            _cashedValue = result;
            _isValueChanged = false;
            return result;
        }


        public void AddModifier(float value)
        {
            modifier.Add(value);
            _isValueChanged = true;
        }

        public void RemoveModifier(float value)
        {
            modifier.Remove(value);
            _isValueChanged = true;
        }

    }
}