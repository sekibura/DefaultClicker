using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SekiburaGames.DefaultClicker.ShopItems
{
    [CreateAssetMenu(fileName = "BackgroundsAsset", menuName = "BackgroundsAsset")]
    public class ShopBackgroundsAsset : ScriptableObject
    {
        public BackgroundShopItem[] Items;


        [Serializable]
        public class BackgroundShopItem
        {
            public Sprite Sprite;
            public int Price;
            public AudioClip VoiceClip;
        }
    }
}
