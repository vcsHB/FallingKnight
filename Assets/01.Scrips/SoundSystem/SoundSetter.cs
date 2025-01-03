using DataManage;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace SoundManage
{
    public class SoundData
    {
        public float bgmVolume = 0;
        public float sfxVolume = 0;

        public SoundData()
        {

        }
    }
    public class SoundSetter : MonoBehaviour
    {
        private SoundData _data;
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private Slider _bgmSlider;
        [SerializeField] private Slider _sfxSlider;

        private void Awake()
        {

            _data = DBManager.FromJson<SoundData>("SoundData");

            _bgmSlider.value = _data.bgmVolume;
            HandleChangedBGMSliderValue(_data.bgmVolume);
            _sfxSlider.value = _data.sfxVolume;
            HandleChangedSFXSliderValue(_data.sfxVolume);

        }


        private void HandleChangedBGMSliderValue(float value)
        {
            _data.bgmVolume = value;
            if (value < -40f)
                value = -80f;
            _audioMixer.SetFloat("Volume_BGM", value);
        }

        private void HandleChangedSFXSliderValue(float value)
        {
            _data.sfxVolume = value;
            if (value < -40f)
                value = -80f;
            _audioMixer.SetFloat("Volume_SFX", value);
        }
    }

}