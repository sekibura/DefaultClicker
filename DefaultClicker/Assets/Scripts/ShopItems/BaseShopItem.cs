using Assets.Scripts;
using SekiburaGames.DefaultClicker.System;
using SekiburaGames.DefaultClicker.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

namespace SekiburaGames.DefaultClicker.ShopItems
{
    public class BaseShopItem: MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        [SerializeField]
        private Button _adButton;

        private ScoreController _scoreController;
        [SerializeField]
        protected TMP_Text _priceText;
        [SerializeField]
        protected float _price = 10;

        public float Price
        {
            get { return _price; }
            set 
            { 
                _price = value;
                _priceText.text = _price.ToString() + "$";
                OnScoreUpdated(_scoreController.Score);
            }
        }
        [SerializeField]
        protected GameObject _adIcon;        
        public Action<ClickType> ClickAction;

        private void Start()
        {
            Init();
        }

        protected virtual void Init()
        {
            _scoreController = SystemManager.Get<ScoreController>();
            _scoreController.ScoreUpdatedEvent += OnScoreUpdated;
            _button.onClick.AddListener(() => { ClickAction?.Invoke(ClickType.DefaultClick);});
            if(_adButton!=null)
                _adButton.onClick.AddListener(() => { ClickAction?.Invoke(ClickType.AdClick);});
            OnScoreUpdated(_scoreController.Score);
            Price = Price;
        }

        private void OnScoreUpdated(float Score)
        {
            SetAvaiable(Score >= _price);
            SetAvaiableAD();
        }

        private void SetAvaiableAD()
        {
            if(_adIcon != null)
            {
                if (_scoreController.Score < _price)
                {
                    _adIcon.SetActive(true);
                    //AdMode = true;
                }
                else
                {
                    _adIcon.SetActive(false);
                    //AdMode = true;
                }
            }
        }

        //public virtual void Buy()
        //{
        //    if (_adIcon != null)
        //    {
        //        if (_scoreController.Score >= _price)
        //        {
        //            _scoreController.UpdateScore(-_price);
        //            OnBuyAction();
        //        }
        //        else
        //        {
        //            OnAdAction();
        //        }
        //    }
        //    else
        //    {
        //        _scoreController.UpdateScore(-_price);
        //        OnBuyAction();
        //    }
            
        //}

        //protected void OnBuyAction() { }
        //protected void OnAdAction() 
        //{
        //    ClickAction?.Invoke(ClickType.AdClick);
        //}

        protected void SetAvaiable(bool Avaiable)
        {
            _button.interactable = Avaiable;
        }

        //public void UpdatePrice(float newPrice)
        //{
        //    _price = newPrice;
        //    _priceText.text = _price.ToString()+"$";
        //    OnScoreUpdated(_scoreController.Score);
        //}

        public enum ClickType
        {
            AdClick,
            DefaultClick
        }
    }
}
