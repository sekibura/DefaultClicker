using SekiburaGames.DefaultClicker.System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.RestService;
using UnityEngine;

namespace SekiburaGames.DefaultClicker.Controllers
{
    public class SaveLoadController : IInitializable
    {
        private PlayerProgressData _progressData;
        public void Initialize()
        {
          
        }

        public PlayerProgressData Load()
        {
            return new PlayerProgressData();
        }

        public void Save()
        {

        }
    }
}
