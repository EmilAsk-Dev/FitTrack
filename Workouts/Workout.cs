using System;
using System.Collections.Generic;
using System.Windows;

namespace FitTrack.Workouts
{
    public abstract class Workout
    {
        public DateTime WorkoutDate { get; set; } // Datum för träningen
        public string WorkoutType { get; set; } // Typ av träning (ex. cardio, styrka)
        public TimeSpan Duration { get; set; } // Varaktighet av träningen
        public int CaloriesBurned { get; set; } // Förbrända kalorier
        public string Notes { get; set; } // Anteckningar om träningen
        public string WorkoutName { get; set; } // Namn på träningen

        // Konstruktör för Workout-klass
        protected Workout(string workoutName, DateTime workoutDate, string workoutType, TimeSpan duration, int caloriesBurned, string notes)
        {
            WorkoutName = workoutName; // Sätter namnet på träningen
            WorkoutDate = workoutDate; // Sätter datum för träningen
            WorkoutType = workoutType; // Sätter typ av träning
            Duration = duration; // Sätter varaktighet
            CaloriesBurned = caloriesBurned; // Sätter förbrända kalorier
            Notes = notes; // Sätter anteckningar
        }

        public abstract int CalcCalBurned(); // Abstrakt metod för att beräkna förbrända kalorier
    }
}
