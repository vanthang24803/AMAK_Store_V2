#!/bin/bash

COMMIT_NAME="$1"

echo "Push commit ${COMMIT_NAME} 🙀🙀🙀🙀🙀"

cd ../

echo "Add commit ${COMMIT_NAME} 💀💀💀💀💀"
git add .

git commit -m "${COMMIT_NAME}"

echo "Pushing ${COMMIT_NAME} 🦄🦄🦄🦄🦄"
git push 
git push gitlab

echo "Commited ${COMMIT_NAME} successfully! ✔️✔️✔️✔️✔️"
