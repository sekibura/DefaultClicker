using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SekiburaGames.DefaultClicker.Controllers.ShopSystem;
using YG;

namespace SekiburaGames.DefaultClicker.UI
{
    public class ShopItemScorePerSec : BaseShopItem
    {
        private ScorePerSecShopCategory _scorePerSecondCat;
        [SerializeField]
        private float _deltaScorePerSecond = 1;
        protected override void Init()
        {
            base.Init();
            _scorePerSecondCat = _shopSystem.GetShopCategory<ScorePerSecShopCategory>();

            if(_buyButton!=null)
                _buyButton.onClick.AddListener(() => _scorePerSecondCat.Buy(_deltaScorePerSecond));

            _scorePerSecondCat.EnableToBuyEvent += SetAvaiable;
            _scorePerSecondCat.ScorePerSecUpdateEvent += OnClickPowerUpdateEvent;
            _scorePerSecondCat.NextItemPriceUpdatedEvent += OnPriceUpdate;
            _scorePerSecondCat.CalculatePrice();
            SetAvaiable(_scorePerSecondCat.CheckEnable());
        }

        private void OnClickPowerUpdateEvent(float value)
        {

        }

        private void OnPriceUpdate(double nextBGPrice)
        {
            if (_priceText != null)
                _priceText.text = nextBGPrice.ToString() + "$";
        }
    }
}
