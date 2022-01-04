pipeline {
  agent {
    node {
      label 'built-in'
    }

  }
  stages {
    stage('Build') {
      parallel {
        stage('Build') {
          steps {
            echo "Build solution ${CHROMEPATH}"
            dotnetTest(configuration: 'Debug', option: '-l:trx', project: 'PlaywrightSharp.csproj', sdk: 'Net5Master', workDirectory: 'PlaywrightSharp', runtime: '5.0.13', specificSdkVersion: true, settings: 'Chrome.runsettings')
          }
        }

        stage('Test') {
          steps {
            echo 'Test app'
            dotnetTest(configuration: 'Debug', option: '-l:trx', project: 'PlaywrightSharp.csproj', sdk: 'Net5Master', workDirectory: 'PlaywrightSharp', runtime: '5.0.13', specificSdkVersion: true, settings: 'Firefox.runsettings')
        }
      }
    }

    stage('Deploy') {
      steps {
        input(message: 'Do you wwant to deploy?', id: 'OK')
        echo 'Deploy app.'
      }
    }

  }
  environment {
    CHROMEPATH = '/cacat'
  }
}
