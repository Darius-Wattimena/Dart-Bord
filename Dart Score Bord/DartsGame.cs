using System;

namespace Dart_Score_Bord
{
    public class DartsGame
    {
        Player player1 = new Player();
        Player player2 = new Player();
        public int Darts;
        public int Playerturn = 1;
        public int P1TotalScore;
        public int P1ThrowedScore;
        public int P2TotalScore;
        public int P2ThrowedScore;
        int _p1NewScore;
        int _p2NewScore;

        public int P1Sets;
        public int P2Sets;
        public int P1Legs;
        public int P2Legs;
        public bool Switched;
        public bool SetAdded;

        public string P1Checkout;
        public string P2Checkout;

        public DartsGame()
        {
            P1TotalScore = player1.SetStartScore();
            _p1NewScore = player1.SetStartScore();
            P2TotalScore = player2.SetStartScore();
            _p2NewScore = player2.SetStartScore();
            P1Legs = player1.SetStartLegs(player1);
            P2Legs = player2.SetStartLegs(player2);
            P1Sets = player1.SetStartSet(player1);
            P2Sets = player2.SetStartSet(player2);
            P2Checkout = @"Can not finish";
        }

        public void GetDartThrow(object number, object fieldstatusEnum)
        {
            var convertedNumber = Convert.ToInt32(number);
            var fieldstatusEnumString = Convert.ToString(fieldstatusEnum);
            DartCounter();
            if (CheckTurn(Playerturn))
            {
                var player1Result = player1.GetThrow(convertedNumber, FieldstatusEnumConvert(fieldstatusEnumString));
                player1.CalculateThrowedScore(player1Result);
                P1ThrowedScore = player1Result;
                _p1NewScore = player1.CalculateTotalScore(_p1NewScore, P1ThrowedScore);
                Console.WriteLine(@"Player 1");
                Console.WriteLine(@"Throwed = " + P1ThrowedScore);
                Console.WriteLine(@"New Total Score = " + _p1NewScore);
                Console.WriteLine(@"Darts left = " + Darts);
                if (Darts != 0)
                {
                    P1Checkout = player1.GetCheckout(_p1NewScore, Darts);
                }
                if (CheckScoreZero() || CheckScoreBelowZeroOrEqualToOne())
                {
                    return;
                }
                if (Darts == 0)
                {
                    P1TotalScore = _p1NewScore;
                    Switched = true;
                    SwitchTurn();
                }
            }
            else
            {
                var player2Result = player2.GetThrow(convertedNumber, FieldstatusEnumConvert(fieldstatusEnumString));
                player2.CalculateThrowedScore(player2Result);
                P2ThrowedScore = player2Result;
                _p2NewScore = player2.CalculateTotalScore(_p2NewScore, P2ThrowedScore);
                Console.WriteLine(@"Player 2");
                Console.WriteLine(@"Throwed = " + P2ThrowedScore);
                Console.WriteLine(@"New Total Score = " + _p2NewScore);
                Console.WriteLine(@"Darts left = " + Darts);
                if (Darts != 0)
                {
                    P2Checkout = player2.GetCheckout(_p2NewScore, Darts);
                }
                if (CheckScoreZero() || CheckScoreBelowZeroOrEqualToOne())
                {
                    return;
                }
                if (Darts == 0)
                {
                    P2TotalScore = _p2NewScore;
                    Switched = true;
                    SwitchTurn();
                }
            }
        }

        public bool CheckTurn(int turn)
        {
            return turn == 1;
        }

        public void SwitchTurn()
        {
            if (Playerturn == 1)
            {
                Playerturn = 2;
                P1Checkout = player1.GetCheckout(P1TotalScore, 3);
                P2Checkout = player2.GetCheckout(P2TotalScore, 3);
            }
            else
            {
                Playerturn = 1;
                P1Checkout = player1.GetCheckout(P1TotalScore, 3);
                P2Checkout = player2.GetCheckout(P2TotalScore, 3);
            }
        }

        public int DartCounter()
        {
            if (Darts != 0) return --Darts;
            return Darts = 2;
        }

        public bool CheckScoreZero()
        {
            if (_p1NewScore == 0)
            {
                P1Legs = player1.AddLeg(player1);
                _p1NewScore = P1TotalScore = player1.ResetScore();
                _p2NewScore = P2TotalScore = player2.ResetScore();
                SetAdded = player1.AddSet(player1);
                if (SetAdded)
                {
                    P1Sets = player1.GetAddedSet();
                    P1Legs = player1.ResetLegs(player1);
                    P2Legs = player2.ResetLegs(player2);
                }
                Switched = true;
                SwitchTurn();
                Darts = 3;
                return true;
            }
            if (_p2NewScore == 0)
            {
                P2Legs = player2.AddLeg(player2);
                _p1NewScore = P1TotalScore = player1.ResetScore();
                _p2NewScore = P2TotalScore = player2.ResetScore();
                SetAdded = player2.AddSet(player2);
                if (SetAdded)
                {
                    P2Sets = player2.GetAddedSet();
                    P1Legs = player1.ResetLegs(player1);
                    P2Legs = player2.ResetLegs(player2);
                }
                Switched = true;
                SwitchTurn();
                Darts = 3;
                return true;
            }
            return false;
        }

        public bool CheckScoreBelowZeroOrEqualToOne()
        {
            if (_p1NewScore < 0 || _p1NewScore == 1)
            {
                P2Legs = player2.AddLeg(player2);
                _p1NewScore = P1TotalScore = player1.ResetScore();
                _p2NewScore = P2TotalScore = player2.ResetScore();
                SetAdded = player2.AddSet(player2);
                if (SetAdded)
                {
                    P2Sets = player2.GetAddedSet();
                    P1Legs = player1.ResetLegs(player1);
                    P2Legs = player2.ResetLegs(player2);
                }
                Switched = true;
                SwitchTurn();
                Darts = 3;
                return true;
            }
            if (_p2NewScore < 0 || _p2NewScore == 1)
            {
                P1Legs = player1.AddLeg(player1);
                _p1NewScore = P1TotalScore = player1.ResetScore();
                _p2NewScore = P2TotalScore = player2.ResetScore();
                if (SetAdded)
                {
                    P1Sets = player1.GetAddedSet();
                    P1Legs = player1.ResetLegs(player1);
                    P2Legs = player2.ResetLegs(player2);
                }
                Switched = true;
                SwitchTurn();
                Darts = 3;
                return true;
            }
            return false;
        }

        public FieldStatus FieldstatusEnumConvert(string selectedEnum)
        {
            switch (selectedEnum)
            {
                case "Single":
                    return FieldStatus.Single;
                case "Double":
                    return FieldStatus.Double;
                case "Triple":
                    return FieldStatus.Triple;
                case "Bull":
                    return FieldStatus.Bull;
            }
            return FieldStatus.Bullseye;
        }

        public string ValueToString(int value)
        {
            var valueString = value.ToString();
            return valueString;
        }
    }
}
