using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Workouts
{
    public class StrenghtWorkout : Workout
    {
        public int Repetitions { get; set; }
        public int CaloriesBurned { get; private set; }

        public StrenghtWorkout(string workoutName, DateTime workoutDate, string workoutType, TimeSpan duration, string notes, int repetitions)
            : base(workoutName, workoutDate, workoutType, duration, 0, notes) // noll som placeholder
        {
            Repetitions = repetitions; 
            CaloriesBurned = CalcCalBurned();
        }

        // Överskriven metod för att beräkna förbrända kalorier        
        public override int CalcCalBurned()
        {
            return Repetitions * (int)Duration.TotalMinutes; // Beräknar förbrända kalorier baserat på repitition och varaktighet
        }
    }
}
