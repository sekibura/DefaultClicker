using SekiburaGames.DefaultClicker.Audio;
using SekiburaGames.DefaultClicker.ShopItems;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace SekiburaGames.DefaultClicker.System
{
    public class ResourcesManager : MonoBehaviour
    {
        #region ResourcesPaths
        private const string ShopBackgroundAssetPath = "ScriptableObjects/BackgroundsAsset"; 
        private const string ShopCharacterAssetPath = "ScriptableObjects/CharacterAsset"; 
        private const string PhrasesCharacterAssetPath = "ScriptableObjects/PhrasesCharacterAsset"; 
        private const string PhrasesBackgroundAssetPath = "ScriptableObjects/PhrasesBackgroundAsset"; 
        private const string PhrasesBuysAssetPath = "ScriptableObjects/PhrasesBuysAsset"; 
        #endregion

        #region Private fields
        private static ShopImageAsset _shopBackgroundsAsset;
        private static ShopImageAsset _shopCharactersAsset;
        private static PhrasesAsset _phrasesCharacterAsset;
        private static PhrasesAsset _phrasesBackgroundAsset;
        private static PhrasesAsset _phrasesBuysAsset;
        #endregion

        #region Getters 

        public static ShopImageAsset GetAssetByName(string assetName)
        {
            switch (assetName)
            {
                case "BackgroundsAsset":
                    return _shopBackgroundsAsset;
                case "CharacterAsset":
                    return _shopCharactersAsset;
                case "PhrasesCharacterAsset":
                    return _shopCharactersAsset;
                case "PhrasesBackgroundAsset":
                    return _shopCharactersAsset;
                default:
                    return _shopBackgroundsAsset;
            }
        }
        public static ShopImageAsset GetShopCharactersAsset()
        {
            return _shopCharactersAsset;
        }
        public static ShopImageAsset GetShopBackgroundsAsset()
        {
            return _shopBackgroundsAsset;
        }
        public static PhrasesAsset GetPhraseCharacterAsset()
        {
            return _phrasesCharacterAsset;
        }
        public static PhrasesAsset GetPhraseBackgroundAsset()
        {
            return _phrasesBackgroundAsset;
        }
        public static PhrasesAsset GetPhraseBuysAsset()
        {
            return _phrasesBuysAsset;
        }
        #endregion

        public static void LoadAllResources()
        {
            _shopBackgroundsAsset = LoadResource<ShopImageAsset>(ShopBackgroundAssetPath);
            _shopCharactersAsset = LoadResource<ShopImageAsset>(ShopCharacterAssetPath);
            _phrasesCharacterAsset = LoadResource<PhrasesAsset>(PhrasesCharacterAssetPath);
            _phrasesBackgroundAsset = LoadResource<PhrasesAsset>(PhrasesBackgroundAssetPath);
            _phrasesBuysAsset = LoadResource<PhrasesAsset>(PhrasesBuysAssetPath);
        }

        public static T LoadResource<T>(string resourcePath) where T : UnityEngine.Object
        {
            return Resources.Load<T>(resourcePath);
        }
    }
}
