using System;

namespace FitTrack.Workouts
{
    public class CardioWorkout : Workout
    {
        public int Distance { get; } // Distans i kilometer eller meter (enligt användning)
        public int CaloriesBurned { get; private set; }

        // Konstruktör för CardioWorkout
        public CardioWorkout(string workoutName, DateTime workoutDate, string workoutType, TimeSpan duration, string notes, int distance)
            : base(workoutName, workoutDate, workoutType, duration, 0, notes)
        {
            Distance = distance; // Sätter avståndet
            CaloriesBurned = CalcCalBurned();
        }

        // Överskriven metod för att beräkna förbrända kalorier
        public override int CalcCalBurned()
        {
            return Distance * (int)Duration.TotalMinutes; // Beräknar förbrända kalorier baserat på avstånd och varaktighet
        }
    }
}
