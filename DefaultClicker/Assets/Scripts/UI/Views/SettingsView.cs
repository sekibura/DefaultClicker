using SekiburaGames.DefaultClicker.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SekiburaGames.DefaultClicker.UI
{
    public class SettingsView : View
    {
        //private GameStateManager.GameState _lastGameState;
        private void OnEnable()
        {
            //_lastGameState = GameStateManager.Instance.State;
            //GameStateManager.Instance.UpdateGameState(GameStateManager.GameState.Pause);
        }

        private void OnDisable()
        {
            //GameStateManager.Instance.UpdateGameState(_lastGameState);
        }
    }
}
