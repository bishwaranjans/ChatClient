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

 - **ChatClient.Console** : It is console based user interface of our solution signifying starting of the application and further processing for identifying a user and receiving as well as displaying messages based on the subscribed "Subject". This is the entry block of our program.
 - **ChatClient.Domain** : Responsible for representing concepts of the business, information about the business situation, and business rules. State that reflects the business situation is controlled and used here, even though the technical details of storing it are delegated to the infrastructure. This layer is the heart of our solution.
 - **ChatClient.Infrastructure** : Responsible for how the data that is initially held in domain entities (in memory). It contains all our publishing and subscribing logic.
 - **ChatClient.Api** : Responsible for providing HTTP and HTTP POST based messaging services.

![alt text](https://github.com/bishwaranjans/ChatClient/blob/master/Documentation/Architecture.png)
 ## Design Patterns
 
Facade and Factory design patterns has been incorporated to design the application. The primary focus was to use the Publish and Subscribe across end user applications i.e. Console and Api. Basic SOLID design patterns has been followed wherever possible. 

## Configuration

appsettings.json file is being used for customizing NATSServerUrl and NATSSubject. Both Api and Console supports the customization.

