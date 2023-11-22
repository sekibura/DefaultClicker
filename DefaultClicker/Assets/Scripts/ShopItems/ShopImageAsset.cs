using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SekiburaGames.DefaultClicker.ShopItems
{
    [CreateAssetMenu(fileName = "BackgroundsAsset", menuName = "BackgroundsAsset")]
    public class ShopImageAsset : ScriptableObject
    {
        public ImageShopItem[] Items;


        [Serializable]
        public class ImageShopItem
        {
            public Sprite Sprite;
            //public int Price;
            public AudioClip VoiceClip;
        }
    }
}
