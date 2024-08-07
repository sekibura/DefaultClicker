using Assets.Scripts;
using SekiburaGames.DefaultClicker.Controllers;
using SekiburaGames.DefaultClicker.System;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SekiburaGames.DefaultClicker.UI
{
    public class BaseShopItem: MonoBehaviour
    {
        [SerializeField]
        protected Button _buyButton; 
        [SerializeField]
        protected Button _adButton;
        [SerializeField]
        protected TMP_Text _priceText;

        protected ShopSystem _shopSystem;
        


        private void Start()
        {
            Init();
        }

        protected virtual void Init()
        {
            _shopSystem = SystemManager.Get<ShopSystem>();
        }

        protected virtual void SetAvaiableAD(bool Avaiable)
        {
            if(_adButton!=null)
                _adButton.interactable = Avaiable;
        }

        protected virtual void SetAvaiable(bool Avaiable)
        {
            if (_buyButton != null)
                _buyButton.interactable = Avaiable;
            SetAvaiableAD(!Avaiable);
        }
    }
}
