pipeline {
  agent {
    node {
      label 'built-in'
    }

  }
  stages {
    stage('Clean') {
      steps {
        dotnetClean(workDirectory: 'PlaywrightSharp', project: 'PlaywrightSharp.csproj', configuration: 'Debug', sdk: 'Net5Master', runtime: '5.0.13')
      }
    }

    stage('Build') {
      parallel {
        stage('Chrome') {
          steps {
            echo "Build solution ${CHROMEPATH}"
            dotnetTest(configuration: 'Debug', option: '-l:trx', project: 'PlaywrightSharp.csproj', sdk: 'Net5Master', workDirectory: 'PlaywrightSharp', runtime: '5.0.13', specificSdkVersion: true, settings: 'Chrome.runsettings')
          }
        }

        stage('Firefox') {
          steps {
            echo 'Run on chrome'
            dotnetTest(configuration: 'Debug', option: '-l:trx', project: 'PlaywrightSharp.csproj', sdk: 'Net5Master', workDirectory: 'PlaywrightSharp', runtime: '5.0.13', specificSdkVersion: true, settings: 'Firefox.runsettings')
          }
        }

      }
    }

    stage('Deploy') {
      steps {
        echo 'Deploy app.'
        mstest(testResultsFile: '**/*.trx')
      }
    }

  }
  environment {
    CHROMEPATH = '/cacat'
  }
}