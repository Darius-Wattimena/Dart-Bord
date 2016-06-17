using System.Collections.Generic;

namespace Dart_Score_Bord
{
    class Match
    {
        public int TotalSets;
        Dictionary<Player, int> legs = new Dictionary<Player, int>();
        Dictionary<Player, int> sets = new Dictionary<Player, int>();

        public int AddNewLeg(Player selectedPlayer, int legValue)
        {
            legs.Add(selectedPlayer, legValue);
            return legValue;
        }

        public int AddNewSet(Player selectedPlayer, int setValue)
        {
            sets.Add(selectedPlayer, setValue);
            return setValue;
        }

        public int UpdateLeg(Player selectedPlayer)
        {
            legs[selectedPlayer] += 1;
            return legs[selectedPlayer];
        }

        public bool CheckLeg(Player selectedPlayer)
        {
            if (legs[selectedPlayer] == 3)
            {
                return true;
            }
            return false;
        }

        public int ResetLegs(Player selectedPlayer)
        {
            legs[selectedPlayer] = 0;
            return 0;
        }

        public int UpdateSet(Player selectedPlayer)
        {
            sets[selectedPlayer] += 1;
            return sets[selectedPlayer];
        }
    }
}
