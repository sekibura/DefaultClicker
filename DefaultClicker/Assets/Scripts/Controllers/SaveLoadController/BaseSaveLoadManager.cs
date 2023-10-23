using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.RestService;
using UnityEngine;

namespace SekiburaGames.DefaultClicker.Controllers
{
    public abstract class BaseSaveLoadManager
    {
        protected PlayerProgressData currentProgressData;
        public abstract void Save(PlayerProgressData progressData);
        public abstract PlayerProgressData Load();
    }
}
