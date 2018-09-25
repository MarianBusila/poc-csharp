docker build -t aspnetcore_docker .
docker run -d -p 8080:80 --name mytestapp aspnetcore_docker