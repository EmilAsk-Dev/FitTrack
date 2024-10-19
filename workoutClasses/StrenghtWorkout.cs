using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Workout
{
    internal class StrenghtWorkout : Workout
    {
        public int Distance { get; set; }

        public StrenghtWorkout(int Distance, DateTime Date, string Type, TimeSpan Duration, int CalBurned, string Notes) : base(Date, Type, Duration, CalBurned, Notes)
        {
            this.Distance = Distance;
        }

        public override int CalcCalBurned()
        {
            Console.WriteLine("CalcCalBurned()");
            return Distance;
        }
    }
}
