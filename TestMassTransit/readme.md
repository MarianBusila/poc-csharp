# Mass Transit Test Application

## Getting started

1. start dependency docker containers (rabbitmq, redis):

```
docker-compose up
```

2. start one or multiple instance of the Service app
```
cd Sample.Service
dotnet run
```

3. start the API
```
cd Sample.Api
dotnet run
```

## Notes

### Mediator
- runs in-process and in-memory, no transport is required. 
- messages are passed by reference, instead than being serialized

### RabbitMQ
- when running a producer / consumer scenario, make sure consumers are run first, otherwise messages produced will be lost
- use IRequestClient, when a response is expeted. RabbitMQ will create a temporary queue to transimit the response
- use ISendEndpoint just to send a message without expecting a response

### Sagas
 - A saga is a long-lived transaction managed by a coordinator. 
 - Sagas are initiated by an event, sagas orchestrate events, and sagas maintain the state of the overall transaction. 
 - Sagas are designed to manage the complexity of a distributed transaction without locking and immediate consistency. 
 - the best way to implement sagas is with a state machine