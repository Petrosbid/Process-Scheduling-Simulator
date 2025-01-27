using System.Windows;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;
using operation.Algorithms;

namespace operation
{
    public partial class MainWindow : Window
    {
        private string? queueselected;
        List<MemoryPartition> memoryPartitions = new List<MemoryPartition>
        {
            new MemoryPartition(2),
            new MemoryPartition(4),
            new MemoryPartition(10),
            new MemoryPartition(50)
        };
        public MainWindow()
        {
            InitializeComponent();
            ProcessDataGrid.ItemsSource = new List<Process>();
        }

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedAlgorithm = (AlgorithmComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            queueselected = (Queue.SelectedItem as ComboBoxItem)?.Content.ToString();
            var processes = ProcessDataGrid.ItemsSource as List<Process>;

            if (processes == null || !processes.Any())
            {
                MessageBox.Show("Please add at least one process.");
                return;
            }

            // Create a copy of the processes to avoid modifying the original list
            var processesCopy = processes.Select(p => new Process(p.ProcessId, p.ArrivalTime, p.ServiceTime, p.Processsize)).ToList();

            var jobQueue = new Queue();

            //foreach (var process in processesCopy)
            //{
            //    jobQueue.Enqueue(process);
            //}
            List<Process> sortedProcesses = processesCopy.OrderByDescending(p => p.ArrivalTime).ToList();
            while (sortedProcesses.Count > 0)
            {
                Process lastProcess = sortedProcesses.Last();

                jobQueue.Enqueue(lastProcess);

                sortedProcesses.Remove(lastProcess);
            }

            if (queueselected == "Single")
            {
                switch (selectedAlgorithm)
                {
                    case "FCFS":
                        FCFS.Schedule(jobQueue);
                        break;
                    case "RR":
                        if (int.TryParse(QuantumTextBox.Text, out int timeQuantum))
                            RR.Schedule(jobQueue, timeQuantum);
                        else
                        {
                            MessageBox.Show("Please enter a valid time quantum.");
                            return;
                        }
                        break;
                    case "SPN":
                        SPN.Schedule(jobQueue);
                        break;
                    case "SRT":
                        SRT.Schedule(jobQueue);
                        break;
                    case "HRRN":
                        HRRN.Schedule(jobQueue);
                        break;
                    case "FeedBack":
                        if (int.TryParse(QuantumTextBox.Text, out int mlfqTimeQuantum))
                            FeedBack.Schedule(jobQueue, 3, new int[] { mlfqTimeQuantum, mlfqTimeQuantum * 2, mlfqTimeQuantum * 4 });
                        else
                        {
                            MessageBox.Show("Please enter a valid time quantum.");
                            return;
                        }
                        break;
                    default:
                        MessageBox.Show("Please select a valid algorithm.");
                        return;
                }
            }
            else
            {
                int currenttime = 0;
                foreach (var process in processesCopy)
                {
                    var bestPartition = BestFit(process, memoryPartitions);
                    if (bestPartition != null)
                    {
                        bestPartition.ProcessQueue.Enqueue(process);
                    }
                    else
                    {
                        MessageBox.Show($"No suitable partition found for process {process.ProcessId}.");
                        return;
                    }
                }
                foreach (var partition in memoryPartitions)
                {
                    if (!partition.ProcessQueue.IsEmpty)
                    {
                        switch (selectedAlgorithm)
                        {
                            case "FCFS":
                                currenttime = FCFS.Schedule(partition.ProcessQueue , currenttime);
                                break;
                            case "RR":
                                if (int.TryParse(QuantumTextBox.Text, out int timeQuantum))
                                    currenttime = RR.Schedule(partition.ProcessQueue, timeQuantum , currenttime);
                                else
                                {
                                    MessageBox.Show("Please enter a valid time quantum.");
                                    return;
                                }
                                break;
                            case "SPN":
                                currenttime = SPN.Schedule(partition.ProcessQueue, currenttime);
                                break;
                            case "SRT":
                                currenttime =SRT.Schedule(partition.ProcessQueue, currenttime);
                                break;
                            case "HRRN":
                                currenttime = HRRN.Schedule(partition.ProcessQueue, currenttime);
                                break;
                            case "FeedBack":
                                if (int.TryParse(QuantumTextBox.Text, out int mlfqTimeQuantum))
                                    currenttime = FeedBack.Schedule(partition.ProcessQueue, 3, new int[] { mlfqTimeQuantum, mlfqTimeQuantum * 2, mlfqTimeQuantum * 4 });
                                else
                                {
                                    MessageBox.Show("Please enter a valid time quantum.");
                                    return;
                                }
                                break;
                            default:
                                MessageBox.Show("Please select a valid algorithm.");
                                return;
                        }
                    }
                }

            }
            DisplayAlgorithmAnalysis(processesCopy);
            UpdateresutChart(processesCopy);
        }
        
        private MemoryPartition BestFit(Process process, List<MemoryPartition> partitions)
        {
            MemoryPartition bestPartition = null;
            foreach (var partition in partitions)
            {
                if (partition.Size >= process.Processsize && (bestPartition == null || partition.Size < bestPartition.Size))
                {
                    bestPartition = partition;
                }
            }
            return bestPartition;
        }
        
        private void AddProcessButton_Click(object sender, RoutedEventArgs e)
        {
            var processes = ProcessDataGrid.ItemsSource as List<Process>;
            processes?.Add(new Process(processes.Count + 1, 0, 0, 0));
            ProcessDataGrid.ItemsSource = null;
            ProcessDataGrid.ItemsSource = processes;
        }
        
        private void AlgorithmComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedAlgorithm = (AlgorithmComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            QuantumTextBox.Visibility = selectedAlgorithm == "RR" || selectedAlgorithm == "FeedBack" ? Visibility.Visible : Visibility.Collapsed;
        }
        
        private void DeleteProcessButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProcessDataGrid.SelectedItem != null)
            {
                var process = ProcessDataGrid.SelectedItem;
                var processes = ProcessDataGrid.ItemsSource as List<Process>;
                if (processes != null)
                {
                    processes.Remove((Process)process);
                }
                ProcessDataGrid.ItemsSource = null;
                ProcessDataGrid.ItemsSource = processes;
            }
        }
        
        private void UpdateresutChart(List<Process> processes)
        {
            var series = new SeriesCollection();

            foreach (var process in processes)
            {
                // اضافه کردن مقادیر به سری
                series.Add(new RowSeries
                {
                    Title = $"Process: {process.ProcessId}\n",
                    Values = new ChartValues<double>
                    {
                        process.TerminationTime,
                        process.TurnarountTime,
                        (double)process.TurnarountTime / process.ServiceTime
                    }
                });
            }

            result.Series = series;
        }
        
        private void DisplayAlgorithmAnalysis(List<Process> processes)
        {

                int totalWaitingTime = 0;
                int totalResponseTime = 0;
                int totalCpuBurstTime = 0;
                int totalTurnarountTime = 0;
                int finishtime = processes.Max(p => p.TerminationTime);
                int trts = 0;
                foreach (var process in processes)
                {
                    totalWaitingTime += process.WaitingTime;
                    totalResponseTime += process.GetResponseTime();
                    totalCpuBurstTime += process.ServiceTime;
                    totalTurnarountTime += process.GetTurnarountTime();
                    trts += process.TurnarountTime / process.ServiceTime;
                }

                double cpuUtilization = (double)totalCpuBurstTime / finishtime * 100;
                double throughput = (double)processes.Count / processes.Last().TerminationTime;

                AnalysisTextBlock.Text = 
                                        $"CPU Utilization: {cpuUtilization:F2}%\n" +
                                        $"Finish time: {finishtime}\n" +
                                        $"Throughput: {throughput:F2} processes/sec\n" +
                                        $"Average Waiting Time: {(double)totalWaitingTime / processes.Count:F2}\n" +
                                        $"Average Turn around Time: {(double)totalTurnarountTime / processes.Count:F2}\n" +
                                        $"Average Response Time: {(double)totalResponseTime / processes.Count:F2}\n" +
                                        $"Total Tr/Ts: {(double)trts}\n" +
                                        $"Average Tr/Ts: {(double)trts / processes.Count:F2}\n" +
                                        $"Total Turnarount Time:{totalTurnarountTime}\n";
           
        }
    }
}