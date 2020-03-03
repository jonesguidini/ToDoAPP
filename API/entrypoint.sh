#!/bin/bash

exec "dotnet ef database update"

# set -e
# run_cmd="dotnet ef database update"
# echo "aeeee"

# until dotnet ef database update; do
# >&2 echo "MySQL Server is starting up"
# sleep 1
# done

# >&2 echo "SQL Server is up - executing command"
# exec $run_cmd