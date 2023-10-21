# Build the Docker image
docker build -t leeklog .

# Run the Docker container
docker run -d -p 8080:80 leeklog
