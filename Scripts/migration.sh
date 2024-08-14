#!/bin/bash

MIGRATION_NAME=$1

cd Src/AMAK.Infrastructure

dotnet ef --startup-project ../AMAK.API migrations add $MIGRATION_NAME

echo "Migarton ${MIGRATION_NAME} successfully! ✔️✔️✔️✔️✔️"
