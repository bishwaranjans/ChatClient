# ChatClient: Messaging based chat client 
The Messaging based chat client application has been designed inline with SOA and Domain Driven Design(DDD) to provide end user a console application as well as API endpoints to  interacts with other clients through the NATS messaging system (See links below). 

## Implementation Requirement:
### Task: Part 1 
Write a simple chat client which accepts messages from a user and sends these to a NATS subject using the PUB/SUB pattern. The client should also receive messages that comes in on the subject and 
output these to the console. Each message should be tagged with the username of the client that sent them and a timestamp. 

### Task: Part 2
Add a REST API (GET) where the client returns the messages that has been received (since the client executable was started, no need for storage) 

### Task: Part 3 
Add a REST API (POST) which accepts a message string in the body of the request and forwards it to the subject used in part 1. 

### Links
- NATS server executable 
  - https://nats.io/download/nats-io/gnatsd/
- NATS documentation 
  - https://nats.io/documentation/writing_applications/concepts/
- NATS clients 
  - https://github.com/nats-io/nats.go
  - https://github.com/nats-io/nats.net
  - https://nats.io/download/nats-io/cnats

## Solution Architecture:

DDD approch has been used for designing the architecture of the solution by clearly segregating the each responsibility with clear structure. Our client is segregated into two part i.e. Console and REST Api. The console part covers the Task 1 and REST API covers Task 2 and Task 3.

> **Note:** This project was built targeting .NET 5.0 (now end-of-life). It is recommended to upgrade to .NET 8 LTS for continued support. VS 2022 or later is recommended.

 - **ChatClient.Console** : It is console based user interface of our solution signifying starting of the application and further processing for identifying a user and receiving as well as displaying messages based on the subscribed "Subject". This is the entry block of our program.
 - **ChatClient.Domain** : Responsible for representing concepts of the business, information about the business situation, and business rules. State that reflects the business situation is controlled and used here, even though the technical details of storing it are delegated to the infrastructure. This layer is the heart of our solution.
 - **ChatClient.Infrastructure** : Responsible for how the data that is initially held in domain entities (in memory). It contains all our publishing and subscribing logic.
 - **ChatClient.Api** : Responsible for providing HTTP and HTTP POST based messaging services. Windows authetication has been enabled so that we can know which machine user is trying to run from the browser or POSTman.

![alt text](https://github.com/bishwaranjans/ChatClient/blob/master/Documentation/Architecture.png)
 ## Design Patterns
 
Facade and Factory design patterns has been incorporated to design the application. The primary focus was to use the Publish and Subscribe across end user applications i.e. Console and Api. Basic SOLID design patterns has been followed wherever possible. 

## Configuration

appsettings.json file is being used for customizing NATSServerUrl and NATSSubject. Both Api and Console supports the customization.

## Prerequisites

Before running the solution, ensure you have the following installed:

- [.NET 5 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/5.0) (or .NET 8 SDK if you have upgraded)
- A running **NATS server** on `localhost:4222`

The quickest way to start a NATS server locally is via Docker:

```bash
docker run -p 4222:4222 nats
```

Alternatively, download the NATS server binary from https://nats.io/download/ and run:

```bash
nats-server
```

## Running Locally

### Console client

```bash
# From the repo root
dotnet run --project ChatClient.Console/ChatClient.Console.csproj
```

Enter your username when prompted, then type messages and press Enter to send. Type `stop` to exit.

### REST API

```bash
dotnet run --project ChatClient.Api/ChatClient.Api.csproj
```

Swagger UI will be available at `https://localhost:5001/swagger`.

## Usage: How to use.

### NATS Server Ready.

To proceed to use the solution, make sure that NATS server is ready and running.
![alt text](https://github.com/bishwaranjans/ChatClient/blob/master/Documentation/NATSServerReady.PNG)

### Making solution ready in VS 2019

As our solution is developed using .NET 5, we need VS 2019 to open it. 
![alt text](https://github.com/bishwaranjans/ChatClient/blob/master/Documentation/SolutionInVS2019.PNG)

### Task 1: Demo
- For Task 1, we need to use ChatClient.Console.exe
- We can directly start it from VS 2019 or after building the solution in VS 2019, open the exe file and start running.
- It will first ask the user to provide a name. After giving a name, the user can enter any text. To stop, user has to enter "stop" text. 
- Once user type any text and click enters, it will publish that message to the NATS's subject. As the console is also subscribed to that NATSSubject, the subscribe message will be displayed with the user name and time stamp.
- Simultaneously we can start multiple ChatClient.Console.exe and each time a new user name be specified. This way we can identify who is using our console client. 

![alt text](https://github.com/bishwaranjans/ChatClient/blob/master/Documentation/Task1_Chat.PNG)

### Task 2: Demo
- For Task 2, we need to use ChatClient.Api
- Start the ChatClient.Api
- We can use the Swagger feature to test out the HTTP GET
- HTTP GET method can be used for getting all the client's received messages

![alt text](https://github.com/bishwaranjans/ChatClient/blob/master/Documentation/Task2_GetAllMessages.PNG)

### Task 3: Demo
- For Task 3 also, we need to use ChatClient.Api
- Start the ChatClient.Api if not started
- We can use the Swagger feature to test out the HTTP POST
- HTTP POST method can be used for posting a new string message 

![alt text](https://github.com/bishwaranjans/ChatClient/blob/master/Documentation/Task3_PostMessage.PNG)

