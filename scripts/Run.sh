#!/usr/bin/env sh

set -e
set -x

project="e2etest"

cd "$(dirname "${0}")/.."

export COMPOSE_HTTP_TIMEOUT=200

docker-compose -p "$project" build

mkdir -m 777 "reports"

docker-compose -p "$project" up -d ea_api ea_webapp db selenium-hub firefox chrome
docker-compose -p "$project" up --no-deps ea_test

docker cp ea_test: src/EETestBDD/LivingDoc.html ./reports
echo "Specflow Living Doc report is copied to ./reports"
ls -l ./reports

exit_code=$(docker inspect ea_test -f '{{ .State.ExitCode }}')

if [ $exit_code -eq 0 ]; then
  exit $exit_code
else
  echo "Test Failed"
fi
