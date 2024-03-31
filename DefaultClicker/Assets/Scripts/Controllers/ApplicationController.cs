using Assets.Scripts;
using SekiburaGames;
using SekiburaGames.DefaultClicker.System;
using SekiburaGames.DefaultClicker.UI;
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
            ResourcesManager.LoadAllResources();
            SystemManager.Register(this);
            RegisterSystems();
            GetSystems();
            SetApplicationSettings();
            GameStateManager.Instance.UpdateGameState(GameStateManager.GameState.InGame);
        }


        private void RegisterSystems()
        {
            SystemManager.Register<SaveLoadController>();
            SystemManager.Register<ClickerGameController>();
            SystemManager.Register<ScoreController>();
            SystemManager.Register<AdSystem>();
            SystemManager.Register<ShopSystem>();
            
        }

        private void GetSystems()
        {
            SystemManager.Get<SaveLoadController>();
            SystemManager.Get<ClickerGameController>();
            SystemManager.Get<ScoreController>();
            SystemManager.Get<AdSystem>();
            SystemManager.Get<ShopSystem>();
        }

        private void SetApplicationSettings()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        private void OnDestroy()
        {
            _isLoaded = false;
            SystemManager.Get<ShopSystem>().Dispose();
            SystemManager.Get<ScoreController>().Dispose();
            SystemManager.Get<SaveLoadController>().Dispose();
            SystemManager.Dispose();
        }
    }
}