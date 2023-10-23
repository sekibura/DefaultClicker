using SekiburaGames.DefaultClicker.ShopItems;
using SekiburaGames.DefaultClicker.System;
using SekiburaGames.DefaultClicker.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static SekiburaGames.DefaultClicker.ShopItems.ShopBackgroundsAsset;

namespace SekiburaGames.DefaultClicker.Controllers
{
    public class ShopSystem : IInitializable
    {
        private List<BaseShopCategory> _baseShopCategories = new List<BaseShopCategory>
        {
            new BackgroundShopCategory(),
        };

        public void Initialize()
        {
            foreach (var category in _baseShopCategories) 
            {
                category.Init();
            }
        }
        public T GetShopCategory<T>() where T: BaseShopCategory
        {
            return _baseShopCategories.OfType<T>().FirstOrDefault();
        }



        public abstract class BaseShopCategory
        {
            protected int _buyIteration;                // ñêîëüêî ðàç áûë êóïëåí ýòîò àéòåì
            protected ScoreController scoreController;
            protected float nextItemPrice;
            public event Action BuyEvent;
            public event Action<bool> EnableToBuyEvent;
            public event Action<float> NextItemPriceUpdatedEvent;
            public virtual void Init()
            {
                scoreController = SystemManager.Get<ScoreController>();
                
                scoreController.ScoreUpdatedEvent += OnScoreUpdate;
                scoreController.ScorePowerUpdatedEvent += OnScorePowerUpdate;
                scoreController.ScorePerSecondUpdatedEvent += OnScorePerSecUpdate;
            }

            protected virtual void OnScoreUpdate(float newScore) { }
            protected virtual void OnScorePowerUpdate(float newScore) { }
            protected virtual void OnScorePerSecUpdate(float newScore) { }

            public abstract void Buy();

            public abstract float CalculatePrice();

            protected void InvokeBuyEvent()
            {
                BuyEvent?.Invoke();
            }

            protected void InvokeEnableToBuyEvent(bool value)
            {
                EnableToBuyEvent?.Invoke(value);
            }     
            protected void InvokeEnableToBuyEvent()
            {
                EnableToBuyEvent?.Invoke(CheckEnable());
            }
            protected void InvokeNextItemPriceUpdatedEvent(float value)
            {
                NextItemPriceUpdatedEvent?.Invoke(value);
            }
            public virtual bool CheckEnable()
            {
                return scoreController.Score >= nextItemPrice;
            }
        }

        public class BackgroundShopCategory : BaseShopCategory
        {
            private ShopBackgroundsAsset _itemBackground;
            private BackgroundShopItem _currentBackground;
            private BackgroundShopItem _nextBackground;
            private int _currentBackgroundIndex;
            private int ÑurrentBackgroundIndex
            { 
                get => _currentBackgroundIndex; 
                set 
                {
                    _currentBackgroundIndex = value;
                    _currentBackground = _itemBackground.Items.Length > _currentBackgroundIndex + 0 ? _itemBackground.Items[_currentBackgroundIndex] : null;
                    _nextBackground = _itemBackground.Items.Length > _currentBackgroundIndex + 1 ? _itemBackground.Items[_currentBackgroundIndex + 1] : null;
                    BackgroundUpdateEvent?.Invoke(_currentBackground, _nextBackground);
                } 
            }
            
            public event Action<BackgroundShopItem, BackgroundShopItem> BackgroundUpdateEvent;
            public override void Init()
            {
                base.Init();
                _itemBackground = ResourcesManager.GetShopBackgroundsAsset();
                //_currentBackgroundIndex = SystemManager.Get<SaveLoadController>().Load().BackGroundIndex; 
                ÑurrentBackgroundIndex = 0;
                _buyIteration = 1;
                CalculatePrice();
                InvokeEnableToBuyEvent();
            }
            public override void Buy()
            {
                if(_nextBackground != null)
                {
                    if (scoreController.Score >= nextItemPrice)
                    {
                        _buyIteration++;
                        scoreController.UpdateScore(-nextItemPrice);
                        ÑurrentBackgroundIndex++;
                        CalculatePrice();
                        InvokeBuyEvent();
                        InvokeEnableToBuyEvent();
                    }
                }
            }

            public override float CalculatePrice()
            {
                if (_nextBackground == null)
                {
                    nextItemPrice = 0;
                    InvokeNextItemPriceUpdatedEvent(nextItemPrice);
                    return nextItemPrice;
                }
                   

                float ScorePower = scoreController.ScorePower > 0 ? scoreController.ScorePower : 1;
                float ScorePerSecond = scoreController.ScorePerSecond > 0 ? scoreController.ScorePerSecond : 1;

                if (_itemBackground.Items.Length > ÑurrentBackgroundIndex)
                    nextItemPrice = _buyIteration * 2 * ScorePower * ScorePerSecond; // expression

                else
                    nextItemPrice = 0;

                InvokeNextItemPriceUpdatedEvent(nextItemPrice);
                return nextItemPrice;
            }

            protected override void OnScorePowerUpdate(float newScore)
            {
                CalculatePrice();
            }

            protected override void OnScorePerSecUpdate(float newScore)
            {
                CalculatePrice();
            }

            protected override void OnScoreUpdate(float newScore)
            {
                InvokeEnableToBuyEvent();
            }

            public override bool CheckEnable()
            {
                if (_nextBackground != null)
                    return scoreController.Score >= nextItemPrice;
                else
                    return false;
            }
        }
    }
}
