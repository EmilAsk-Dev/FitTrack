using System;
using System.Collections.Generic;
using System.Windows;

namespace FitTrack.Workouts
{
    public abstract class Workout
    {
        public DateTime WorkoutDate { get; }
        public string WorkoutType { get; }
        public TimeSpan Duration { get; }
        public int CaloriesBurned { get; }
        public string Notes { get; }


        protected Workout(DateTime workoutDate, string workoutType, TimeSpan duration, int caloriesBurned, string notes)
        {
            WorkoutDate = workoutDate;
            WorkoutType = workoutType;
            Duration = duration;
            CaloriesBurned = caloriesBurned;
            Notes = notes;
        }

        public abstract int CalcCalBurned(); 
    }
}
