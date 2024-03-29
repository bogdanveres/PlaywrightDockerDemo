pipeline {
    environment {
        imagename = "vbsorin/playwrightdemonet"
        registryCredential = 'git'
        dockerImage = ''
    }
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
        
        stage('Test Docker image') {
            agent {
                label 'built-in'
            }
            steps {
                script {
                    
                    try {                           
                        def status = 0
                        status = sh(returnStdout: true, script: "container-structure-test test --image vbsorin/playwrightdemonet --config ./unit-test.yaml --output json  | jq .Fail") as Integer
                        if (status != 0) {                            
                            error 'Image Test has failed'
                        }
        
                    } catch (err) {
                        error "Test-Image ERROR: The execution of the container structure tests failed, see the log for details."
                        echo err
                    } 
                }
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

        stage('Building image') {
            steps {
                script {
                    dockerImage = docker.build imagename
                }
            }
        }

        stage('Deploy Image') {
            steps{
                script {
                    docker.withRegistry( '', registryCredential ) {
                        dockerImage.push("$BUILD_NUMBER")
                        dockerImage.push('latest')
                    }
                }
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
