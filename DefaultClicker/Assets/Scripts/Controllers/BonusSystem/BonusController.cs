using PimDeWitte.UnityMainThreadDispatcher;
using SekiburaGames.DefaultClicker.System;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using YG;

namespace SekiburaGames.DefaultClicker.Controllers
{
    public class BonusController : IInitializable
    {
        public Action<Type, float, object> OnBonusAdded;
        public Action<Type> OnBonusRemoved;


        private List<BaseBonus> _bonuses = new List<BaseBonus>();

        public void Initialize()
        {
            // ������������� �� ������� �������� ������� � OnEnable
            //YandexGame.RewardVideoEvent += Rewarded;
        }

        public void AddBonus<T>(float duration, object o = null) where T : BaseBonus, new()
        {
            var newBonus = new T();
            _bonuses.Add(newBonus);
            OnBonusAdded?.Invoke(typeof(T), duration, o);
            newBonus.timerData = TimersController.Instance.StartTimer(() => RemoveBonus(newBonus), duration, false);
            newBonus.Apply(o);
        }

        private void RemoveBonus(BaseBonus baseBonus)
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                TimersController.Instance.StopTimer(baseBonus.timerData);

                baseBonus.RemoveBonus();
                _bonuses.Remove(baseBonus);
                OnBonusRemoved?.Invoke(baseBonus.GetType());
            });
        }

        // ����������� ����� ��������� �������
        // 0 - ��������� ������ ClickScoreBonus
        // 1 - ��������� ������ ScorePerSecond
        void Rewarded(int id)
        {
            switch (id)
            {
               case (int)ClickerConstants.AdTypes.PowerClick:
                    AddBonus<ClickScoreBonus>(10, (object)10f);
                    break;
                case (int)ClickerConstants.AdTypes.ScorePerSec:
                    AddBonus<ScorePerSecondBonus>(10, (object)10f);
                    break;
            }
        }

        public void Dispose()
        {
            for (int i = 0; i < _bonuses.Count; i++)
            {
                RemoveBonus(_bonuses[i]);
            }

            // ������������ �� ������� �������� ������� � OnDisable
            //YandexGame.RewardVideoEvent -= Rewarded;
        }
    }
}
