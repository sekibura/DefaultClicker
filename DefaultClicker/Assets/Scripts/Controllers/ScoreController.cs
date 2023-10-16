using SekiburaGames.DefaultClicker.Controllers;
using SekiburaGames.DefaultClicker.System;
using System;

using System.Threading;

using Timer = System.Threading.Timer;

namespace SekiburaGames.DefaultClicker.UI
{
    internal class ScoreController: IInitializable
    {
        public float Score { get; private set; }
        public float ScorePower { get; private set; }
        public float ScorePerSecond { get; private set; }

        public event Action<float> ScoreUpdatedEvent;
        public event Action<float> ScorePowerUpdatedEvent;
        public event Action<float> ScorePerSecondUpdatedEvent;

        public void Initialize()
        {
            InitDefaultValues();
            TimerCallback tm = new TimerCallback(Tick);
            Timer timer = new Timer(tm, null, 0, 1000);
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
                UpdateScore(ScorePerSecond);

        }

        public void UpdateScore(float delta)
        {
            Score = Score + delta > 0 ? Score + delta : 0;

            if(delta != 0)
                ScoreUpdatedEvent?.Invoke(Score);
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
    }
}

