FibonacciExchange
=================
###Logic
Two applications communicate with each other through transports, realizing the calculation of Fibonacci numbers.
Each application performs one step of calculating the number.

Logic for calculating one sequence is as follows:
- The first initializes the calculation.
- The first sends the second N_I
- The second calculates N_ (I + 1) = N_ (I - 1) + N_I and sends the result back.
- Calculates first N_ (I + 2) = N_ (I + 1) + N_I.
- The logic is repeated

Features:
The first application startup takes a parameter - an integer number of asynchronous calculations begin.

All calculations are parallel.
Data transmission from 1 to 2 goes through Rest WebApi
Data transmission from 2 to 1 is by MessageBus.
It is important that passed only one of the numbers, the previous is not transmitted (technical information does not count).

Featured Technology:
- ASP.NET WebApi
- RestSharp
- MassTransit Bus
- Log4Net
- StructureMap
