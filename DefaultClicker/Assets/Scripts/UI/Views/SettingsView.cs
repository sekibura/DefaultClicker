using SekiburaGames.DefaultClicker.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using YG;

namespace SekiburaGames.DefaultClicker.UI
{
    public class SettingsView : View
    {
        [SerializeField]
        private Button _resetBtn;

        [SerializeField]
        private Slider _sliderMusic;
        [SerializeField] 
        private Slider _sliderDialogs;
        [SerializeField]
        private UI_LanguageChooseController _languageChooseController;
        [SerializeField]
        private AudioMixerGroup _audioMixer;
        private bool _isSetSliderValues = false;

        [SerializeField]
        private GameObject _resetWindow;
        [SerializeField]
        private Button _resetSavesBtn;
        [SerializeField]
        private Button _resetWindowCloseBtn;


        public override void Initialize()
        {
            base.Initialize();
            _sliderDialogs.onValueChanged.AddListener((value) => OnSoundDialogsSliderChange(value));
            _sliderMusic.onValueChanged.AddListener((value) => OnMusicSliderChange(value));
            _resetBtn.onClick.AddListener(() => _resetWindow.SetActive(true));
            _resetSavesBtn.onClick.AddListener(() => { YandexGame.ResetSaveProgress(); _resetWindow.SetActive(false); });
            _resetWindowCloseBtn.onClick.AddListener(() => _resetWindow.SetActive(false));
        }

        public override void Show(object parameter = null)
        {
            base.Show(parameter);
            _resetWindow.SetActive(false);
            _languageChooseController.UpdateButtonStates();
            SlidersUpdateValue();
        }
    

        private void SlidersUpdateValue()
        {
            _isSetSliderValues = true;
            float FXVolume;
            _audioMixer.audioMixer.GetFloat("FXVolume", out FXVolume);
            _sliderDialogs.value = Mathf.InverseLerp(-80, 0, FXVolume);



            float MusicVolume;
            _audioMixer.audioMixer.GetFloat("MusicVolume", out MusicVolume);
            _sliderMusic.value = Mathf.InverseLerp(-80, 0, MusicVolume);
            _isSetSliderValues = false;
        }

        private void OnSoundDialogsSliderChange(float value)
        {
            if (_isSetSliderValues)
                return;
            //Set Mixer voluve
            _audioMixer.audioMixer.SetFloat("Dialogs", Mathf.Lerp(-80, 0, value));
        }

        private void OnMusicSliderChange(float value)
        {
            if (_isSetSliderValues)
                return;
            //Set Mixer voluve
            _audioMixer.audioMixer.SetFloat("Music", Mathf.Lerp(-80, 0, value));
        }
    }
}
