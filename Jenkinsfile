pipeline {
  agent any
  stages {
    stage('Build') {
      parallel {
        stage('Build') {
          steps {
            echo "Build solution ${CHROMEPATH}"
            dotnetBuild(sdk: 'NET5', configuration: 'Debug', workDirectory: 'PlaywrightSharp', project: 'PlaywrightSharp.csproj', force: true)
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