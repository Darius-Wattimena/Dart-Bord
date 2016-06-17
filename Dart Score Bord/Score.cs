using System.Collections.Generic;

namespace Dart_Score_Bord
{
    class Score
    {
        private int _count;
        private int _totalThrowedScore;
        private string _finishString;
        private string _bulleyeFinishString;
        private int _dartsValue;

        public int SetStartScore()
        {
            return 501;
        }

        public int ScoreAdd(int sendScore)
        {
            if (!ScoreCounter())
            {
                _totalThrowedScore = 0;
                _totalThrowedScore += sendScore;
                return _totalThrowedScore;
            }
            _totalThrowedScore += sendScore;
            return _totalThrowedScore;
        }

        public int SendScoreTotal()
        {
            return _totalThrowedScore;
        }

        public bool ScoreCounter()
        {
            if (_count == 3)
            {
                _count = 0;
                return false;
            }
            _count++;
            return true;
        }

        public int CalculateScore(int currentScore , int totalThrowedScore)
        {
            currentScore -= totalThrowedScore;
            return currentScore;
        }

        public string GetCheckout(int score, int leftDarts)
        {
            if (score > 170) return "Can not finish";

            var dartsBulleye = new List<DartFinisher>{ new DartFinisher(50, 1), new DartFinisher(0, 0), new DartFinisher(0, 0) };
            dartsBulleye = GetDartList(score, dartsBulleye, leftDarts);

            for (var i = 2; i >=0; i--)
            {
                _dartsValue += dartsBulleye[i].Value();
                _bulleyeFinishString += dartsBulleye[i].SetToString();
            }

            if (IsScoreEaqual(_dartsValue, score, _bulleyeFinishString) != null)
            {
                var returnString = ReturnString(_bulleyeFinishString);
                return returnString;
            }
            
            for(var dubble = 20 ; dubble >= 1 ; dubble--)
            {
                var darts = new List<DartFinisher>{ new DartFinisher(dubble, 2), new DartFinisher(0, 0), new DartFinisher(0, 0) };
                darts = GetDartList(score, darts, leftDarts);
                _dartsValue = 0;
                for (var i = 2; i >= 0; i--)
                {
                    _dartsValue += darts[i].Value();
                    _finishString += darts[i].SetToString();
                }
                if(IsScoreEaqual(_dartsValue, score, _finishString) != null)
                {
                    var returnString = ReturnString(_finishString);
                    return returnString;
                }
                _finishString = null;
            }
            return _dartsValue != score ? "Can not finish" : null;
        }

        public static List<DartFinisher> GetDartList(int desired, List<DartFinisher> darts, int leftDarts)
        {
            switch (leftDarts)
            {
                case 1:
                {
                    OneDartScore(desired, darts);
                    return darts;
                }
                case 2:
                {
                    OneDartScore(desired, darts);
                    TwoDartScore(desired, darts);
                    return darts;
                }
                case 3:
                {
                    OneDartScore(desired, darts);
                    TwoDartScore(desired, darts);
                    ThreeDartScore(desired, darts);
                    return darts;
                }
            }
            return null;
        }

        public static List<DartFinisher> OneDartScore(int desired, List<DartFinisher> darts)
        {
            var dart1 = DartFinishes(desired);
            if (dart1 == null) return darts;

            darts[0] = dart1;
            return darts;
        }
        public static List<DartFinisher> TwoDartScore(int desired, List<DartFinisher> darts)
        {
            var rest = desired - darts[0].Value();
            if (rest <= 0)
                return darts;

            var dart2 = DartScores(rest);
            if (dart2 == null) return darts;

            darts[1] = dart2;
            return darts;
        }
        public static List<DartFinisher> ThreeDartScore(int desired, List<DartFinisher> darts)
        {
            var rest = desired - darts[0].Value();
            if (rest <= 0)
                return darts;
            var dart2 = DartScores(rest);

            if (dart2 != null)
            {
                darts[1] = dart2;
                return darts;
            }

            var temp = darts[1];

            if (temp.Increment())
            {
                if (rest > 60 && temp.Increment())
                    temp.Estimate(rest / 2);

                darts[1] = temp;

                var rest2 = desired - darts[0].Value() - darts[1].Value();
                var dart3 = DartScores(rest2);
                if (dart3 != null)
                {
                    darts[2] = dart3;
                    return darts;
                }

                return darts;
            }
            return null;
        }

        public string IsScoreEaqual(int score1, int score2, string scoreReturn)
        {
            return score1 == score2 ? scoreReturn : null;
        }

        public string ReturnString(string finishString)
        {
            if (finishString == null) return null;
            _finishString = null;
            _bulleyeFinishString = null;
            return finishString;
        }

        public static DartFinisher DartFinishes(int points)
        {
            return LeftDarts(points);
        }
        public static DartFinisher LeftDarts(int points)
        {
            float dividend = points;
            var value = dividend / 2;
            if (value >= 1 && value <= 20)
            {
                if (value % 1 == 0)
                    return new DartFinisher(points/2, 2);
            }

            return points == 50 ? new DartFinisher(50, 1) : null;
        }
        public static DartFinisher DartScores(int points)
        {
            return AddDarts(points);
        }

        public static DartFinisher AddDarts(int points)
        {
            switch (points)
            {
                case 50:
                    return new DartFinisher(50, 1);
                case 25:
                    return new DartFinisher(25, 1);
            }

            float dividend = points;
            var value = dividend / 3;
            if (value % 1 == 0)
                if (value >= 1 && value <= 20)
                    return new DartFinisher(points / 3, 3);

            var value2 = dividend / 2;
            if (value2 % 1 == 0)
                if (value2 >= 1 && value2 <= 20)
                    return new DartFinisher(points / 2, 2);

            return points <= 20 ? new DartFinisher(points, 1) : null;
        }
    }
}
