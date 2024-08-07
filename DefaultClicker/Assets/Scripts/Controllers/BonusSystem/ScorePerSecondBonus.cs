using SekiburaGames.DefaultClicker.System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SekiburaGames.DefaultClicker.Controllers
{
    public class ScorePerSecondBonus : BaseBonus
    {

        private float _scoreDelta;
        private float _bonusAdder;
        private ScoreController _scoreController;

        public ScorePerSecondBonus()
        {
            _scoreController = SystemManager.Get<ScoreController>();
        }

        public override void Apply(object o)
        {
            Debug.Log($"Apply bonus ScorePerSecondBonus");

            _bonusAdder = (float)o;
            _scoreController.UpdateScorePerSecond(_bonusAdder);
        }

        public override void RemoveBonus()
        {
            _scoreController.UpdateScorePerSecond(-_bonusAdder);
            Debug.Log($"Remove bonus ScorePerSecondBonus");
        }
    }
}
