using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

namespace SekiburaGames.DefaultClicker.Controllers
{
    public abstract class BaseSaveLoadManager
    {
        public abstract void Init();
        protected SavesYG currentProgressData;
        public abstract void Save(SavesYG progressData);
        public abstract SavesYG Load();
    }
}
