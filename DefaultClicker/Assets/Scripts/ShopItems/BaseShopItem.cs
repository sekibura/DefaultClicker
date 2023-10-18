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
        private ScoreController _scoreController;
        [SerializeField]
        protected TMP_Text _priceText;
        [SerializeField]
        protected float _price = 0;

        [SerializeField]
        protected GameObject _adIcon;

        private void Start()
        {
            Init();
        }

        protected virtual void Init()
        {
            _scoreController = SystemManager.Get<ScoreController>();
            _scoreController.ScoreUpdatedEvent += OnScoreUpdated;
            OnScoreUpdated(_scoreController.Score);
            UpdatePrice(_price);
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
                    //_priceText.gameObject.SetActive(false);
                }
                else
                {
                    _adIcon.SetActive(false);
                    //_priceText.gameObject.SetActive(true);
                }
            }
                
        }

        public virtual void Buy()
        {
            if (_adIcon != null)
            {
                if (_scoreController.Score >= _price)
                {
                    _scoreController.UpdateScore(-_price);
                    OnBuyAction();
                }
                else
                {
                    OnAdAction();
                }
            }
            else
            {
                _scoreController.UpdateScore(-_price);
                OnBuyAction();
            }
            
        }

        protected void OnBuyAction() { }
        protected void OnAdAction() 
        {
            Debug.Log("ShowAd");
        }



        protected void SetAvaiable(bool Avaiable)
        {
            //if(_adIcon == null)
                _button.interactable = Avaiable;
        }

        public void UpdatePrice(float newPrice)
        {
            _price = newPrice;
            _priceText.text = _price.ToString()+"$";
            OnScoreUpdated(_scoreController.Score);
        }
    }
}
