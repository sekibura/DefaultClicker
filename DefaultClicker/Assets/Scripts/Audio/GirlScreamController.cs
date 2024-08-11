using SekiburaGames.DefaultClicker.Controllers;
using SekiburaGames.DefaultClicker.System;
using SekiburaGames.DefaultClicker.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SekiburaGames.DefaultClicker.Audio
{
    public class GirlScreamController : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _audioSource;
        [SerializeField]
        private AudioClip[] _audioClips;
        private ClickerGameController _gameController;
        private List<int> _lastAudioSourceIndex = new List<int>();
        private float _lastClickTime;
        [SerializeField]
        private float _deltaTime;

        [SerializeField]
        private float _maxPitch;
        [SerializeField]
        private float _deltaPitch;
        private uint _step;

        private UI_GirlPhrasePanel _girlPhrasePanel;
        private void Start()
        {
            _gameController = SystemManager.Get<ClickerGameController>();
            _gameController.OnClickEvent += Clicked;
            _audioSource = GetComponent<AudioSource>();
            _audioClips.Shuffle(); 
            _girlPhrasePanel = FindAnyObjectByType<UI_GirlPhrasePanel>();
        }
        private void Clicked()
        {
            
            if ((Time.time - _lastClickTime >= _deltaTime))
            {
                _step = 0;
                SetPitch();
                _lastClickTime = Time.time;
                return;
            }

            _lastClickTime = Time.time;
                

            _step++;
            
            SetPitch();
            if (_audioSource.isPlaying)
                return;

            //_audioSource.clip = _audioClips.GetRandom();
            int index = Extensions.GetRandomExclude(0, _audioClips.Length, _lastAudioSourceIndex);
            _audioSource.clip = _audioClips[index];
            _lastAudioSourceIndex.Add(index);
            Debug.Log($"GirlScreamController {index} - {_lastAudioSourceIndex.ToPrintString(" ")}");
            if (_lastAudioSourceIndex.Count == _audioClips.Length)
                _lastAudioSourceIndex.Clear();
            _audioSource.Play();
            _girlPhrasePanel.ShowText("Phrases/Uwu", _audioClips[index].length);
        }

        private void SetPitch()
        {
            _audioSource.pitch = 1 + _deltaPitch * _step > _maxPitch ? _maxPitch : 1 + _deltaPitch * _step;
        }

    }
}

