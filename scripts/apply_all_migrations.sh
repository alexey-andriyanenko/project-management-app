#!/bin/sh

find ../ -name "*DataAccess" -type d | while read migrationProjectPath; do
   dotnet ef database update --project $migrationProjectPath --startup-project $migrationProjectPath -v
done