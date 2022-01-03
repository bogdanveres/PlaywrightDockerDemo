pipeline {
  agent any
  stages {
    stage('Build') {
      parallel {
        stage('Build') {
          steps {
            echo 'Build solution ${ChromePath}'
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
        echo 'Deploy app.'
      }
    }

  }
  environment {
    ChromePath = '/test/location'
  }
}