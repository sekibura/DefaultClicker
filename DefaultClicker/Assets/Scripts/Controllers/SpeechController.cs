using Lean.Localization;
using PimDeWitte.UnityMainThreadDispatcher;
using SekiburaGames.DefaultClicker.Audio;
using SekiburaGames.DefaultClicker.System;
using SekiburaGames.DefaultClicker.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SekiburaGames.DefaultClicker.Controllers.ShopSystem;

namespace SekiburaGames.DefaultClicker.Controllers
{
    public class SpeechController : MonoBehaviour
    {
        private PhrasesAsset _phrasesCharacterAsset;
        private PhrasesAsset _phrasesBackgroundAsset;
        [SerializeField]
        private AudioSource _audioSource;
        [SerializeField]
        private UI_GirlPhrasePanel _phrasesLocalizedText;

        private void Start()
        {
            //_audioSource = GetComponent<AudioSource>();
            _phrasesCharacterAsset = ResourcesManager.GetPhraseCharacterAsset();
            _phrasesBackgroundAsset = ResourcesManager.GetPhraseBackgroundAsset();
            SystemManager.Get<ShopSystem>().GetShopCategory<CharacterShopCategory>().BuyEvent += OnCharacterBuy;
            SystemManager.Get<ShopSystem>().GetShopCategory<ImageShopCategory>().BuyEvent += OnBackgroundBuy;
        }

        private void OnCharacterBuy()
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                int numberPhrase = Random.Range(0, _phrasesCharacterAsset.Phrases.Length);
                Debug.Log(numberPhrase);
                _audioSource.clip = _phrasesCharacterAsset.Phrases[numberPhrase].VoicePhrase;
                _audioSource.Play();
                _phrasesLocalizedText.ShowText(_phrasesCharacterAsset.Phrases[numberPhrase].TranslationName, _audioSource.clip.length + 2);
            });
     
        }

        private void OnBackgroundBuy()
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                int numberPhrase = Random.Range(0, _phrasesBackgroundAsset.Phrases.Length);
                Debug.Log(numberPhrase);
                _audioSource.clip = _phrasesBackgroundAsset.Phrases[numberPhrase].VoicePhrase;
                _audioSource.Play();
                _phrasesLocalizedText.ShowText(_phrasesBackgroundAsset.Phrases[numberPhrase].TranslationName, _audioSource.clip.length + 2);
            });
    
        }
    }
}
