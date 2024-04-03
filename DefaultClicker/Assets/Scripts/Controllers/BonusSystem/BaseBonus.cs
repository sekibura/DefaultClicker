using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SekiburaGames.DefaultClicker.Controllers
{
    public abstract class BaseBonus : MonoBehaviour
    {
        public abstract void Apply(object o = null); 
        public abstract void RemoveBonus();
    }
}
