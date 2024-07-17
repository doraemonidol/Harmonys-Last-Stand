using System.Threading;

namespace MHLab.Patch.Core.Client.Progresses
{
    public class UpdateProgress
    {
        public long TotalSteps { get; set; }

        private long _currentSteps;
        public long CurrentSteps
        {
            get => _currentSteps;
            set
            {
                if (value > TotalSteps) _currentSteps = TotalSteps;
                _currentSteps = value;
            }
        }

        public long Percentage
        {
            get
            {
                if (TotalSteps == 0) return 0;
                
                var percentage = CurrentSteps * 100 / TotalSteps;

                if (percentage > 100) percentage = 100;
                if (percentage < 0) percentage   = 0;

                return percentage;
            }
        }

        public string StepMessage { get; set; }
        
        public void IncrementStep(long increment)
        {
            Interlocked.Add(ref _currentSteps, increment);
        }
    }
}
