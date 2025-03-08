FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
COPY . /source
WORKDIR /source/Zoo_Management_And_Staff_Operations_System
ARG TARGETARCH
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish -a ${TARGETARCH/amd64/x64} --use-current-runtime --self-contained false -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app
COPY --from=build /app .
RUN ls -la /app  # Kiểm tra các tệp trong /app
USER $APP_UID
ENTRYPOINT ["dotnet", "Zoo_Management_And_Staff_Operations_System.dll"]