pipeline {
    agent {
        dockerfile true
        label 'sclavu'
    }
    
    stages {
        stage('Build') {
            steps {
                sh 'node --version'
                sh 'pwd'
            }
        }
    }
}