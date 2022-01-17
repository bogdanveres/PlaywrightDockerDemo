pipeline {
    agent {
        docker { image 'vbsorin/playwrightsharp' }
    }
    stages {
        stage('Test') {
            steps {
                sh 'node --version'
            }
        }
    }
}
