using System;

namespace FitTrack.Workouts
{
    public class CardioWorkout : Workout
    {
        public int Distance { get; } // Distans i kilometer eller meter (enligt användning)

        // Konstruktör för CardioWorkout
        public CardioWorkout(string workoutName, DateTime workoutDate, string workoutType, TimeSpan duration, int caloriesBurned, string notes, int distance)
            : base(workoutName, workoutDate, workoutType, duration, caloriesBurned, notes)
        {
            Distance = distance; // Sätter avståndet
        }

        // Överskriven metod för att beräkna förbrända kalorier
        public override int CalcCalBurned()
        {
            return Distance * (int)Duration.TotalMinutes; // Beräknar förbrända kalorier baserat på avstånd och varaktighet
        }
    }
}
