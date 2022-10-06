## Overview

A GraphQL API with .NET 5 and Hot Chocolate based on this [Youtube tutorial](https://www.youtube.com/watch?v=HuN94qNwQmM)

## Graph QL core concepts

![](images/CoreConcepts.png)

## Graph QL vs REST

### Overfetching

![](images/Overfetching.png)

### Underfetching

![](images/Underfetching.png)

### When to use

![](images/Usage.png)

## Application architecture

![](images/ApplicationArchitecture.png)

## Setup

- start sql server

```
docker-compose up -d
```

- install _dotnet ef_

```
dotnet tool install --global dotnet-ef --version 5.0.17
```

- apply migrations

```
dotnet ef database update
```

- run application

```
dotnet run
```

## Test
- access endpoint: http://localhost:5000/graphql. This will open the _Banana Cake Pop_ UI. Alternatively, Postman or Insomnia can be used
- access endpoint: http://localhost:5000/graphql-voyager. This will display the schema.

### Query
- run query to get platforms

```
query {
	platform {
		id
		name
		commands {
			id
			howTo
			commandLine
		}
	}
}
```

- run query to get commands

```
query {
	command {
		id
		howTo
		commandLine
		platform {
			name
		}
	}
}
```

- run query with filter

```
query {
	command(where: {platformId: {eq: 1}}) {
		id
		howTo
		commandLine
		platform {
			name
		}
	}
}
```

- run query with sorting

```
query {
	platform(order: {name: DESC}) {
		name
	}
}

```

### Mutation
- add a platform using a mutation

```
mutation {
	addPlatform(input: {
		name: "Ubuntu"
	})
	{
		platform {
		id
		name
	}
	}
}
```

- add a command using a mutation

```
mutation {
	addCommand(input: {
		howTo: "List files in a folder"
		commandLine: "ls"
		platformId: 5
	})
	{
		command {
			id
			howTo
			commandLine
			platform {
				name
			}
		}
	}
}
```

### Subscription
- go to http://localhost:5000/graphql. This opens the Banana Cake Pop UI.
- create subscription
```
subscription {
  onPlatformAdded {
    id
    name
  }
}
```

- add a new platform
```
mutation {
	addPlatform(input: {
		name: "RedHat"
	})
	{
		platform {
		id
		name
	}
	}
}
```
- you should see in the Banana Cake Pop UI, a message that platform was added, like:
```
{
  "data": {
    "onPlatformAdded": {
      "id": 6,
      "name": "RedHat"
    }
  }
}
```