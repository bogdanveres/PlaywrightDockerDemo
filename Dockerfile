FROM mcr.microsoft.com/playwright:v1.23.1-focal

FROM mcr.microsoft.com/dotnet/sdk:5.0.404-focal AS build

WORKDIR /src
COPY ["PlaywrightSharp/PlaywrightSharp.csproj", "PlaywrightSharp/"]
RUN dotnet restore "PlaywrightSharp/PlaywrightSharp.csproj"

COPY . .
WORKDIR "/src/PlaywrightSharp"

RUN apt-get update -yq && apt-get upgrade -yq && apt-get install -yq curl git nano
RUN curl -sL https://deb.nodesource.com/setup_16.x | bash - && apt-get install -yq nodejs build-essential
RUN npm install -g npm

RUN apt-get install -y wget xvfb unzip

# Set up the Chrome PPA
#RUN wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub | apt-key add -
#RUN echo "deb http://dl.google.com/linux/chrome/deb/ stable main" >> /etc/apt/sources.list.d/google.list

# Update the package list and install chrome
#RUN apt-get update -y
#RUN apt-get install -y google-chrome-stable
#RUN apt-get install -y firefox




#RUN dotnet add package Microsoft.Playwright

RUN dotnet build "PlaywrightSharp.csproj" -c Release -o /app/build
RUN npx playwright install-deps
RUN npx playwright install

# RUN dotnet test --no-build

FROM build AS testrunner


WORKDIR "/src/PlaywrightSharp"

#RUN npx playwright install-deps
#RUN npx playwright@1.20.0 install
#RUN npx playwright@1.20.0 install-deps
CMD ["dotnet", "test", "--no-restore", "--settings:Firefox.runsettings", "--logger:trx"]

#docker run -it -v /Users/bogdanveres/Documents/Logs:/src/PlaywrightSharp/TestResults vbsorin/playwrightnet
#docker run -it -v /Users/sorin.veres/Documents/Logs:/src/PlaywrightSharp/TestResults vbsorin/playwrightdemonet



