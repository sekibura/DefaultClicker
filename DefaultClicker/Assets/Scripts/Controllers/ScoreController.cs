using PimDeWitte.UnityMainThreadDispatcher;
using SekiburaGames.DefaultClicker.System;
using System;
using System.Reflection;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using YG;
using Timer = System.Threading.Timer;

namespace SekiburaGames.DefaultClicker.Controllers
{
    public class ScoreController: System.IInitializable
    {
        public float Score { get; private set; }
        public float ScorePower { get; private set; }
        public float ScorePerSecond { get; private set; }

        public event Action<float> ScoreUpdatedEvent;
        public event Action<float> ScorePowerUpdatedEvent;
        public event Action<float> ScorePerSecondUpdatedEvent;
        Timer timer;

        protected SaveLoadController saveLoadController;

        public void Initialize()
        {
            InitDefaultValues();
            TimerCallback tm = new TimerCallback(Tick);
            timer = new Timer(tm, null, 0, 1000);
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

        private void Tick(object obj)
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

        public bool UpdateScore(float delta)
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

        public void UpdateScorePerSecond(float delta)
        {
            ScorePerSecond = ScorePerSecond + delta > 0 ? ScorePerSecond + delta : 0;
            if (delta != 0)
                ScorePerSecondUpdatedEvent?.Invoke(ScorePerSecond);
        }

        public void Dispose()
        {
            timer.Dispose();
            SaveProgress();

        }

    }
}

