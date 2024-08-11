using SekiburaGames.DefaultClicker.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SekiburaGames.DefaultClicker.Controllers.ShopSystem;
using YG;

namespace SekiburaGames.DefaultClicker.UI
{
    public class ShopItemPowerClick : BaseShopItem
    {
        private ClickPowerShopCategory _clickPowerShopCategory;
        [SerializeField]
        private float _deltaScorePower = 1; 
        protected override void Init()
        {
            base.Init();
            _clickPowerShopCategory = _shopSystem.GetShopCategory<ClickPowerShopCategory>();

            if(_buyButton!=null)
                _buyButton.onClick.AddListener(() => _clickPowerShopCategory.Buy(_deltaScorePower));

            _clickPowerShopCategory.EnableToBuyEvent += SetAvaiable;
            _clickPowerShopCategory.ClickPowerUpdateEvent += OnClickPowerUpdateEvent;
            _clickPowerShopCategory.NextItemPriceUpdatedEvent += OnPriceUpdate;
            _clickPowerShopCategory.CalculatePrice();
            SetAvaiable(_clickPowerShopCategory.CheckEnable());
        }

        private void OnClickPowerUpdateEvent(float value)
        {
            
        }

        private void OnPriceUpdate(float nextBGPrice)
        {
            if (_priceText != null)
                _priceText.text = nextBGPrice.ToString() + "$";
        }
    }
}
