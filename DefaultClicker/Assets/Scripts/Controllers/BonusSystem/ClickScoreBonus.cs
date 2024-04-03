using SekiburaGames.DefaultClicker.System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SekiburaGames.DefaultClicker.Controllers
{
    /// <summary>
    /// Временный бонус к силе клика
    /// </summary>
    public class ClickScoreBonus : BaseBonus
    {
        private float _scoreDelta;
        private float _bonusAdder;
        private ScoreController _scoreController;

        public ClickScoreBonus()
        {
            _scoreController = SystemManager.Get<ScoreController>();
        }

        public override void Apply(object o)
        {
            Debug.Log($"Apply bonus ClickScoreBonus");

            _bonusAdder = (float)o;
            _scoreController.UpdateScorePower(_bonusAdder);
        }

        public override void RemoveBonus()
        {
            _scoreController.UpdateScorePower(-_bonusAdder);
        }
    }
}
