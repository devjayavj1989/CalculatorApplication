# Calculator Application
## Project Description
Host and runs a simple caluclator app with login module.

## Clone the git repo
Use git clone using ssh or https to clone the repo to your local

## Prerequisite
Uses docker to run. Please install docker following the documentation here: https://docs.docker.com/desktop/

## How to run? 

### Enter the following command to build the docker image
```
docker build -t calc-app .
```
### Run docker image
```
docker -run -dp 8080:5000 calc-app
```

### Launch app
```
http://localhost:8080
```


