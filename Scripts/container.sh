#!/bin/bash

if [ $# -lt 1 ]; then
  echo "Error: No container names provided. Usage: ./container.sh <container1> [params...]"
  exit 1
fi

declare -A images
images=(
  ["pg"]="postgres:16"
  ["pgadmin4"]="dpage/pgadmin4"
  ["redis"]="redis:latest"
)

setup_pg() {
  local db_name="Amak"
  local user="postgres"
  local pass="anhthang123"

  while [ "$#" -gt 0 ]; do
    case "$1" in
      --db-name) db_name="$2"; shift 2 ;;
      --user) user="$2"; shift 2 ;;
      --pass) pass="$2"; shift 2 ;;
      *) shift ;;
    esac
  done

  echo "Configuring PostgreSQL with DB: $db_name, User: $user, Pass: $pass"
}

setup_pgadmin4() {
  local email="admin@example.com"
  local pass="admin123"

  while [ "$#" -gt 0 ]; do
    case "$1" in
      --email) email="$2"; shift 2 ;;
      --pass) pass="$2"; shift 2 ;;
      *) shift ;;
    esac
  done

  echo "Configuring pgAdmin4 with Email: $email, Pass: $pass"
}

while [ "$#" -gt 0 ]; do
  CONTAINER_NAME=$1
  shift

  if [ -n "${images[$CONTAINER_NAME]}" ]; then
    echo "Pulling image for ${CONTAINER_NAME} (${images[$CONTAINER_NAME]})..."
    docker pull "${images[$CONTAINER_NAME]}"

    case "$CONTAINER_NAME" in
      "pg") setup_pg "$@" ;;
      "pgadmin4") setup_pgadmin4 "$@" ;;
      "redis") echo "No additional configuration for Redis" ;;
      *) echo "Error: Unknown container name ${CONTAINER_NAME}. No image found." ;;
    esac
  else
    echo "Error: Unknown container name ${CONTAINER_NAME}. No image found."
  fi
done
