using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitTrack.Workout
{
    abstract class Workout
    {
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public TimeSpan Duration { get; set; }
        public int CalBurned { get; set; }
        public string Notes { get; set; }

        public Workout(DateTime Date, string Type, TimeSpan Duration, int CalBurned, string Notes)
        {
            this.Date = Date;
            this.Type = Type;
            this.Duration = Duration;
            this.CalBurned = CalBurned;
            this.Notes = Notes;
        }

        public abstract int CalcCalBurned();
    }
}
