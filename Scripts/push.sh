#!/bin/bash

COMMIT_NAME="$1"

echo "Push commit ${COMMIT_NAME} 🙀🙀🙀🙀🙀"

sh build.sh

sh test.sh

cd ../

echo "Add commit ${COMMIT_NAME} 💀💀💀💀💀"
git add .

git commit -m "${COMMIT_NAME}"

echo "Pushing Github ${COMMIT_NAME} 🦄🦄🦄🦄🦄"
git push 

echo "Pushing Gitlab ${COMMIT_NAME} 🦊🦊🦊🦊🦊"
git push gitlab

echo "Pushing Azure ${COMMIT_NAME} 🐸🐸🐸🐸🐸🐸"
git push azure

echo "Commited ${COMMIT_NAME} successfully! ✔️✔️✔️✔️✔️"
