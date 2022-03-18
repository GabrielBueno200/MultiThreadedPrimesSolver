# Multithreaded Primes Solver

<p align="center">
  <img alt="GitHub language count" src="https://img.shields.io/github/languages/count/GabrielBueno200/MultiThreadedPrimesSolver">

  <img alt="GitHub repo size" src="https://img.shields.io/github/repo-size/GabrielBueno200/MultiThreadedPrimesSolver">
  
  <a href="https://github.com/GabrielBueno200/monty-hall">
    <img alt="GitHub last commit" src="https://img.shields.io/github/last-commit/GabrielBueno200/MultiThreadedPrimesSolver">
  </a>
  
   <img alt="GitHub" src="https://img.shields.io/github/license/GabrielBueno200/MultiThreadedPrimesSolver">
</p>

<!-- PROJECT LOGO -->
<br />
<p align="center">
  <a href="#">
    <img src="https://www.mathcad.com/-/media/Images/Blog/post/mathcad-blog/2020/june/prime-numbers-featured.png?h=450&w=900&la=en&hash=AD76300A2E6C4BEE2BD38267395748B2" alt="Logo" width="550">
  </a>
</p>

<p align="center">
  <img alt="C#" src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white"/>
  <img alt=".NET" src="https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white"/>
</p>



<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#-about-the-project">About The Project</a>
    </li>
    <li>
      <a href="#-how-to-run">How To Run</a>
    </li>
  </ol>
</details>


<!-- ABOUT THE PROJECT -->
## 💻 About The Project - Multithread Prime Solver
C# / .NET 5 console application that simulates differents scenarios to solve the amount of prime numbers in a numeric dataset, using threaded and serial approaches to do it.

## GUI
In the main window you can choose some graphs options based in different solving approaches.

![image](https://user-images.githubusercontent.com/56837996/158877974-97b05c10-587a-4be6-bcb5-ef3192f1eb7b.png)

You can choose the options below:

- 1. Serial Solution X Threaded Solution: this option will execute a range starting in 1 (serial solution) until an input thread amount, iterating this range and running the program with a thread amount relative to the current iteration.

- 2. Thread Amount X Execution Time: this option will ask the user three inputs, the number of times that the solution will be executed relative to the two other inputs, a lower thread amount and a higher thread amount.

- 3. Thread Amount X SpeedUp: this option will run the program 50 for each thread amount in a range from 2 to 452 threads.

## Graphs
- Threads Amount X SpeedUp (Error bar chart): generated after the option 3 execution 

![image](https://user-images.githubusercontent.com/56837996/158878017-5f5d338b-4d38-49b7-983b-8014bdf2356c.png)
![image](https://user-images.githubusercontent.com/56837996/159025515-1dbe74cb-2ada-4caf-8dc4-00e185651813.png)

- Threads Amount X SpeedUp (Line Bar Chart): generated after the option 3 execution

The main difference to the previous chart is the speedup error, calculated using standard deviation.

![image](https://user-images.githubusercontent.com/56837996/158878199-f943ed22-f0c2-4468-b3b3-6c34ba89efc8.png)
![image](https://user-images.githubusercontent.com/56837996/159025584-0f10e165-0415-4923-9407-33d2703dc492.png)

- Thread X Time (Line chart): generated after the option 2 execution
![image](https://user-images.githubusercontent.com/56837996/159028122-8d347fd6-7db5-4c69-8942-5e0edd0e0e3f.png)

- Comparison between none, few and many threads X Executed Time (Bar Chart): generated after option 1 execution
![image](https://user-images.githubusercontent.com/56837996/159028496-a401782d-9bfd-4e71-b193-7904008851c8.png)

## Serial Fraction
Based in the best execution case, we use the amount of threads, the speedup and the execution time of this case to calculate the serial fraction

![image](https://user-images.githubusercontent.com/56837996/158878228-f25d8144-5226-4cd6-8896-7261c4fc94c2.png)


## ❗ Requirements
* .NET 5 sdk or later.

<!-- HOW TO RUN -->
## 🚀 How To Run
 
First, run the commands below:

```bash
dotnet run build
# or
dotnet run
```

The GUI will open and ready to be used.
