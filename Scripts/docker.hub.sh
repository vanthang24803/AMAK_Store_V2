#!/bin/bash


cd ../

docker build -t vanthang24803/amak-api-prod:latest .

echo "✅ Build image successfully!"
echo "Please enter your Docker Hub password:"
docker login 

echo "✅ Docker login successfully!"

docker push vanthang24803/amak-api-prod:latest

echo "✅ Docker push successfully!"

echo "🧹 Cleaning up local image..."
docker rmi vanthang24803/amak-api-prod:latest

echo "✅ Local image removed successfully!"