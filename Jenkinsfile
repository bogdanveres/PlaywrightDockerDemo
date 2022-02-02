pipeline {
    agent {
        docker {
            image 'vbsorin/playwrightdemonet'
            args '-u root:root -v /Users/sorin.veres/Documents/Logs:/src/PlaywrightSharp/TestResults'
        }   
    }
    stages {
        stage('Build') {
            steps {
                sh 'dotnet test /src/PlaywrightSharp.sln --settings:/src/PlaywrightSharp/Firefox.runsettings --logger:trx'
                
            }
        }
    }
}
