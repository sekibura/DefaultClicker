using Lean.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace SekiburaGames.DefaultClicker.Audio
{
    [CreateAssetMenu(fileName = "PhrasesAsset", menuName = "PhrasesAsset")]
    public class PhrasesAsset : ScriptableObject
    {
        public Phrase[] Phrases;


        [Serializable]
        public class Phrase
        {
            public AudioClip VoicePhrase;
            [Tooltip("The name of the phrase we want to use for this localized component")]
            [SerializeField]
            [LeanTranslationName]
            [FormerlySerializedAs("phraseName")]
            [FormerlySerializedAs("translationTitle")]
            public string TranslationName;
        }
    }
}
