#!/bin/bash

cd ../Src/AMAK.Infrastructure 

dotnet ef --startup-project ../AMAK.API  database update