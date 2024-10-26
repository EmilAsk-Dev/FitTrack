using System;
using System.Collections.Generic;
using System.Windows;

namespace FitTrack.Workouts
{
    public abstract class Workout
    {
        public DateTime WorkoutDate { get; set; }
        public string WorkoutType { get; set; }
        public TimeSpan Duration { get; set; }
        public int CaloriesBurned { get; set; }
        public string Notes { get; set; }
        public string WorkoutName { get; set; }


        protected Workout(string workoutName, DateTime workoutDate, string workoutType, TimeSpan duration, int caloriesBurned, string notes)
        {
            WorkoutName = workoutName;
            WorkoutDate = workoutDate;
            WorkoutType = workoutType;
            Duration = duration;
            CaloriesBurned = caloriesBurned;
            Notes = notes;
            
        }

        public abstract int CalcCalBurned(); 
    }
}
