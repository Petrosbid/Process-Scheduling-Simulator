namespace operation.Algorithms
{
    public class SRT
    {
        public static int Schedule(Queue jobQueue , int currentTime = 0)
        {
            Queue readyQueue = new Queue();
            Queue terminatedQueue = new Queue();
            Process? runningProcess = null;

            while (jobQueue.processes.Count + readyQueue.processes.Count != 0 || runningProcess != null)
            {
                // Add arriving processes to the ready queue
                if (!jobQueue.IsEmpty && jobQueue.Front().ArrivalTime <= currentTime)
                {
                    readyQueue.Enqueue(jobQueue.Dequeue());
                }

                // Check if a new process with shorter remaining time has arrived
                if (!readyQueue.IsEmpty)
                {
                    var shortestRemainingTimeProcess = readyQueue.GetProcessWithMinimumCpuServiceTime();
                    if (runningProcess == null || shortestRemainingTimeProcess.remainingtime < runningProcess.remainingtime)
                    {
                        if (runningProcess != null)
                        {
                            readyQueue.Enqueue(runningProcess);
                        }
                        runningProcess = shortestRemainingTimeProcess;
                        readyQueue.processes.Remove(runningProcess);
                        if (runningProcess.WaitingTime == -1)
                            runningProcess.WaitingTime = currentTime - runningProcess.ArrivalTime;
                    }
                }

                // Execute the running process
                if (runningProcess != null)
                {
                    runningProcess.remainingtime--;
                    if (runningProcess.remainingtime == 0)
                    {
                        runningProcess.TerminationTime = currentTime + 1;
                        terminatedQueue.Enqueue(runningProcess);
                        runningProcess = null;
                    }
                }

                currentTime++;
            }
        return currentTime;
        }
    }
}
