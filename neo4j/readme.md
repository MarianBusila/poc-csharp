[Graph Data Modeling for Neo4j](#graph-data-modeling-for-neo4j)

# Graph Data Modeling for Neo4j

## Introduction to Graph Data Modeling

- **Nodes** should have **labels** to categorize entities
- **Nodes** and **relationships** can have **properties**
- [Arrow Tool](http://www.apcjones.com/arrows/) to model a graph database

![Example](./docs/Person.svg)

## Designing the Initial Graph Data Model
    1. Understanding the domain
    2. Create high-level sample data
    3. Define specific questions for the application
    4. Identify entities
    5. Identify connections between entities
    6. Test the questions against the model
    7. Test scalability

## Graph Data Modeling Core Principles

### Best practices for modeling nodes
 - Uniqueness of nodes
 - Use fanout judiciously for complex data. use fanout to do one of two things:
    - Reduce duplication of properties. Instead of having a repeated property on every node, you can instead have all of those nodes connected to a shared node with that property. This can make data updates massively easier.
    - Reduce gather-and-inspect behavior during a traversal to reduce "wasted" hops

### Best practices for modeling relationships    
- Using specific relationship types.
- Reducing symmetric relationships.
- Using types vs. properties.

### Property best practices
 - Property lookups have a cost.
- Parsing a complex property adds more cost.
- Anchors and properties used for traversal should be as simple as possible.
- Identifiers, outputs, and decoration are OK as complex values.

### Hierarchy of accessibility

1. Anchor node label, indexed anchor node properties
2. Relationship types
3. Non-indexed anchor node properties
4. Downstream node labels
5. Relationship properties, downstream node properties

Query performance is not the only metric that matters! Query simplicity, write/update speed, and the human-intuitiveness of a model are also important factors.

## Common Graph Structures

- Intermediate nodes
    - to model a relationship that connects more than two nodes
    - sharing data
    - organizing data

- Linked lists
    - never use a doubly-linked list in Neo4j because doubly-linked lists use redundant symmetrical relationships.

- Timeline trees
    - each node contains the Year, Month, Day
- Multiple structures in a single graph

## Refactoring and Evolving a Model

Data models can be optimized for one of four things:

- Query performance
- Model simplicity & intuitiveness
- Query simplicity (i.e., simpler Cypher strings)
- Easy data updates

| Goal | Refactor example |
| ----------- | ----------- |
| Eliminate duplicate data in properties | Extracting nodes from properties |
| Use labels instead of property values | Turn property values into labels for nodes |
| Use nodes instead of properties for relationships | Extract nodes from relationship properties |

