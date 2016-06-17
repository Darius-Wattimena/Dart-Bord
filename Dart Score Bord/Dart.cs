using System;
using System.Collections.Generic;

namespace Dart_Score_Bord
{
    public class Dart
    {
        public int GetFieldScore(int way, int thrownumber, FieldStatus status)
        {
            var fieldOrder = new List<int> //Volgorde van de velden
            {
                20, 1, 18, 4, 13, 6, 10, 15, 2, 17, 3, 19, 7, 16, 8, 11, 14, 9, 12, 5
            };
            int multi;
            switch (status)
            {
                case FieldStatus.Single:
                {
                    multi = 1;
                }
                    break;
                case FieldStatus.Double:
                {
                    multi = 2;
                }
                    break;
                default:
                    multi = 3;
                    break;
            }
            int field;
            switch (way)
            {
                case 1: //rechter kant van de geselecteerde
                    field = fieldOrder.NextOf(thrownumber);
                    return field * multi;
                case 2: //linker kant van de geselecteerde
                    if (thrownumber == 20)
                    {
                        field = 5;
                        return field * multi;
                    }

                    field = fieldOrder.PreviousOf(thrownumber);
                    return field * multi;
            }
            return thrownumber * multi;
        }

        public int Throw(int throwNumber, FieldStatus status)
        {
            switch (status)
            {
                case FieldStatus.Single:
                    var listSingle = new[]
                    {
                        RandomValue.Create(0.8, GetFieldScore(0, throwNumber, FieldStatus.Single)),
                        RandomValue.Create(0.025, GetFieldScore(0, throwNumber, FieldStatus.Double)),
                        RandomValue.Create(0.025, GetFieldScore(0, throwNumber, FieldStatus.Triple)),
                        RandomValue.Create(0.025, GetFieldScore(1, throwNumber, FieldStatus.Single)),
                        RandomValue.Create(0.025, GetFieldScore(1, throwNumber, FieldStatus.Double)),
                        RandomValue.Create(0.025, GetFieldScore(1, throwNumber, FieldStatus.Triple)),
                        RandomValue.Create(0.025, GetFieldScore(2, throwNumber, FieldStatus.Single)),
                        RandomValue.Create(0.025, GetFieldScore(2, throwNumber, FieldStatus.Double)),
                        RandomValue.Create(0.025, GetFieldScore(2, throwNumber, FieldStatus.Triple))
                    };
                    var selectedSingle = listSingle.ChooseByRandom();
                    return selectedSingle;

                case FieldStatus.Double:
                    var listDouble = new[]
                    {
                        RandomValue.Create(0.8, GetFieldScore(0, throwNumber, FieldStatus.Double)),
                        RandomValue.Create(0.020, GetFieldScore(0, throwNumber, FieldStatus.Single)),
                        RandomValue.Create(0.020, GetFieldScore(1, throwNumber, FieldStatus.Single)),
                        RandomValue.Create(0.020, GetFieldScore(1, throwNumber, FieldStatus.Double)),
                        RandomValue.Create(0.020, GetFieldScore(2, throwNumber, FieldStatus.Single)),
                        RandomValue.Create(0.020, GetFieldScore(2, throwNumber, FieldStatus.Double)),
                        RandomValue.Create(0.1, 0)
                    };
                    var selectedDouble = listDouble.ChooseByRandom();
                    return selectedDouble;

                case FieldStatus.Triple:
                    var listTriple = new[]
                    {
                        RandomValue.Create(0.8, GetFieldScore(0, throwNumber, FieldStatus.Triple)),
                        RandomValue.Create(0.1, GetFieldScore(0, throwNumber, FieldStatus.Single)),
                        RandomValue.Create(0.025, GetFieldScore(1, throwNumber, FieldStatus.Triple)),
                        RandomValue.Create(0.025, GetFieldScore(1, throwNumber, FieldStatus.Single)),
                        RandomValue.Create(0.025, GetFieldScore(2, throwNumber, FieldStatus.Triple)),
                        RandomValue.Create(0.025, GetFieldScore(2, throwNumber, FieldStatus.Single))
                    };
                    var selectedTriple = listTriple.ChooseByRandom();
                    return selectedTriple;

                case FieldStatus.Bull:
                    var listBull = new[]
                    {
                        RandomValue.Create(0.8, 25),
                        RandomValue.Create(0.1, 50),
                        RandomValue.Create(0.1, RandomValue.GetRandomNumber(1,21))
                    };
                    var selectedBull = listBull.ChooseByRandom();
                    return selectedBull;

                case FieldStatus.Bullseye:
                    var listBullseye = new[]
                    {
                        RandomValue.Create(0.8, 50),
                        RandomValue.Create(0.1, 25),
                        RandomValue.Create(0.1, RandomValue.GetRandomNumber(1,21))
                    };
                    var selectedBullseye = listBullseye.ChooseByRandom();
                    return selectedBullseye;
            }
            return 0;
        }
    }
    public class RandomValue<T>
    {
        public double Proportion { get; set; } //Aantal procent
        public T Value { get; set; } //Waarde
    }

    public static class RandomValue
    {
        public static RandomValue<T> Create<T>(double proportion, T value)
        {
            return new RandomValue<T> { Proportion = proportion, Value = value };
        }

        static Random random = new Random();

        private static readonly object syncLock = new object();
        public static int GetRandomNumber(int min, int max)
        {
            lock (syncLock)
            {
                return random.Next(min, max); //return a number between 1 and 20
            }
        }
        public static T ChooseByRandom<T>(
            this IEnumerable<RandomValue<T>> collection) //Kies een random uit bovenstaande collection
        {
            var rnd = random.NextDouble(); //rnd = een random floating-point gelijk aan 0.0 of kleiner dan 1.0
            foreach (var item in collection)
            {
                if (rnd < item.Proportion) //als rnd kleiner is dan de opgegeven procenten
                    return item.Value; //return dan de gekozen random waarde
                rnd -= item.Proportion; //haal de opgegeven procent van rnd af
            }
            throw new InvalidOperationException("De percentages zijn niet samen 100 procent");
        }
    }

    public static class FieldsList
    {
        public static T NextOf<T>(this IList<T> list, T item)
        {
            return list[(list.IndexOf(item) + 1) == list.Count ? 0 : (list.IndexOf(item) + 1)]; //Pak het rechter veld van het geselecteerde veld 
        }

        public static T PreviousOf<T>(this IList<T> list, T item)
        {
            return list[(list.IndexOf(item) - 1) == list.Count ? 0 : (list.IndexOf(item) - 1)]; //Pak het linker veld van het geselecteerde veld 
        }
    }


}
