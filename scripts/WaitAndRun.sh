#!/usr/bin/env sh

set -e
set -x

until [ ]; do
  dotnet "$1" && break
  sleep 1
done

until [ ]; do
  sleep 30
  curl -f "http://selenium-hub:4444/wd/hub/status" && break
done

dotnet test --logger "console;verbosity=detailed"

dotnet tool install --global Specflow.Plus.LivingDoc.CLI
export PATH="$PATH:/root/.dotnet/tools"
echo "Starting Living Doc report generation"
livingdoc test-assembly "src/EATestBDD/bin/Debug/net6.0/EATestBDD.dll" -t "src/EATestBDD/bin/Debug/net6.0/TestExecution.json"
