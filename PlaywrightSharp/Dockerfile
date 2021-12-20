FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base

RUN apt-get update && apt-get install -y --no-install-recommends \
    xvfb

RUN apt-get update && apt-get install -y --no-install-recommends \
    fonts-liberation\
    libasound2\
    libatk-bridge2.0-0\
    libatk1.0-0\
    libatspi2.0-0\
    libcairo2\
    libcups2\
    libdbus-1-3\
    libdrm2\
    libgbm1\
    libglib2.0-0\
    libgtk-3-0\
    libnspr4\
    libnss3\
    libpango-1.0-0\
    libx11-6\
    libxcb1\
    libxcomposite1\
    libxdamage1\
    libxext6\
    libxfixes3\
    libxrandr2

 RUN apt-get update && apt-get install -y npm && \
     npm i playwright-chromium@1.14.1 && \
     npm i playwright-firefox@1.14.1 && \
     npm i playwright-webkit@1.14.1

RUN apt-get remove nodejs -y



WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["PlaywrightSharp/PlaywrightSharp.csproj", "PlaywrightSharp/"]
RUN dotnet restore "PlaywrightSharp/PlaywrightSharp.csproj"
COPY . .
WORKDIR "/src/PlaywrightSharp"
RUN dotnet build "PlaywrightSharp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet test "PlaywrightSharp.csproj" -c Release
RUN dotnet publish "PlaywrightSharp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PlaywrightSharp.dll"]