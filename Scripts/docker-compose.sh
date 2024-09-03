#!/bin/bash

ENV=${1:-dev}
BUILD_OPTION=""

if [[ "$ENV" != "dev" && "$ENV" != "prod" ]]; then
    echo "Invalid environment specified. Use 'dev' or 'prod'."
    exit 1
fi

if [ "$2" == "--build" ]; then
    BUILD_OPTION="--build"
fi

echo "Docker-compose for $ENV environment 🎉🎉🎉🎉🎉"

cd ../docker

docker-compose -f docker-compose.${ENV}.yml up $BUILD_OPTION
