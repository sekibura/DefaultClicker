using SekiburaGames.DefaultClicker.ShopItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SekiburaGames.DefaultClicker.UI
{
    public class UI_ShopPanel: MonoBehaviour
    {
        [SerializeField]
        private ShopBackgroundsAsset _shopBackgroundsAsset;
        [SerializeField]
        private ShopCharactersAsset _shopCharactersAsset;

        [SerializeField]
        private BaseShopItem _bgShopItem;
        [SerializeField]
        private BaseShopItem _charShopItem;
        [SerializeField]
        private BaseShopItem _outfitShopItem;
        [SerializeField]
        private BaseShopItem _scorePerSecShopItem;
        [SerializeField]
        private BaseShopItem _powerClickShopItem;
        [SerializeField]
        private BaseShopItem _adPowerClickShopItem;
    }
}
