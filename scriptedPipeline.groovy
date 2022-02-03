node ("built-in"){
  stage('Build') {
    docker.image('vbsorin/playwrightdemonet').inside {
      sh 'dotnet --version'
    }
  }
}
