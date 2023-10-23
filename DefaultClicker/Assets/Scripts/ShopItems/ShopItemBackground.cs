using SekiburaGames.DefaultClicker.ShopItems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SekiburaGames.DefaultClicker.Controllers.ShopSystem;
using static SekiburaGames.DefaultClicker.ShopItems.ShopBackgroundsAsset;

namespace SekiburaGames.DefaultClicker.UI
{
    public class ShopItemBackground : BaseShopItem
    {
        private BackgroundShopCategory _backgroundShopCategory;
        [SerializeField]
        private Image _backgroundImage;


        protected override void Init()
        {
            base.Init();
            _backgroundShopCategory = _shopSystem.GetShopCategory<BackgroundShopCategory>();
            _buyButton.onClick.AddListener(()=> _backgroundShopCategory.Buy());
            _backgroundShopCategory.EnableToBuyEvent += SetAvaiable;
            _backgroundShopCategory.BackgroundUpdateEvent += OnUpdateBackground;
            _backgroundShopCategory.NextItemPriceUpdatedEvent += OnPriceUpdate;
            _backgroundShopCategory.CalculatePrice();
            SetAvaiable(_backgroundShopCategory.CheckEnable());
        }

        private void OnUpdateBackground(BackgroundShopItem current, BackgroundShopItem next)
        {
            _backgroundImage.sprite = current.Sprite;
        }

        private void OnPriceUpdate(float nextBGPrice)
        {
            _priceText.text = nextBGPrice.ToString()+"$";
        }
    }
}
