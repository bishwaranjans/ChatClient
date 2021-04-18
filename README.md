# ChatClient
## Messaging based chat client 

Developed a messaging-based chat client which interacts with other clients through the NATS messaging system (See links below). 

### Part 1 
Write a simple chat client which accepts messages from a user and sends these to a NATS subject using the PUB/SUB pattern. The client should also receive messages that comes in on the subject and 
output these to the console. Each message should be tagged with the username of the client that sent them and a timestamp. 

### Part 2 
Add a REST API (GET) where the client returns the messages that has been received (since the client executable was started, no need for storage) 

### Part 3 
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

### TODO:
Invoke the REST API from client console
- Get all received messages
- post a new message
