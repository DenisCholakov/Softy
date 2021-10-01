using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronometer
{
    public class Chronometer : IChronometer
    {
        private Stopwatch stopwatch = new Stopwatch();
        private List<string> laps = new List<string>();

        public string GetTime => this.stopwatch.Elapsed.ToString("mm\\:ss\\.ffff");

        public List<string> Laps => this.laps;

        public string Lap()
        {
            var timeElapsed = this.stopwatch.Elapsed.ToString("mm\\:ss\\.ffff");
            this.laps.Add(timeElapsed);
            return timeElapsed;
        }

        public void Reset()
        {
            this.stopwatch.Reset();
            this.laps = new List<string>();
        }

        public void Start()
        {
            this.stopwatch.Start();
        }

        public void Stop()
        {
            this.stopwatch.Stop();
        }
    }
}
