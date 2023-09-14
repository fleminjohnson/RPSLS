using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPSLS
{
    public class GameManager : SingletonBehaviour<GameManager>
    {
        [SerializeField] private UIManager uIManager;

        private GameRules gameRules;

        private byte playerMove; // Store the player's move
        private byte cpuMove;    // Store the CPU's move
        private int currentRound = 1;
        private float currentScore = 0;

        private void Start()
        {
            gameRules = new GameRules();
        }

        public void OnStart()
        {
            uIManager.OnGameStart(currentRound, () => { SimulateCPUMove(); });
        }

        public void SetPlayerMove(State move)
        {
            playerMove = gameRules.GetByte(move);
            DetermineWinner();
        }

        public void SimulateCPUMove()
        {
            // Generate a random move for the CPU
            int index = UnityEngine.Random.Range(0, gameRules.GetStatesCount());
            cpuMove = gameRules.GetByte((State)index);
            
            uIManager.SetCPUState(Enum.GetName(typeof(State), (State)index));
        }

        private void DetermineWinner()
        {
            if (playerMove == cpuMove)
            {
                Debug.Log("It's a tie!");
            }
            else if (gameRules.StateDictionary.ContainsKey((byte)(playerMove | cpuMove)))
            {
                byte winnerMove = gameRules.StateDictionary[(byte)(playerMove | cpuMove)];
                if (winnerMove == playerMove)
                {
                    Debug.Log("Player wins!");
                    currentRound++;
                    currentScore++;
                    uIManager.OnWin(currentRound, currentScore);
                }
                else
                {
                    Debug.Log("CPU wins!");
                    uIManager.OnLose();
                }
            }
            else
            {
                Debug.Log("Invalid move combination!");
            }
        }

        public void QuitGame()
        {
            // Check if the application is running in the Unity Editor (for testing purposes)
            #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
            #else
                    // Quit the application (works in a standalone build)
                    Application.Quit();
            #endif
        }
    }
}
