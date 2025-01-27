namespace operation.Algorithms
{
    public class FeedBack
    {
        public static int Schedule(Queue jobQueue, int numQueues, int[] quantumTimes, int currentTime = 0)
        {
            int jobqueues = jobQueue.processes.Count;
            List<Queue> readyQueues = new List<Queue>();

            //building queues 
            for (int i = 0; i < numQueues; i++)
            {
                readyQueues.Add(new Queue());
            }

            Queue terminatedQueue = new Queue();
            Process? runningProcess = null;
            int tempElapsedTime = 0;

            // number of queue that is proccesing 
            int processPreviousRunningLevel = 1;

            //do processing until all processes are done
            while (terminatedQueue.processes.Count != jobqueues)
            {
                //add to ready queue on first line
                if (!jobQueue.IsEmpty && jobQueue.Front().ArrivalTime <= currentTime)
                {
                    readyQueues[0].Enqueue(jobQueue.Dequeue());
                }


                //start from beggining and add process to running from each line till the end 
                for (int i = 0; i < numQueues; i++)
                {
                     // the prevoius line must be empty so it can go to next line 
                    if (!readyQueues[i].IsEmpty && runningProcess == null)
                    {
                        runningProcess = readyQueues[i].Dequeue();
                        processPreviousRunningLevel = i + 1;
                        tempElapsedTime = 0;
                        if (runningProcess.WaitingTime == -1)
                            runningProcess.WaitingTime = currentTime - runningProcess.ArrivalTime;
                        break;
                    }
                }
                
                // processing 
                if (runningProcess != null)
                {
                    int queueIndex = processPreviousRunningLevel - 1;
                    runningProcess.remainingtime--;
                    tempElapsedTime++;

                    // finish the proccess if it is done
                    if (runningProcess.remainingtime == 0)
                    {
                        runningProcess.TerminationTime = currentTime + 1;
                        terminatedQueue.Enqueue(runningProcess);
                        runningProcess = null;
                    }

                    //if it isnt on the last queue and quantum time reaches add it to the next queue 
                    else if (tempElapsedTime == quantumTimes[queueIndex])
                    {
                        if (queueIndex + 1 < numQueues)
                        {
                            readyQueues[queueIndex + 1].Enqueue(runningProcess);
                            runningProcess = null;
                        }
                        
                        //if it is on the last queue and quantum time reaches add it the back of the line till it finish
                        else
                        {
                            readyQueues[queueIndex].Enqueue(runningProcess);
                            runningProcess = null;
                        }
                    }
                }

                currentTime++;
            }
            return currentTime;

        }
    }
}