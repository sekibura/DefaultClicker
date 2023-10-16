using Assets.Scripts;
using SekiburaGames.DefaultClicker.System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SekiburaGames.DefaultClicker.UI
{
    public class UI_StatsPanel : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _scoreTMP;
        [SerializeField]
        private TMP_Text _scorePerSecTMP;
        [SerializeField]
        private TMP_Text _scorePowerTMP;

        private ScoreController _scoreController;


        void Start()
        {
            _scoreController = SystemManager.Get<ScoreController>();
            Init();
        }

        private void Init()
        {
            _scoreTMP.text = _scoreController.Score.ToString();
            _scorePowerTMP.text = _scoreController.ScorePower.ToString();
            _scorePerSecTMP.text = _scoreController.ScorePerSecond.ToString();

            _scoreController.ScoreUpdatedEvent += ScoreUpdated;
            _scoreController.ScorePerSecondUpdatedEvent += ScorePerSecUpdated;
            _scoreController.ScorePowerUpdatedEvent += ScorePowerUpdated;
        }

        private void ScoreUpdated(float value)
        {
            _scoreTMP.text = value.ToString();
        }
        private void ScorePowerUpdated(float value)
        {
            _scorePowerTMP.text = value.ToString();
        }
        private void ScorePerSecUpdated(float value)
        {
            _scorePerSecTMP.text = value.ToString();
        }


    }
}
