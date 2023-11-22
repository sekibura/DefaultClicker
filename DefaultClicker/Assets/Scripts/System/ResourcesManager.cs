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
        #endregion

        #region Private fields
        private static ShopImageAsset _shopBackgroundsAsset;
        private static ShopImageAsset _shopCharactersAsset;
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
        #endregion

        public static void LoadAllResources()
        {
            _shopBackgroundsAsset = LoadResource<ShopImageAsset>(ShopBackgroundAssetPath);
            _shopCharactersAsset = LoadResource<ShopImageAsset>(ShopCharacterAssetPath);
        }

        public static T LoadResource<T>(string resourcePath) where T : UnityEngine.Object
        {
            return Resources.Load<T>(resourcePath);
        }
    }
}
