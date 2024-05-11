FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MedRecordManager/MedRecordManager.csproj", "MedRecordManager/"]
COPY ["AdvancedMDService/AdvancedMDService.csproj", "AdvancedMDService/"]
COPY ["AdvancedMDDomain/AdvancedMDDomain.csproj", "AdvancedMDDomain/"]
COPY ["UrgentCareData/UrgentCareData.csproj", "UrgentCareData/"]
COPY ["CucmsCommon/CucmsCommon.csproj", "CucmsCommon/"]
COPY ["ExpressionBuilder/ExpressionBuilder.csproj", "ExpressionBuilder/"]
COPY ["PracticeVelocityDomain/PracticeVelocityDomain.csproj", "PracticeVelocityDomain/"]
RUN dotnet restore "./MedRecordManager/./MedRecordManager.csproj"
COPY . .
WORKDIR "/src/MedRecordManager"
RUN dotnet build "./MedRecordManager.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MedRecordManager.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MedRecordManager.dll"]