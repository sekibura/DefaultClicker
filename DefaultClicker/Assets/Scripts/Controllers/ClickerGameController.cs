using SekiburaGames.DefaultClicker.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
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
            OnClickEvent?.Invoke();
        }
    }

   
}
