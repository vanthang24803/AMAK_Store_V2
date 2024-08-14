#!/bin/bash

COMMIT_NAME="$1"

echo "Push Commit ${COMMIT_NAME}"

cd ../

git add .

git commit -m "${COMMIT_NAME}"

git push 
git push gitlab