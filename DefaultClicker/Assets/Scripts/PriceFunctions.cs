using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using YG;

namespace SekiburaGames.DefaultClicker.Controllers
{
    public static class PriceFunctions
    {
        public static float BaseBackgroundPrice = 100f;
        public static float MultiplerBackgroundPrice = 1.1f;
        // 111 max price = 51278931068
        public static double CalcBackgroundPrice(int iteration)
        {
            return Math.Round(BaseBackgroundPrice * Math.Pow(MultiplerBackgroundPrice, iteration));
        }

        public static float BasePriceCharacter = 100f;
        public static float MultiplerPriceCharacter = 2.1f;
        // 11 max price = 49239910319
        public static double CalcPriceCharacter(int iteration)
        {
            return Math.Round(BasePriceCharacter * Math.Pow(MultiplerPriceCharacter, iteration));
        }
        
        public static float BasePriceClickScore = 100f;
        public static float MultiplerPriceClickScore = 1.3f;
        public static double CalcPriceClickScore(int iteration)
        {
            return Math.Round(BasePriceClickScore * Math.Pow(MultiplerPriceClickScore, iteration));
        }
        
        public static float BasePricePerSec = 100f;
        public static float MultiplerPricePerSec = 1.3f;
        public static double CalcPricePerSec(int iteration)
        {
            return Math.Round(BasePricePerSec * Math.Pow(MultiplerPricePerSec, iteration));
        }
    }
}
