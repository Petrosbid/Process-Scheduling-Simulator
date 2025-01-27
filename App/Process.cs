namespace operation
{
    public class Process
    {
        public int ProcessId { get; set; }
        public int Processsize { get; set; }
        // زمان شروع
        public int ArrivalTime { get; set; }
        //زمان درخواست
        public int ServiceTime { get; set; }
        public int remainingtime { get; set; }
        //زمان ورود
        public int WaitingTime { get; set; } = -1;
        public int ResponseTime { get; set; } = -1;
        public int TerminationTime { get; set; } = -1;
        //زمان پایان
        public double Ratio { get; set; } = -1;
        public int TurnarountTime { get; set; } = -1;


        public Process(int processId, int arrivalTime, int cpuBurstTime, int processsize)
        {
            ProcessId = processId;
            ArrivalTime = arrivalTime;
            ServiceTime = cpuBurstTime;
            remainingtime = ServiceTime;
            Processsize = processsize;
        }



        public int GetResponseTime()
        {
            ResponseTime = WaitingTime + ServiceTime;
            return ResponseTime;
        }

        public double CalculateRatioNumber()
        {
            Ratio = (double)(WaitingTime + ServiceTime) / ServiceTime;
            return Ratio;
        }
        public int GetTurnarountTime()
        {
            TurnarountTime = TerminationTime - ArrivalTime;
            return TurnarountTime;
        }
    }
}
