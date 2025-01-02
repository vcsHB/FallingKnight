using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Managers
{

    public class VolumeManager : MonoSingleton<VolumeManager>
    {
        [SerializeField] private Volume _globalVolume;
        private ChromaticAberration _chromaticEffect;


        private void Awake()
        {
            _globalVolume.profile.TryGet(out _chromaticEffect);
        }

        public void HandleChromaticActive()
        {
            SetChromatic(0.13f);
        }

        public void HandleChromaticDisable()
        {
            SetChromatic(0f);
        }
        

        public void SetChromatic(float value)
        {
            _chromaticEffect.intensity.value = value;
            
        }

        
    }

}