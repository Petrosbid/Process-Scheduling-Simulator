namespace operation.Algorithms
{
    public class FCFS
    {
        public static int Schedule(Queue jobQueue , int currentTime = 0)
        {
            Queue readyQueue = new Queue();
            Queue terminatedQueue = new Queue();
            Process? runningProcess = null;

            while (jobQueue.processes.Count + readyQueue.processes.Count != 0 || runningProcess != null)
            {
                if (!jobQueue.IsEmpty && jobQueue.Front().ArrivalTime <= currentTime)
                {
                    readyQueue.Enqueue(jobQueue.Dequeue());
                }

                if (!readyQueue.IsEmpty && runningProcess == null)
                {
                    runningProcess = readyQueue.Dequeue();
                    if (runningProcess.WaitingTime == -1)
                        runningProcess.WaitingTime = currentTime - runningProcess.ArrivalTime;
                }

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
