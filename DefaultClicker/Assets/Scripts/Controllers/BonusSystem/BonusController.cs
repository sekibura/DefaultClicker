using PimDeWitte.UnityMainThreadDispatcher;
using SekiburaGames.DefaultClicker.System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using YG;
using YG.Example;

namespace SekiburaGames.DefaultClicker.Controllers
{
    public class BonusController : IInitializable
    {
        public Action<Type, float, object> OnBonusAdded;
        public Action<Type> OnBonusRemoved;
        private Timer _timer;


        private List<BaseBonus> _bonuses = new List<BaseBonus>();

        public void Initialize()
        {
            // Подписываемся на событие открытия рекламы в OnEnable
            YandexGame.RewardVideoEvent += Rewarded;
        }

        public void AddBonus<T>(float duration, object o = null) where T : BaseBonus, new()
        {
            var newBonus = new T();
            _bonuses.Add(newBonus);
            OnBonusAdded?.Invoke(typeof(T), duration, o);
            _timer = new Timer(duration * 1000);
            _timer.AutoReset = true;
            _timer.Elapsed += (sender, e) => RemoveBonus(newBonus, _timer);
            _timer.Start(); 
            newBonus.Apply(o);
            Debug.Log($"Add bonus");
        }

        private void RemoveBonus(BaseBonus baseBonus, Timer timer)
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
               
            
                Debug.Log($"Remove bonus ");
                if (timer != null)
                {
                    timer.Stop();
                    timer.Dispose();
                }
            
                baseBonus.RemoveBonus();
                _bonuses.Remove(baseBonus);
                OnBonusRemoved?.Invoke(baseBonus.GetType());
            });
        }

        // Подписанный метод получения награды
        // 0 - Получение бонуса ClickScoreBonus
        void Rewarded(int id)
        {
            switch (id)
            {
               case 0:
                    AddBonus<ClickScoreBonus>(5, (object)10f);
                    break;
            }
        }

        public void Dispose()
        {
            for (int i = 0; i < _bonuses.Count; i++)
            {
                RemoveBonus(_bonuses[i], null);
            }
      
            // Отписываемся от события открытия рекламы в OnDisable
            YandexGame.RewardVideoEvent -= Rewarded;
        }


    }
}
