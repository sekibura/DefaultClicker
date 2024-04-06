using SekiburaGames.DefaultClicker.Controllers;
using SekiburaGames.DefaultClicker.System;
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

        protected SaveLoadController saveLoadController;

        public override void Initialize()
        {
            base.Initialize();
            _sliderDialogs.onValueChanged.AddListener((value) => OnSoundDialogsSliderChange(value));
            _sliderMusic.onValueChanged.AddListener((value) => OnMusicSliderChange(value));
            _resetBtn.onClick.AddListener(() => _resetWindow.SetActive(true));
            _resetSavesBtn.onClick.AddListener(() => { YandexGame.ResetSaveProgress(); _resetWindow.SetActive(false); });
            _resetWindowCloseBtn.onClick.AddListener(() => _resetWindow.SetActive(false));

            SystemManager.Get(out saveLoadController);
            saveLoadController.LoadEvent += (x) => LoadSettings();
            if (YandexGame.SDKEnabled == true)
            {
                LoadSettings();
            }
            
        }

        public override void Show(object parameter = null)
        {
            base.Show(parameter);
            _resetWindow.SetActive(false);
            _languageChooseController.UpdateButtonStates();
            SlidersUpdateValue();
        }

        public override void OnBackButton()
        {
            if (YandexGame.SDKEnabled == true)
            {
                SaveSettings();
            }
        }


        private void SlidersUpdateValue()
        {
            _isSetSliderValues = true;
            float dialogs;
            _audioMixer.audioMixer.GetFloat("Dialogs", out dialogs);            
            _sliderDialogs.value = Mathf.InverseLerp(-80, 0, dialogs);

            float music;
            _audioMixer.audioMixer.GetFloat("Music", out music);
            _sliderMusic.value = Mathf.InverseLerp(-80, 0, music);
            Debug.Log($"FXVolume MusicVolume {dialogs} {music}");
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

        private void LoadSettings()
        {
            var saveData = saveLoadController.Load();
            Debug.Log($"Load settings = {saveData.SettingsDialogVolume} {saveData.SettingsMusicVolume} {saveData.lang}");
            //_sliderDialogs.value = saveData.SettingsDialogVolume;
            //_sliderMusic.value = saveData.SettingsMusicVolume;
            OnMusicSliderChange(saveData.SettingsMusicVolume);
            OnSoundDialogsSliderChange(saveData.SettingsDialogVolume);
            _languageChooseController.SetLanguage(saveData.lang);
        }

        private void SaveSettings()
        {
            var saveData = saveLoadController.Load();
            saveData.SettingsDialogVolume = _sliderDialogs.value;
            saveData.SettingsMusicVolume = _sliderMusic.value;
            saveData.lang = _languageChooseController.Language;
            Debug.Log($"Save settings = {_sliderDialogs.value} {_sliderMusic.value} {_languageChooseController.Language}");
            saveLoadController.Save(saveData);
        }
    }
}
