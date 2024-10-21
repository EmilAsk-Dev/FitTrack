using System;

namespace FitTrack.Workouts
{
    public class CardioWorkout : Workout
    {
        public int Distance { get; set; }

        public CardioWorkout(DateTime Date, string Type, TimeSpan Duration, int CalBurned, string Notes, int Distance)
        : base(Date, Type, Duration, CalBurned, Notes)
        {
            this.Distance = Distance;
        }

        public override int CalcCalBurned()
        {           
            return Distance * (int)Duration.TotalMinutes;
        }
    }
}