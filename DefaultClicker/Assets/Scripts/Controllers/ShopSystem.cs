using SekiburaGames.DefaultClicker.ShopItems;
using SekiburaGames.DefaultClicker.System;
using SekiburaGames.DefaultClicker.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
//using YG;
using static SekiburaGames.DefaultClicker.ShopItems.ShopImageAsset;

namespace SekiburaGames.DefaultClicker.Controllers
{
    public class ShopSystem : IInitializable
    {
        private List<BaseShopCategory> _baseShopCategories = new List<BaseShopCategory>
        {
            new ImageShopCategory("BackgroundsAsset", 1),
            new CharacterShopCategory("CharacterAsset", 2),
            new ClickPowerShopCategory(),
            new ScorePerSecShopCategory(),
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

        public IEnumerable<T> GetShopCategories<T>() where T : BaseShopCategory
        {
            return _baseShopCategories.OfType<T>();
        }

        public abstract class BaseShopCategory
        {
            protected int _buyIteration;                // ñêîëüêî ðàç áûë êóïëåí ýòîò àéòåì
            protected ScoreController scoreController;
            protected SaveLoadController saveLoadController;
            protected double nextItemPrice;
            public event Action BuyEvent;
            public event Action<bool> EnableToBuyEvent;
            public event Action<double> NextItemPriceUpdatedEvent;
            public virtual void Init()
            {
                scoreController = SystemManager.Get<ScoreController>();
                saveLoadController = SystemManager.Get<SaveLoadController>();
                
                scoreController.ScoreUpdatedEvent += OnScoreUpdate;
                scoreController.ScorePowerUpdatedEvent += OnScorePowerUpdate;
                scoreController.ScorePerSecondUpdatedEvent += OnScorePerSecUpdate;
            }

            protected virtual void OnScoreUpdate(double newScore) { }
            protected virtual void OnScorePowerUpdate(double newScore) { }
            protected virtual void OnScorePerSecUpdate(double newScore) { }

            public abstract void Buy(object obj = null);

            public abstract double CalculatePrice();

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
            protected void InvokeNextItemPriceUpdatedEvent(double value)
            {
                NextItemPriceUpdatedEvent?.Invoke(value);
            }
            public virtual bool CheckEnable()
            {
                return scoreController.Score >= nextItemPrice;
            }

            public abstract void SaveProgress();
        }

        public class ImageShopCategory: BaseShopCategory
        {
            protected ShopImageAsset _imagesItem;
            protected ImageShopItem _currentImage;
            protected ImageShopItem _nextImage;
            protected int _currentImageIndex;
            protected bool _allOpened;
            public string  AssetName { get; private set; }
            private float _priceMultipler;
            protected int ÑurrentImageIndex
            { 
                get => _currentImageIndex; 
                set 
                {
                    _currentImageIndex = value;
                    _currentImage = _imagesItem.Items.Length > _currentImageIndex + 0 ? _imagesItem.Items[_currentImageIndex] : null;
                    _nextImage = _imagesItem.Items.Length > _currentImageIndex + 1 ? _imagesItem.Items[_currentImageIndex + 1] : null;
                    ImageUpdateEvent?.Invoke(_currentImage, _nextImage);
                } 
            }
            
            public event Action<ImageShopItem, ImageShopItem> ImageUpdateEvent;

            public ImageShopItem GetCurrentImage()
            {
                return _currentImage;
            }

            public ImageShopCategory(string assetName, float priceMultipler)
            {
                AssetName = assetName;
                _priceMultipler = priceMultipler;
            }

            public override void Init()
            {
                base.Init();
                _imagesItem = ResourcesManager.GetAssetByName(AssetName);
                //saveLoadController.LoadEvent += (x) => InitOnLoad();
                //if (YandexGame.SDKEnabled == true)
                //{ 
                //        InitOnLoad();
                //}
            }

            private void InitOnLoad()
            {
                Debug.Log($"InitOnLoad");
                ÑurrentImageIndex = LoadCurrentImageIndex();
                Debug.Log($"Loaded index: {ÑurrentImageIndex}");
                _allOpened = LoadOpenedImageIndex() >= _imagesItem.Items.Length - 1;
                //ÑurrentBackgroundIndex = 0;
                _buyIteration = ÑurrentImageIndex + 1;
                CalculatePrice();
                InvokeEnableToBuyEvent();
            }
            public override void Buy(object obj = null)
            {
                if(_nextImage != null)
                {
                    if (scoreController.Score >= nextItemPrice)
                    {
                        _buyIteration++;
                        scoreController.UpdateScore(-nextItemPrice);
                        ÑurrentImageIndex++;
                        CalculatePrice();
                        InvokeBuyEvent();
                        InvokeEnableToBuyEvent();
                        SaveProgress();
                    }
                }
            }

            public override double CalculatePrice()
            {
                if (_nextImage == null)
                {
                    nextItemPrice = 0;
                    InvokeNextItemPriceUpdatedEvent(nextItemPrice);
                    return nextItemPrice;
                }
                   

                float ScorePower = scoreController.ScorePower > 0 ? scoreController.ScorePower : 1;
                float ScorePerSecond = scoreController.ScorePerSecond > 0 ? scoreController.ScorePerSecond : 1;

                if (_imagesItem.Items.Length > ÑurrentImageIndex)
                {
                    //nextItemPrice = _buyIteration * _buyIteration * _buyIteration * 2 * ScorePower * ScorePower * ScorePerSecond * ScorePerSecond * _priceMultipler; // expression
                    nextItemPrice = PriceFunctions.CalcBackgroundPrice(_buyIteration);
                }

                else
                    nextItemPrice = 0;

                InvokeNextItemPriceUpdatedEvent(nextItemPrice);
                return nextItemPrice;
            }

            protected override void OnScorePowerUpdate(double newScore)
            {
                CalculatePrice();
            }

            protected override void OnScorePerSecUpdate(double newScore)
            {
                CalculatePrice();
            }

            protected override void OnScoreUpdate(double newScore)
            {
                InvokeEnableToBuyEvent();
            }

            public override bool CheckEnable()
            {
                if (_nextImage != null)
                    return scoreController.Score >= nextItemPrice;
                else
                    return false;
            }

            private int LoadCurrentImageIndex()
            {
                int index = 0;
                switch (AssetName)
                {
                    case "BackgroundsAsset":
                        //index = saveLoadController.Load().CurrentBackGroundIndex;
                        break;
                    case "CharacterAsset":
                        //index = saveLoadController.Load().CurrentCharacterIndex;
                        break;
                    default:
                        Debug.LogError("ShopSystem. Error LoadImageIndex - Wrong AssetName value!");
                        break;
                }
                return index;
            }

            private int LoadOpenedImageIndex()
            {
                int index = 0;
                switch (AssetName)
                {
                    case "BackgroundsAsset":
                        //index = saveLoadController.Load().OpenedBackGroundIndex;
                        break;
                    case "CharacterAsset":
                        //index = saveLoadController.Load().OpenedCharacterIndex;
                        break;
                    default:
                        Debug.LogError("ShopSystem. Error GetOpenedImageIndex - Wrong AssetName value!");
                        break;
                }
                return index;
            }

            public override void SaveProgress()
            {
                //var savesYG = saveLoadController.Load();
                if (_allOpened)
                {
                    switch (AssetName)
                    {
                        case "BackgroundsAsset":
                            {
                                //savesYG.OpenedBackGroundIndex = _imagesItem.Items.Length - 1;
                                //savesYG.CurrentBackGroundIndex = ÑurrentImageIndex;
                            }
                            break;
                        case "CharacterAsset":
                            {
                                //savesYG.OpenedCharacterIndex = _imagesItem.Items.Length - 1;
                                //savesYG.CurrentCharacterIndex = ÑurrentImageIndex;
                            }
                            break;
                        default:
                            Debug.LogError("ShopSystem. Error SaveProgress - Wrong AssetName value!");
                            break;
                    }
                   
                }
                else
                {
                    switch (AssetName)
                    {
                        case "BackgroundsAsset":
                            {
                                //savesYG.OpenedBackGroundIndex = _currentImageIndex;
                                //savesYG.CurrentBackGroundIndex = ÑurrentImageIndex;
                            }
                            break;
                        case "CharacterAsset":
                            {
                                //savesYG.OpenedCharacterIndex = _currentImageIndex;
                                //savesYG.CurrentCharacterIndex = ÑurrentImageIndex;
                            }
                            break;
                        default:
                            Debug.LogError("ShopSystem. Error SaveProgress - Wrong AssetName value!");
                            break;
                    }
                }
                //saveLoadController.Save(savesYG);
            }
        }
        public void Dispose()
        {
            foreach (var shopCat in _baseShopCategories)
            {
                shopCat.SaveProgress();
            }
        }
        public class CharacterShopCategory : ImageShopCategory
        {
            public CharacterShopCategory(string assetName, float priceMultipler) : base(assetName, priceMultipler)
            {
            }

            public override double CalculatePrice()
            {
                if (_nextImage == null)
                {
                    nextItemPrice = 0;
                    InvokeNextItemPriceUpdatedEvent(nextItemPrice);
                    return nextItemPrice;
                }


                float ScorePower = scoreController.ScorePower > 0 ? scoreController.ScorePower : 1;
                float ScorePerSecond = scoreController.ScorePerSecond > 0 ? scoreController.ScorePerSecond : 1;

                if (_imagesItem.Items.Length > ÑurrentImageIndex)
                {
                    nextItemPrice = PriceFunctions.CalcPriceCharacter(_buyIteration);
                }

                else
                    nextItemPrice = 0;

                InvokeNextItemPriceUpdatedEvent(nextItemPrice);
                return nextItemPrice;
            }
        }

        public class ClickPowerShopCategory : BaseShopCategory
        {
            public event Action<float> ClickPowerUpdateEvent;
            private float _defaultPrice = 10;

            public override void Init()
            {
                base.Init();
                base.Init();
                //saveLoadController.LoadEvent += (x) => InitOnLoad();
                //if (YandexGame.SDKEnabled == true)
                //{
                //    InitOnLoad();
                //}
            }

            private void InitOnLoad()
            {
                Debug.Log($"InitOnLoad");
                LoadClickPower();
                LoadIndex();


                CalculatePrice();
                InvokeEnableToBuyEvent();
            }
            public override void Buy(object obj = null)
            {
                float delta = 0;
                if (obj != null)
                    delta = (float)obj;
                if (scoreController.Score >= nextItemPrice)
                {
                    scoreController.UpdateScore(-nextItemPrice);
                    scoreController.UpdateScorePower(delta);
                    _buyIteration++;
                    CalculatePrice();
                    InvokeBuyEvent();
                    InvokeEnableToBuyEvent();
                    SaveProgress();
                }
            }

            public override double CalculatePrice()
            {
                if (_buyIteration != 0)
                    nextItemPrice = PriceFunctions.CalcPriceClickScore(_buyIteration);
                //nextItemPrice = _buyIteration * 2 * _buyIteration * _buyIteration * _buyIteration * 10; // expression 
                else
                    nextItemPrice = PriceFunctions.BasePriceClickScore;

                InvokeNextItemPriceUpdatedEvent(nextItemPrice);
                return nextItemPrice;
            }

            public override void SaveProgress()
            {
                //var savesYG = saveLoadController.Load();
                //savesYG.ScorePower = scoreController.ScorePower;
                //savesYG.ScorePowerIndex = _buyIteration;
                //saveLoadController.Save(savesYG);
            }

            protected override void OnScoreUpdate(double newScore)
            {
                InvokeEnableToBuyEvent();
            }

            private int LoadIndex()
            {
                //int value = saveLoadController.Load().ScorePowerIndex;
                //_buyIteration = value;
                //return value;
                return 0;
            }

            private float LoadClickPower()
            {
                //    float value = saveLoadController.Load().ScorePower;
                //scoreController.SetScorePower(value);
                //return value;
                return 0;
            }
        }

        public class ScorePerSecShopCategory : BaseShopCategory
        {
            public event Action<float> ScorePerSecUpdateEvent;
            //private float _defaultPrice = 20;

            public override void Init()
            {
                //base.Init();
                //base.Init();
                //saveLoadController.LoadEvent += (x) => InitOnLoad();
                //if (YandexGame.SDKEnabled == true)
                //{
                //    InitOnLoad();
                //}
            }

            private void InitOnLoad()
            {
                LoadScorePerSec();
                LoadIndex();


                CalculatePrice();
                InvokeEnableToBuyEvent();
            }
            public override void Buy(object obj = null)
            {
                float delta = 0;
                if (obj != null)
                    delta = (float)obj;
                if (scoreController.Score >= nextItemPrice)
                {
                    scoreController.UpdateScore(-nextItemPrice);
                    scoreController.UpdateScorePerSecond(delta);
                    _buyIteration++;
                    CalculatePrice();
                    InvokeBuyEvent();
                    InvokeEnableToBuyEvent();
                    SaveProgress();
                }
            }

            public override double CalculatePrice()
            {
                if (_buyIteration != 0)
                    nextItemPrice = PriceFunctions.CalcPricePerSec(_buyIteration);
                //nextItemPrice = _defaultPrice +  _buyIteration * 2 * _buyIteration * _buyIteration * _buyIteration * 10; // expression 
                else
                    nextItemPrice = PriceFunctions.BasePricePerSec;

                InvokeNextItemPriceUpdatedEvent(nextItemPrice);
                return nextItemPrice;
            }

            public override void SaveProgress()
            {
                //var savesYG = saveLoadController.Load();
                //savesYG.ScorePerSecond = scoreController.ScorePerSecond;
                //savesYG.ScorePerSecondIndex = _buyIteration;
                //saveLoadController.Save(savesYG);
            }

            protected override void OnScoreUpdate(double newScore)
            {
                InvokeEnableToBuyEvent();
            }

            private int LoadIndex()
            {
                //int value = saveLoadController.Load().ScorePerSecondIndex;
                //_buyIteration = value;
                //return value;
                return 0;
            }

            private float LoadScorePerSec()
            {
                //float value = saveLoadController.Load().ScorePerSecond;
                //scoreController.SetScorePerSec(value);
                //return value;
                return 0;
            }
        }
    }
}
