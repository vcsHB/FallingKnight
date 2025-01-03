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
            if (_data == null)
            {
                _data = new SoundData();
            }
        }

        private void Start()
        {
            _bgmSlider.onValueChanged.AddListener(HandleChangedBGMSliderValue);
            _sfxSlider.onValueChanged.AddListener(HandleChangedSFXSliderValue);

            _bgmSlider.value = _data.bgmVolume;
            _audioMixer.SetFloat("Volume_BGM", _data.bgmVolume);
            _sfxSlider.value = _data.sfxVolume;
            _audioMixer.SetFloat("Volume_SFX", _data.sfxVolume);
        }


        private void HandleChangedBGMSliderValue(float value)
        {
            _data.bgmVolume = value;
            if (value <= -40f)
                value = -80f;
            _audioMixer.SetFloat("Volume_BGM", value);
        }

        private void HandleChangedSFXSliderValue(float value)
        {
            _data.sfxVolume = value;
            if (value <= -40f)
                value = -80f;
            _audioMixer.SetFloat("Volume_SFX", value);
        }

        public void Save()
        {
            DBManager.ToJson<SoundData>(_data, "SoundData", true);
        }
    }

}