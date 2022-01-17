#FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim AS base
FROM mcr.microsoft.com/dotnet/runtime:5.0.404-focal AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0.404-focal AS build
WORKDIR /src/..
COPY ["PlaywrightSharp.csproj", "PlaywrightSharp/"]
RUN dotnet restore "PlaywrightSharp/PlaywrightSharp.csproj"
COPY . "/src/PlaywrightSharp"
WORKDIR "/src/PlaywrightSharp"
RUN apt-get update -yq && apt-get upgrade -yq && apt-get install -yq curl git nano
RUN curl -sL https://deb.nodesource.com/setup_16.x | bash - && apt-get install -yq nodejs build-essential
RUN npm install -g npm

RUN apt-get install -y wget xvfb unzip

# Set up the Chrome PPA
RUN wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub | apt-key add -
RUN echo "deb http://dl.google.com/linux/chrome/deb/ stable main" >> /etc/apt/sources.list.d/google.list

# Update the package list and install chrome
RUN apt-get update -y
RUN apt-get install -y google-chrome-stable
RUN apt-get install -y firefox

#RUN dotnet add package Microsoft.Playwright
RUN dotnet build "PlaywrightSharp.csproj" -c Release -o /app/build
RUN npx playwright install-deps
RUN npx playwright install

# RUN dotnet test --no-build

FROM build AS testrunner
WORKDIR "/src/PlaywrightSharp"
CMD ["dotnet", "test", "--no-restore", "--settings:Chrome.runsettings"]