# SouthWestTraders
# Code Coverage
Install package: coverlet.collector

install package: ReportGenerator

`dotnet tool install -g dotnet-reportgenerator-globaltool`

`dotnet test --collect:"XPlat Code Coverage" --results-directory:"./.coverage"`

`reportgenerator "-reports:.coverage/**/*.cobertura.xml" "-targetdir:.coverage-report/" "-reporttypes:HTML;"`

