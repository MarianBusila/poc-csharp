# Getting started

## Run neo4j docker container

```
$neo4j_dir = "C:/work/neo4j"

mkdir $neo4j_dir/plugins
Invoke-WebRequest `
  -Uri https://github.com/neo4j-contrib/neo4j-apoc-procedures/releases/download/4.1.0.2/apoc-4.1.0.2-all.jar `
  -OutFile $neo4j_dir/plugins/apoc-4.1.0.2-all.jar
docker run -d --name neo4j `
  -p=7474:7474 -p=7687:7687 `
  -v $neo4j_dir/data:/data -v $neo4j_dir/plugins:/plugins `
  -e NEO4J_apoc_export_file_enabled=true `
  -e NEO4J_apoc_import_file_enabled=true `
  -e NEO4J_apoc_import_file_use__neo4j__config=true `
  -e NEO4J_dbms_security_procedures_unrestricted=apoc.\\\* `
  neo4j:4.1.3
```

## Load movie database
- go to http://localhost:7474/browser/
- set the new password for neo4j user
- execute 
```
:play movie-graph
```

##  Execute console application
- update appsettings.json with the correct password
- the application performs simple queries in the database

## Cleanup data (optional)

```
MATCH (n) DETACH DELETE n
```