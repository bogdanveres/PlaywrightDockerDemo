pipeline {
  agent any
  stages {
    stage('Build') {
      parallel {
        stage('Build') {
          steps {
            echo "Build solution ${CHROMEPATH}"
            dotnetTest(configuration: 'Debug', option: '-l:trx', project: 'PlaywrightSharp.csproj', sdk: '5.0.13', workDirectory: 'PlaywrightSharp')
          }
        }

        stage('Test') {
          steps {
            echo 'Test app'
          }
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