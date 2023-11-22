using SekiburaGames.DefaultClicker.System;
using SekiburaGames.DefaultClicker.UI;
using System;
using UnityEngine;

namespace SekiburaGames.DefaultClicker.Controllers
{
    internal class ClickerGameController: IInitializable
    {
        public ScoreController ScoreController;
        public event Action OnClickEvent;

        public void Initialize()
        {
            ScoreController = SystemManager.Get<ScoreController>();
            OnClickEvent += ScoreController.OnClick;
        }

        /// <summary>
        /// Click on main target
        /// </summary>
        public void OnClick()
        {
            //Debug.Log("MainClick");
            OnClickEvent?.Invoke();
        }
    }

   
}
