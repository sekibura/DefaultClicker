using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SekiburaGames.DefaultClicker.ShopItems
{
    [CreateAssetMenu(fileName = "CharactersAsset", menuName = "CharactersAsset")]
    public class ShopCharactersAsset : ScriptableObject
    {
        public CharacterShopItem[] CharacterItems;

        [Serializable]
        public class CharacterShopItem
        {
            public CharacterClothesVariant[] CharacterClothesVariants;
            public int CharacterPrice;
            public AudioClip VoiceClip;
        }

        [Serializable]
        public class CharacterClothesVariant
        {
            public Sprite Sprite;
            public int ClothesPrice;
        }
    }
}
