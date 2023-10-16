using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SekiburaGames.DefaultClicker.Controllers
{
    public class GameStateManager : MonoBehaviour
    {
        public static GameStateManager Instance;
        public GameState State { get; private set; }
        public static event Action<GameState> OnStateChanged;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            UpdateGameState(GameState.InGame);
        }

        public void UpdateGameState(GameState newGameState)
        {
            State = newGameState;

            switch (State)
            {
                case GameState.None:
                    break;
                case GameState.Pause:
                    break;
                case GameState.InGame:
                    break;
                case GameState.GoalsFinished:
                    break;
                default:
                    break;
            }
            OnStateChanged?.Invoke(newGameState);
        }



        public enum GameState
        {
            None,
            Pause,
            InGame,
            GoalsFinished,
            Ad
        }
    }
}