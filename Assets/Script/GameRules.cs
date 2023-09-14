using System.Collections.Generic;
using UnityEngine;

namespace RPSLS
{
    public enum State
    {
        Rock,
        Paper,
        Scissors,
        Lizard,
        Spock
    }

    public class GameRules
    {
        private readonly byte[] table = new byte[]
        {
            0b00001, // Rock
            0b00010, // Paper
            0b00100, // Scissors
            0b01000, // Lizard
            0b10000  // Spock
        };

        public Dictionary<byte, byte> StateDictionary { get; } = new Dictionary<byte, byte>();

        private void InitializeStateDictionary()
        {
            // Calculate the outcomes and add them to the dictionary using stateBytes and GameState enum
            StateDictionary.Add((byte)(table[(int)State.Paper] | table[(int)State.Rock]), table[(int)State.Paper]);         // Paper covers Rock
            StateDictionary.Add((byte)(table[(int)State.Scissors] | table[(int)State.Paper]), table[(int)State.Scissors]); // Scissors cuts Paper
            StateDictionary.Add((byte)(table[(int)State.Lizard] | table[(int)State.Scissors]), table[(int)State.Scissors]); // Scissors decapitates Lizard
            StateDictionary.Add((byte)(table[(int)State.Spock] | table[(int)State.Lizard]), table[(int)State.Lizard]);     // Lizard poisons Spock
            StateDictionary.Add((byte)(table[(int)State.Rock] | table[(int)State.Spock]), table[(int)State.Spock]);       // Spock vaporizes Rock

            StateDictionary.Add((byte)(table[(int)State.Rock] | table[(int)State.Scissors]), table[(int)State.Rock]);     // Rock crushes Scissors
            StateDictionary.Add((byte)(table[(int)State.Spock] | table[(int)State.Scissors]), table[(int)State.Spock]);   // Spock smashes Scissors
            StateDictionary.Add((byte)(table[(int)State.Lizard] | table[(int)State.Paper]), table[(int)State.Lizard]);   // Lizard eats Paper
            StateDictionary.Add((byte)(table[(int)State.Rock] | table[(int)State.Lizard]), table[(int)State.Rock]);       // Rock crushes Lizard
            StateDictionary.Add((byte)(table[(int)State.Paper] | table[(int)State.Spock]), table[(int)State.Paper]);     // Paper disproves Spock
        }

        public GameRules()
        {
            // Initialize the dictionary with game rules
            InitializeStateDictionary();
        }

        public byte GetByte(State state)
        {
            return table[(int)state];
        }

        public int GetStatesCount()
        {
            return table.Length;
        }
    }
}
