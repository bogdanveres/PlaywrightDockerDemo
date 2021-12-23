node {
    def mvnHome
    stage('Preparation') { // for display purposes
        deleteDir()
        checkout([$class: 'GitSCM', branches: [[name: '*/main']], extensions: [], userRemoteConfigs: [[credentialsId: '0af14516-0587-4a7c-a028-dbefa02c66c5', url: 'https://github.com/bogdanveres/PlaywrightDemo.git']]])
    }
    stage('Build') {
        // Run the dotnet test
        dotnetTest configuration: 'Debug', option: '-l:trx', project: 'PlaywrightSharp.csproj', sdk: 'NET5', workDirectory: 'PlaywrightSharp'
        
    }
    stage('Results') {
        mstest failOnError: false, keepLongStdio: true
        archiveArtifacts artifacts: '**/*.trx', followSymlinks: false
    }
    stage('Send Mail') {
        emailext body: 'Am rulat cu succes.', subject: 'Build completed', to: 'vbsorin@aaa.com'
    }
}
