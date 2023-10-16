using Assets.Scripts;
using SekiburaGames;
using SekiburaGames.DefaultClicker.System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace SekiburaGames.DefaultClicker.Controllers
{
    public class ApplicationController : MonoBehaviour
    {
        private static bool _isLoaded = false;

        void Awake()
        {
            if (_isLoaded)
                return;

            _isLoaded = true;
            SystemManager.Register(this);
            RegisterSystems();
            SetApplicationSettings();
        }


        private void RegisterSystems()
        {
            SystemManager.Register<ClickerGameController>();
            SystemManager.Register<ScoreController>();
            SystemManager.Register<AdSystem>();
        }

        private void SetApplicationSettings()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }
    }
}