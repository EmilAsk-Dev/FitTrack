using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FitTrack.Workout
{
    public class StrenghtWorkout : Workout
    {
        public StrenghtWorkout(DateTime Date, string Type, TimeSpan Duration, int CalBurned, string Notes)
            : base(Date, Type, Duration, CalBurned, Notes)
        {

        }

        public override int CalcCalBurned()
        {            
            return (int)(Duration.TotalMinutes * 5); 
        }
    }
}