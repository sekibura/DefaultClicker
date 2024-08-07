using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SekiburaGames.DefaultClicker.Controllers.TimersController;


namespace SekiburaGames.DefaultClicker.Controllers
{
    public abstract class BaseBonus
    {
        public TimerData timerData;
        public abstract void Apply(object o = null); 
        public abstract void RemoveBonus();
    }
}
