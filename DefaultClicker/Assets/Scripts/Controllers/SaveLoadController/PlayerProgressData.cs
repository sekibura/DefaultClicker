using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SekiburaGames.DefaultClicker.Controllers
{
    [Serializable]
    public class PlayerProgressData
    {
        public int Score;
        public int ScorePower;
        public int ScorePerSecond;
        public int BackGroundIndex;
        public int GirlIndex;
        public int OutfitIndex;

        public PlayerProgressData()
        {
            Score = 0;
            ScorePower = 1;
            ScorePerSecond = 0;
            BackGroundIndex = 0;
            GirlIndex = 0;
            OutfitIndex = 0;
        }
    }
}
