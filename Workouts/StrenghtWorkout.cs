using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Workouts
{
    public class StrenghtWorkout : Workout
    {
        public int Repetitions { get; } // Antal repetitioner i övningen

        // Konstruktör för StrenghtWorkout
        public StrenghtWorkout(string workoutName, DateTime workoutDate, string workoutType, TimeSpan duration, int caloriesBurned, string notes, int repetitions)
            : base(workoutName, workoutDate, workoutType, duration, caloriesBurned, notes)
        {
            Repetitions = repetitions; // Sätter antalet repetitioner
        }

        // Överskriven metod för att beräkna förbrända kalorier
        public override int CalcCalBurned()
        {
            return (int)(Duration.TotalMinutes * 5); // Beräknar förbrända kalorier baserat på varaktighet
        }
    }
}
