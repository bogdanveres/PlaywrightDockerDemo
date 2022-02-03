pipeline {
    agent any
    stages {
        stage('Clean WS') {
            agent {
                label 'built-in'
            }
            
            steps {
                cleanWs()
            }
        }
        
        stage('Build') {
            agent {
                docker {
                    image 'vbsorin/playwrightdemonet'
                    args '-u root:root -v ${WORKSPACE}/TestResults:/src/PlaywrightSharp/TestResults'
                }
            }
            steps {
                sh 'dotnet test /src/PlaywrightSharp.sln --settings:/src/PlaywrightSharp/Firefox.runsettings --logger:trx'
            }
        }
        
        stage('Publish') {
            agent {
                label 'built-in'
            }
            steps {
                mstest testResultsFile:'**/*.trx', keepLongStdio: true, failOnError: false
                archiveArtifacts artifacts: 'TestResults/*.trx', fingerprint: true
                always {
                   xunit([MSTest(deleteOutputFiles: true, failIfNotNew: true, pattern: 'TestResults/*.trx', skipNoTestFiles: false, stopProcessingIfError: true)])
                }
            }
        }
    }
    
    post {
        // Clean after build
        always {
            cleanWs(cleanWhenNotBuilt: false,
                    deleteDirs: true,
                    disableDeferredWipeout: true,
                    notFailBuild: true,
                    patterns: [[pattern: '.gitignore', type: 'INCLUDE'],
                               [pattern: '.propsfile', type: 'EXCLUDE']])
        }
    }
}
