using SekiburaGames.DefaultClicker.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SekiburaGames.DefaultClicker.Controllers.ShopSystem;
using YG;

namespace SekiburaGames.DefaultClicker.UI
{
    public class ShopItemADPowerClick : BaseADShopItem
    {
        private ClickPowerShopCategory _clickPowerShopCategory;
        [SerializeField]
        private float _deltaScorePower = 1;
        protected override void Init()
        {
            base.Init();
            _clickPowerShopCategory = _shopSystem.GetShopCategory<ClickPowerShopCategory>();

            if (_adButton != null)
                _adButton.onClick.AddListener(() => OnAdClick());

            //_clickPowerShopCategory.EnableToBuyEvent += SetAvaiable;
            //_clickPowerShopCategory.ClickPowerUpdateEvent += OnClickPowerUpdateEvent;
            //_clickPowerShopCategory.NextItemPriceUpdatedEvent += OnPriceUpdate;
            //_clickPowerShopCategory.CalculatePrice();
            //SetAvaiable(_clickPowerShopCategory.CheckEnable());
        }

        private void OnAdClick()
        {
            /// TODO
            /// Show AD
            Debug.Log("OnAdClick");
            YandexGame.RewVideoShow(0);
            DisableButtonForSeconds();
        }
    }
}
