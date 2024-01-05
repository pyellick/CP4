using Newtonsoft.Json;
using System;

namespace CP4.Classes
{
    public class Player
    {
        public Guid Id { get; set; }
        public Guid TeamId { get; set; }
        public string TeamName { get; set; }
        public string Name { get; set; }
        public int JerseyNumber { get; set; }
        public int Speed { get; set; }
        public int Defense { get; set; }
        public int Throwing { get; set; }
        public int Cutting { get; set; }
        public int ScoringAbility { get; set; }
        public bool OLine { get; set; }
        public bool DLine { get; set; }
        public bool ALine { get; set; }
        public bool SLine { get; set; }
        public bool Handler { get; set; }
        public bool Cutter { get; set; }
        public bool Hybrid { get; set; }
        public bool Inactive { get; set; }
        public bool DeepThreat { get; set; }
        public bool CupPlayer { get; set; }
        public bool WingPlayer { get; set; }
        public bool MiddlePlayer { get; set; }
        public bool DeepPlayer { get; set; }
        public bool Bracket { get; set; }
        public bool StarPlayer { get; set; }
        public int Points { get; set; }
        public int Assists { get; set; }

        private int pntsWon = 0;
        private int pntsPlayed = 0;

        public int PntsWon
        {
            get { return pntsWon; }
            set
            {
                pntsWon = value;
                // Recalculate WinPct whenever PntsWon or PntsPlayed changes
                CalculateWinPct();
            }
        }

        public int PntsPlayed
        {
            get { return pntsPlayed; }
            set
            {
                pntsPlayed = value;
                // Recalculate WinPct whenever PntsWon or PntsPlayed changes
                CalculateWinPct();
            }
        }

        public double WinPct { get; private set; }

        // Constructor
        public Player()
        {
            Id = Guid.NewGuid();
            Name = string.Empty;
            TeamId = Guid.Empty;
            PntsWon = 0;       // Set the default value of PntsWon to 0
            PntsPlayed = 0;    // Set the default value of PntsPlayed to 0
            CalculateWinPct(); // Calculate the initial WinPct
        }

        // Private method to calculate WinPct
        private void CalculateWinPct()
        {
            if (PntsPlayed == 0)
            {
                WinPct = 0; // Avoid division by zero
            }
            else
            {
                WinPct = (double)PntsWon / PntsPlayed;
            }
        }
    }
}
