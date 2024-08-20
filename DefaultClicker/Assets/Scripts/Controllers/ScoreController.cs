﻿using PimDeWitte.UnityMainThreadDispatcher;
using SekiburaGames.DefaultClicker.System;
using System;
using System.Reflection;
//using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using YG;
using static SekiburaGames.DefaultClicker.Controllers.TimersController;
using Timer = System.Threading.Timer;

namespace SekiburaGames.DefaultClicker.Controllers
{
    public class ScoreController: System.IInitializable
    {
        public double Score { get; private set; }
        public float ScorePower { get; private set; }
        public float ScorePerSecond { get; private set; }

        public event Action<double> ScoreUpdatedEvent;
        public event Action<double> ScorePowerUpdatedEvent;
        public event Action<double> ScorePerSecondUpdatedEvent;
        TimerData timer;
        TimerData timerSave;

        protected SaveLoadController saveLoadController;

        public void Initialize()
        {
            InitDefaultValues();

            timer = TimersController.Instance.StartTimer(() => Tick(), 1, true);
            timerSave = TimersController.Instance.StartTimer(() => SaveTick(), 10, true);

            saveLoadController = SystemManager.Get<SaveLoadController>();
            saveLoadController.LoadEvent += (x) => InitOnLoad();
            if (YandexGame.SDKEnabled == true)
            {
                InitOnLoad();
            }
        }

        private void InitOnLoad()
        {
            Score = saveLoadController.Load().Score;
            ScoreUpdatedEvent?.Invoke(Score);
        }

        private void SaveProgress()
        {
            var savesYG = saveLoadController.Load();
            savesYG.Score = Score;
            saveLoadController.Save(savesYG);
        }

        private void InitValuesFromSave()
        {
            SavesYG playerProgressData = SystemManager.Get<SaveLoadController>().Load();
            Score = playerProgressData.Score;
            ScorePower = playerProgressData.ScorePower;
            ScorePerSecond = playerProgressData.ScorePerSecond;
        }

        private void InitDefaultValues()
        {
            Score = 0;
            ScorePower = 1;
            ScorePerSecond = 0;
        }

        public void Init(float score, float scorePower, float scorePerSecond)
        {
            Score = score;
            ScorePower = scorePower;
            ScorePerSecond = scorePerSecond;
        }


        public void OnClick()
        {
            if (GameStateManager.Instance.State == GameStateManager.GameState.InGame)
                UpdateScore(ScorePower);
        }

        private void Tick()
        {
            if (GameStateManager.Instance.State == GameStateManager.GameState.InGame)
            {
                UnityMainThreadDispatcher.Instance().Enqueue(() =>
                {
                    UpdateScore(ScorePerSecond);
                });
                
            } 
            //Debug.Log("Tick");
        }

        public bool UpdateScore(double delta)
        {
            //Debug.Log($"Update score  = {delta}");
            if (Score + delta < 0)
                return false;

            Score = Score + delta > 0 ? Score + delta : 0;

            if(delta != 0)
                ScoreUpdatedEvent?.Invoke(Score);

            return true;
        }

        public void UpdateScorePower(float delta)
        {
            ScorePower = ScorePower + delta > 0 ? ScorePower + delta : 1;
            if (delta != 0)
                ScorePowerUpdatedEvent?.Invoke(ScorePower);
        }
        public void SetScorePower(float value)
        {
            ScorePower =  value > 0 ? value : 1;
            if (value != 0)
                ScorePowerUpdatedEvent?.Invoke(ScorePower);
        } 
        public void SetScorePerSec(float value)
        {
            ScorePerSecond =  value > 0 ? value : 0;
        }

        public void UpdateScorePerSecond(float delta)
        {
            ScorePerSecond = ScorePerSecond + delta > 0 ? ScorePerSecond + delta : 0;
            if (delta != 0)
                ScorePerSecondUpdatedEvent?.Invoke(ScorePerSecond);
        }

        private void SaveTick()
        {
            Debug.Log("Auto save");
            SaveProgress();
        }

        public void Dispose()
        {
            TimersController.Instance.StopTimer(timer);
            TimersController.Instance.StopTimer(timerSave);
            SaveProgress();
        }

    }
}

