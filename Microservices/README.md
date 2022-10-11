## Overview

Implemenation of 2 microservices using HTTP and GRPC APIs, asyncronous messaging with RabbitMQ, deployed on Kubernetes. Based on this [.NET Microservices – Full Course](https://www.youtube.com/watch?v=DgVjEo3OGBI)

- Building two .NET Microservices using the REST API pattern
- Working with dedicated persistence layers for both services
- Deploying our services to Kubernetes cluster
- Employing the API Gateway pattern to route to our services
- Building Synchronous messaging between services (HTTP & gRPC)
- Building Asynchronous messaging between services using an Event Bus (RabbitMQ)

## Architecture

![](images/Architecture.png)
![](images/PlatformService.png)
![](images/CommandService.png)
![](images/Kubernetes.png)

## Setup

### Docker

The images will be pushed to Docker Hub.

- build platform service image

```
docker build -t YOUR_DOCKERHUB_USER/platformservice .
```

- run platform service

```
docker run -p 8080:80 -d YOUR_DOCKERHUB_USER/platformservice
```

- push image to Docker Hub

```
docker push YOUR_DOCKERHUB_USER/platformservice
```

### Kubernetes

- install and start minikube

- deploy platform service

```
kubectl apply -f platforms-depl.yaml
```