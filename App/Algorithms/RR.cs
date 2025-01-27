namespace operation.Algorithms
{
    public class RR
    {
        public static int Schedule(Queue jobQueue, int timeQuantum, int currentTime = 0)
        {
            Queue readyQueue = new Queue();
            Queue terminatedQueue = new Queue();
            Queue tempQueue = new Queue();

            Process? runningProcess = null;
            int tempElapsedTime = 0;

            while (jobQueue.processes.Count + readyQueue.processes.Count != 0 || runningProcess != null || tempQueue.processes.Count != 0)
            {

                if (!jobQueue.IsEmpty && jobQueue.Front().ArrivalTime <= currentTime)
                {
                    readyQueue.Enqueue(jobQueue.Dequeue());
                }

                if (tempQueue.processes.Count != 0)
                {
                    readyQueue.Enqueue(tempQueue.Dequeue());
                }

                if (!readyQueue.IsEmpty && runningProcess == null)
                {
                    runningProcess = readyQueue.Dequeue();
                    if (runningProcess.WaitingTime == -1)
                        runningProcess.WaitingTime = currentTime - runningProcess.ArrivalTime;
                    tempElapsedTime = 0;
                }


                if (runningProcess != null)
                {
                    runningProcess.remainingtime--;
                    tempElapsedTime++;

                    if (runningProcess.remainingtime == 0)
                    {
                        runningProcess.TerminationTime = currentTime + 1;
                        terminatedQueue.Enqueue(runningProcess);
                        runningProcess = null;
                    }
                    else if (tempElapsedTime == timeQuantum)
                    {
                        tempQueue.Enqueue(runningProcess);
                        runningProcess = null;
                    }
                }
                currentTime++;
            }
            return currentTime;

        }
    }
}