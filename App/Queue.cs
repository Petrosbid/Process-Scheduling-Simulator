namespace operation
{
    public class Queue
    {
        public List<Process> processes = new List<Process>();

        public bool IsEmpty => processes.Count == 0;

        public void Enqueue(Process process)
        {
            processes.Add(process);
        }

        public Process Dequeue()
        {
            if (IsEmpty)
                return null;

            Process process = processes[0];
            processes.RemoveAt(0);
            return process;
        }

        public Process Front()
        {
            if (IsEmpty)
                return null;

            return processes[0];
        }

        public Process GetProcessWithMinimumCpuServiceTime()
        {
            Process? minProcess = null;
            foreach (var process in processes)
            {
                if (minProcess == null || process.remainingtime < minProcess.remainingtime)
                    minProcess = process;
            }
            return minProcess;
        }

        public Process GetProcessWithHighestPriority()
        {
            Process? highestPriorityProcess = null;
            foreach (var process in processes)
            {
                if (highestPriorityProcess == null || process.Ratio > highestPriorityProcess.Ratio)
                    highestPriorityProcess = process;
            }
            return highestPriorityProcess;
        }
    }
}
