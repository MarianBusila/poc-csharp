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

