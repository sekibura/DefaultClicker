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
        #endregion

        #region Private fields
        private static ShopBackgroundsAsset _shopBackgroundsAsset;
        #endregion

        #region Getters 
        public static ShopBackgroundsAsset GetShopBackgroundsAsset()
        {
            return _shopBackgroundsAsset;
        }
        #endregion

        public static void LoadAllResources()
        {
            _shopBackgroundsAsset = LoadResource<ShopBackgroundsAsset>(ShopBackgroundAssetPath);
        }

        public static T LoadResource<T>(string resourcePath) where T : UnityEngine.Object
        {
            return Resources.Load<T>(resourcePath);
        }
    }
}
