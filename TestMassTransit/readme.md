# Mass Transit Test Application

## Getting started

1. start rabbitmq docker container:

```
docker-compose up
```

## Notes

### Mediator
- runs in-process and in-memory, no transport is required. 
- messages are passed by reference, instead than being serialized

### RabbitMQ
- when running a producer / consumer scenario, make sure consumers are run first, otherwise messages produced will be lost
- use IRequestClient, when a response is expeted. RabbitMQ will create a temporary queue to transimit the response
- use ISendEndpoint just to send a message without expecting a response