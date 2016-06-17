using System;

namespace Dart_Score_Bord
{
    class DartFinisher
    {
        private int _number;
        private int _status;

        public DartFinisher(int number, int status)
        {
            _number = number;
            _status = status;
        }

        public int Value()
        {
            var value = _number*_status;
            return value;
        }

        public void Estimate(int estimate)
        {
            var temp = Score.DartScores(estimate);
            if (temp != null)
            {
                _status = temp._status;
                _number = temp._number;
            }
            else
            {
                _number = estimate / 3;
                if (_number >= 19)
                    _number = 19;

                _status = 3;
            }
        }

        public bool Increment()
        {
            if (_status == 3 && _number == 20)
                return false;

            if (_status == 0)
                _status = 1;

            _number++;
            if (_number >= 20)
            {
                _number = 20;
                _status++;

                if (_status >= 3)
                {
                    _status = 3;
                }
            }

            return true;
        }
        public String SetToString()
        {
            return "[" + _number + "," + _status + "] ";
        }
    }
}
