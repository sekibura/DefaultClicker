using Assets.Scripts;
using SekiburaGames.DefaultClicker.Controllers;
using SekiburaGames.DefaultClicker.System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

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
        [SerializeField]
        private TMP_Text _clickCountTMP;

        private ScoreController _scoreController;
        private SaveLoadController _saveLoadController;

        void Start()
        {
            _scoreController = SystemManager.Get<ScoreController>();
            _saveLoadController = SystemManager.Get<SaveLoadController>();

            _saveLoadController.LoadEvent += (a) => Init();
            Init();
            _scoreController.ScoreUpdatedEvent += ScoreUpdated;
            _scoreController.ScorePerSecondUpdatedEvent += ScorePerSecUpdated;
            _scoreController.ScorePowerUpdatedEvent += ScorePowerUpdated;
            _scoreController.ClicksUpdatedEvent += ClicksUpdated;
        }

        private void Init()
        {
            SavesYG saveData = _saveLoadController.Load();
            _scoreTMP.text = saveData.Score.ToString() + "$";
            _scorePowerTMP.text = saveData.ScorePower.ToString() + "$";
            _scorePerSecTMP.text = saveData.ScorePerSecond.ToString() + "$";
            _clickCountTMP.text = saveData.Clicks.ToString();
        }

        private void ScoreUpdated(double value)
        {
            _scoreTMP.text = value.ToString() + "$";
        }
        private void ScorePowerUpdated(double value)
        {
            Debug.Log($"ScorePowerUpdated = {value}");
            _scorePowerTMP.text = value.ToString() + "$";
        }
        private void ScorePerSecUpdated(double value)
        {
            _scorePerSecTMP.text = value.ToString() + "$";
        }
        private void ClicksUpdated(uint value)
        {
            _clickCountTMP.text = value.ToString();
        }
    }
}
