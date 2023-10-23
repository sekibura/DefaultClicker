using PimDeWitte.UnityMainThreadDispatcher;
using SekiburaGames.DefaultClicker.System;
using System;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
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

        public void Initialize()
        {
            InitDefaultValues();
            TimerCallback tm = new TimerCallback(Tick);
            timer = new Timer(tm, null, 0, 1000);
        }

        private void InitValuesFromSave()
        {
            PlayerProgressData playerProgressData = SystemManager.Get<SaveLoadController>().Load();
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
            Debug.Log($"Update score {delta}");
            if (Score + delta < 0)
                return false;

            Score = Score + delta > 0 ? Score + delta : 0;

            if(delta != 0)
                ScoreUpdatedEvent?.Invoke(Score);

            return true;
        }

        public void UpdateScorePower(float delta)
        {
            ScorePower = ScorePower + delta > 0 ? ScorePower + delta : 0;
            if (delta != 0)
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
        }
    }
}

