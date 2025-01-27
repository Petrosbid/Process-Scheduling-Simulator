# Process-Scheduling-Simulator
An operating systems process control block simulator including six process scheduling algorithms in single partition or multiple partitions memory for one CPU and one core.

This project is a Process Scheduling Simulator that allows you to test and analyze various process scheduling algorithms. It is developed using C# and the WPF framework.
## Features

- **Support for Multiple Scheduling Algorithms**:
  - FCFS (First-Come, First-Served)
  - RR (Round Robin)
  - SPN (Shortest Process Next)
  - SRT (Shortest Remaining Time)
  - HRRN (Highest Response Ratio Next)
  - FeedBack (Multilevel Feedback Queue)

- **Process Management**:
  - Dynamically add and remove processes.
  - Set arrival time, service time, and process size.

- **Algorithm Analysis**:
  - Display metrics such as waiting time, response time, turnaround time, and more.
  - Visualize scheduling results with charts.

- **Multiple Queue Support**:
  - Use multiple queues to manage processes in partitioned memory.

![Screenshot 2025-01-27 230106](https://github.com/user-attachments/assets/864c7b33-4ea9-44b0-b182-030103154a4c)
![Screenshot 2025-01-27 230124](https://github.com/user-attachments/assets/1107ec48-3908-4e65-902f-e4d1813fdac7)

## How to Run the Project

1. **Prerequisites**:
   - .NET Framework 4.7.2 or higher.
   - Visual Studio 2019 or higher.

2. **Clone the Repository**:
   ```bash
   git clone https://github.com/your-username/process-scheduling-simulator.git
   ```
3. **Run the Project:**
   - Open the project in Visual Studio.
   - Set MainWindow.xaml as the startup file.
   - Run the project (F5).
## File Descriptions
### MainWindow.xaml
This file defines the user interface of the project. It includes:
  - Algorithm Selection: Users can choose the desired scheduling algorithm.
  - Process Input Table: Users can manually input processes.
  - Run and Process Management Buttons: Includes "Run", "Add Process", and "Delete Process" buttons.
  - Algorithm Analysis: Displays performance metrics of the algorithms.
  - Result Chart: Visualizes the scheduling results.
### MainWindow.xaml.cs
This file implements the logic behind the user interface. It includes:
  - Handling button events.
  - Executing scheduling algorithms.
  - Displaying results and analysis.
### MemoryPartition.cs
This file defines the MemoryPartition class, which is used to manage memory partitions and process queues.

#### Process.cs
This file defines the Process class, which contains information about each process, such as arrival time, service time, waiting time, etc.

#### Queue.cs
This file defines the Queue class, which is used to manage process queues. It includes functions for adding, removing, and searching processes.

#### Scheduling Algorithms
  - FCFS.cs: Implementation of the First-Come, First-Served algorithm.
  - RR.cs: Implementation of the Round Robin algorithm.
  - SPN.cs: Implementation of the Shortest Process Next algorithm.
  - SRT.cs: Implementation of the Shortest Remaining Time algorithm.
  - HRRN.cs: Implementation of the Highest Response Ratio Next algorithm.
  - FeedBack.cs: Implementation of the Multilevel Feedback Queue algorithm.

### How to Use
  1. Add Processes:

Use the process input table to add new processes with the desired specifications.

  2. Select Algorithm:

Choose the scheduling algorithm from the ComboBox.

  3. Run Simulation:

Click the "Run" button to execute the simulation with the selected algorithm.

  4. View Results:

The simulation results, including waiting time, response time, turnaround time, and other metrics, will be displayed in the algorithm analysis section.

  5. The results will also be visualized in a chart.

### Contributing
If you would like to contribute to this project, please follow these steps:

  1. Fork the project.

  2. Create a new branch (git checkout -b feature/YourFeatureName).

  3. Commit your changes (git commit -m 'Add some feature').

  4. Push your changes to the branch (git push origin feature/YourFeatureName).
      
