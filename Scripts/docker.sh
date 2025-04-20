#!/bin/bash

IMAGE_NAME=$1

cd ../

docker build -t $IMAGE_NAME -t $IMAGE_NAME:latest .

echo "Build ${IMAGE_NAME} image successfully! ✔️✔️✔️✔️✔️"
