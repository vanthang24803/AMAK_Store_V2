#!/bin/bash


echo "Docker compose dev stop! 🦄🦄🦄🦄🦄"

cd ../

docker-compose -f ./Docker/docker-compose.dev.yml down

echo "Docker compose dev stop successfully! ✔️✔️✔️✔️✔️"