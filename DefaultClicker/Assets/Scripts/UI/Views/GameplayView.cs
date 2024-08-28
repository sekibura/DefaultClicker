using SekiburaGames.DefaultClicker.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

namespace SekiburaGames.DefaultClicker.UI
{
    public class GameplayView : View
    {
        TimerBeforeAdsYG timerBeforeAdsYG;

        public override void Initialize()
        {
            base.Initialize();
            timerBeforeAdsYG = FindAnyObjectByType<TimerBeforeAdsYG>();
        }
        public override void Show(object parameter = null)
        {
            base.Show(parameter);
            //YandexGame.timerShowAd = 0;
            timerBeforeAdsYG.ToShow = true;
        }
    }
}
