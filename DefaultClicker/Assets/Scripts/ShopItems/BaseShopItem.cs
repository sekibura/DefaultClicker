using Assets.Scripts;
using SekiburaGames.DefaultClicker.System;
using SekiburaGames.DefaultClicker.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace SekiburaGames.DefaultClicker.ShopItems
{
    public abstract class BaseShopItem: MonoBehaviour
    {
        private ScoreController _scoreController;

        [SerializeField]
        private float _price = 0;

        private void Start()
        {
            Init();
        }

        protected virtual void Init()
        {
            _scoreController = SystemManager.Get<ScoreController>();
            _scoreController.ScoreUpdatedEvent += OnScoreUpdated;
            OnScoreUpdated(_scoreController.Score);
        }

        private void OnScoreUpdated(float Score)
        {
            SetAvaiable(Score >= _price);
        }

        public virtual void Buy()
        {
            _scoreController.UpdateScore(-_price);
            OnBuyAction();
        }

        protected abstract void OnBuyAction();

        protected abstract void SetAvaiable(bool Avaiable);
    }
}
