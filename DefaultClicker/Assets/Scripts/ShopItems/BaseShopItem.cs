﻿using Assets.Scripts;
using SekiburaGames.DefaultClicker.Controllers;
using SekiburaGames.DefaultClicker.System;
using System;
using System.Collections;
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
        protected Image _adButtonDisableMask;
        [SerializeField]
        protected float _timeForDisabled = 20;
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

        protected virtual void SetAvaiable(bool Avaiable)
        {
            if (_buyButton != null)
                _buyButton.interactable = Avaiable;
        }

        protected void DisableButtonForSeconds()
        {
            _adButtonDisableMask.fillAmount = 1;
            StartCoroutine(ChangeFillAmount());
        }

        private IEnumerator ChangeFillAmount()
        {
            float amoutStep = 1f/360;
            float timeStep = _timeForDisabled / 360;
            while (_adButtonDisableMask.fillAmount > 0)
            {
                _adButtonDisableMask.fillAmount -= amoutStep;
                yield return new WaitForSeconds(timeStep);
            }
        }
    }
}
