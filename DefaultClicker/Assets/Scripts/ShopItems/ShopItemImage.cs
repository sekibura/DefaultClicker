using SekiburaGames.DefaultClicker.ShopItems;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static SekiburaGames.DefaultClicker.Controllers.ShopSystem;
using static SekiburaGames.DefaultClicker.ShopItems.ShopImageAsset;

namespace SekiburaGames.DefaultClicker.UI
{
    public class ShopItemImage : BaseShopItem
    {
        private ImageShopCategory _imageShopCategory;
        [SerializeField]
        private Image _image;
        [SerializeField]
        private string ImageAssetName;


        protected override void Init()
        {
            base.Init();
            _imageShopCategory = _shopSystem.GetShopCategories<ImageShopCategory>().Where(x => x.AssetName == ImageAssetName).FirstOrDefault();
            _buyButton.onClick.AddListener(()=> _imageShopCategory.Buy());
            _imageShopCategory.EnableToBuyEvent += SetAvaiable;
            _imageShopCategory.ImageUpdateEvent += OnUpdateBackground;
            _imageShopCategory.NextItemPriceUpdatedEvent += OnPriceUpdate;
            _imageShopCategory.CalculatePrice();
            SetAvaiable(_imageShopCategory.CheckEnable());
            OnUpdateBackground(_imageShopCategory.GetCurrentImage(), null);
            
        }

        private void OnUpdateBackground(ImageShopItem current, ImageShopItem next)
        {
            _image.sprite = current.Sprite;
        }

        private void OnPriceUpdate(double nextBGPrice)
        {
            _priceText.text = nextBGPrice.ToString()+"$";
        }
    }
}
