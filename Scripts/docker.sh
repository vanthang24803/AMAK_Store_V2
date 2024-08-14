#!/bin/bash

IMAGE_NAME=$1

cd ../

docker build  -t $IMAGE_NAME .

echo "Build ${MIGRATION_NAME} image successfully! ✔️✔️✔️✔️✔️"
