FibonacciExchange
=================
###Logic
Two applications communicate with each other through transports, implementing the calculation of Fibonacci numbers.
Each application performs one step of calculating the number.

Logic for calculating one sequence is as follows:
- The first initializes the calculation
- The first sends the second N_I
- The second calculates N_(I + 1) = N_(I - 1) + N_I and sends the result back
- The first calculates N_(I + 2) = N_(I + 1) + N_I
- The logic is repeated

Features:
The first application at startup takes a parameter - an integer number of asynchronous calculations to begin.

All calculations are parallel.
Data transfer from 1 to 2 goes through Rest WebApi.
Data transfer from 2 to 1 goes through MessageBus.
It is important that only one of the numbers is passed, the previous is not transfered (technical information is not considered).

Used Technologies:
- ASP.NET WebApi
- RestSharp
- MassTransit Bus
- Log4Net
- StructureMap
