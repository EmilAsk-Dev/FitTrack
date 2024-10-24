﻿using System;

namespace FitTrack.Workouts
{
    public class CardioWorkout : Workout
    {
        public int Distance { get; }

        public CardioWorkout(DateTime workoutDate, string workoutType, TimeSpan duration, int caloriesBurned, string notes, int distance)
            : base(workoutDate, workoutType, duration, caloriesBurned, notes)
        {
            Distance = distance;
        }

        public override int CalcCalBurned()
        {           
            return Distance * (int)Duration.TotalMinutes;
        }
    }
}