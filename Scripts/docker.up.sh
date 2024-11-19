#!/bin/bash


echo "Docker compose dev start! 🦄🦄🦄🦄🦄"

cd ../

docker-compose -f ./Docker/docker-compose.dev.yml up -d

echo "Docker compose dev start successfully! ✔️✔️✔️✔️✔️"