namespace Dart_Score_Bord
{
    public class Player
    {
        Dart darts = new Dart();
        Score score = new Score();
        Match match = new Match();
        int Set;
        public int SetStartScore()
        {
             return score.SetStartScore();
        }

        public int SetStartLegs(Player selectedPlayer)
        {
            return match.AddNewLeg(selectedPlayer, 0);
        }

        public int SetStartSet(Player selectedPlayer)
        {
            return match.AddNewSet(selectedPlayer, 0);
        }

        public int GetThrow(int section, FieldStatus fieldstatusEnum)
        {
            return darts.Throw(section, fieldstatusEnum);
        }

        public int CalculateThrowedScore(int sendScore)
        {
            return score.ScoreAdd(sendScore);
        }

        public int CalculateTotalScore(int totalCurentScore, int totalThrowedScore)
        {
            return score.CalculateScore(totalCurentScore, totalThrowedScore);
        }

        public int GetTotalScore()
        {
            return score.SendScoreTotal();
        }

        public int AddLeg(Player selectedPlayer)
        {
            return match.UpdateLeg(selectedPlayer);
        }

        public bool AddSet(Player selectedPlayer)
        {
            if (match.CheckLeg(selectedPlayer))
            {
                Set = match.UpdateSet(selectedPlayer);
                return true;
            }
            return false;
        }

        public int GetAddedSet()
        {
            return Set;
        }

        public int ResetScore()
        {
            return score.SetStartScore();
        }

        public int ResetLegs(Player selectedPlayer)
        {
            return match.ResetLegs(selectedPlayer);
        }

        public string GetCheckout(int leftScore, int leftDarts)
        {
            return score.GetCheckout(leftScore, leftDarts);
        }
    }
}
