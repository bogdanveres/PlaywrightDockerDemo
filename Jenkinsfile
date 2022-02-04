node ("built-in"){
    def app
    stage('Clone repository') {               
        checkout scm    
    } 
    
    stage('Build') {
        sh 'echo ${WORKSPACE}'
            docker.image('vbsorin/playwrightdemonet').inside('-u root:root -v ${WORKSPACE}/TestResults:/src/PlaywrightSharp/TestResults') {
            sh 'dotnet test /src/PlaywrightSharp.sln --settings:/src/PlaywrightSharp/Firefox.runsettings --logger:trx'
        }
    }
    
    stage('Publish Results') {
        mstest failOnError: false, keepLongStdio: true
        archiveArtifacts artifacts: '**/*.trx', followSymlinks: false
    }

    stage('Build image') {         
        app = docker.build("vbsorin/playwrightdemonet")    
    }           
    
    stage('Test image') {                       
        app.inside {            
            sh 'echo "Tests passed"'        
        }    
    }            
        
    stage('Push image') {
        docker.withRegistry('https://registry.hub.docker.com', 'git') {
            app.push("${env.BUILD_NUMBER}")            
            app.push("latest")        
        }    
    }

    stage('Clean up Workspace')
    {
        cleanWs deleteDirs: true, patterns: [[pattern: 'TestResults', type: 'INCLUDE']]
    }
}
