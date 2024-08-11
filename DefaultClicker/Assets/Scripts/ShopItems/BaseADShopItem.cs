using Assets.Scripts;
using SekiburaGames.DefaultClicker.Controllers;
using SekiburaGames.DefaultClicker.System;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SekiburaGames.DefaultClicker.UI
{
    public class BaseADShopItem : MonoBehaviour
    {
        //[SerializeField]
        //protected Button _buyButton;
        [SerializeField]
        protected Button _adButton;
        [SerializeField]
        protected Image _adButtonDisableMask;
        [SerializeField]
        protected float _timeForDisabled = 20;
        protected ShopSystem _shopSystem;

        private void Start()
        {
            Init();
        }

        protected virtual void Init()
        {
            _shopSystem = SystemManager.Get<ShopSystem>();
            if (_adButtonDisableMask != null)
                _adButtonDisableMask.fillAmount = 0;
        }

        protected virtual void SetAvaiableAD(bool Avaiable)
        {
            if (_adButton != null)
                _adButton.interactable = Avaiable;
        }

        protected void DisableButtonForSeconds()
        {
            SetAvaiableAD(false);
            _adButtonDisableMask.fillAmount = 1;
            StartCoroutine(ChangeFillAmount());
        }

        private IEnumerator ChangeFillAmount()
        {
            float amoutStep = 1f / 360;
            float timeStep = _timeForDisabled / 360;
            while (_adButtonDisableMask.fillAmount > 0)
            {
                _adButtonDisableMask.fillAmount -= amoutStep;
                yield return new WaitForSeconds(timeStep);
            }
            SetAvaiableAD(true);
        }
    }
}
