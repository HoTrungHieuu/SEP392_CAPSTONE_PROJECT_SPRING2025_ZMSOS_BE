pipeline {
    agent any
    environment {
        REPO_URL = 'https://github.com/thduc123/SEP392_CAPSTONE_PROJECT_SPRING2025_ZMSOS_BE.git'
        IMAGE_NAME = 'trhoangduc/zmsos_be'
        DEPLOY_SERVER = '157.66.218.189'  // Change to your VPS IP
        DEPLOY_USER = 'zmsos'  // Change to your SSH user
    }

    stages {
        stage('Checkout Code') {
            steps {
                git branch: 'main', credentialsId: "9e79187a-99df-4ecd-b689-3a0212797ff8", url: env.REPO_URL
            }
        }

        stage('Determine Running Docker Image') {
            steps {
                script {
                    def runningImage = sh(script: "ssh -o StrictHostKeyChecking=no zmsos@157.66.218.189 docker ps --format "{{'{{'}}.Image{{'}}'}}" | grep ${IMAGE_NAME} || echo ""
).trim()
                    env.CACHE_IMAGE = runningImage ?: ''
                    echo "Using cache from: ${env.CACHE_IMAGE}"
                }
            }
        }

        stage('Generate Docker Tag') {
            steps {
                script {
                    def dateTag = new Date().format('MMddyyyy')
                    def buildCount = sh(script: "docker images | grep ${env.IMAGE_NAME} | grep ${dateTag} | wc -l", returnStdout: true).trim()
                    def buildNumber = buildCount ? buildCount.toInteger() + 1 : 1
                    env.DOCKER_TAG = "${dateTag}${String.format('%02d', buildNumber)}"
                    env.FULL_IMAGE_TAG = "${env.IMAGE_NAME}:${env.DOCKER_TAG}"
                    echo "Generated Docker Tag: ${env.FULL_IMAGE_TAG}"
                }
            }
        }

        stage('Build Docker Image') {
            steps {
                script {
                    def cacheArg = env.CACHE_IMAGE ? "--cache-from=${env.CACHE_IMAGE}" : ""
                    sh "docker build ${cacheArg} -t ${env.FULL_IMAGE_TAG} ."
                }
            }
        }

        stage('Push Docker Image to Docker Hub') {
            steps {
                script {
                    docker.withRegistry('https://registry.hub.docker.com', 'DockerHub') {
                      dockerImage.push()
                    }
                }
            }
        }

        stage('Deploy to VPS') {
            steps {
                sshagent(['vps-ssh-credentials']) {
                    sh """
                        ssh ${env.DEPLOY_USER}@${env.DEPLOY_SERVER} << EOF
                        docker pull ${env.FULL_IMAGE_TAG}
                        docker stop zmsos_be || true
                        docker rm zmsos_be || true
                        docker run -d -p 8080:80 --name zmsos_be -e ASPNETCORE_ENVIRONMENT=Development ${env.FULL_IMAGE_TAG}
                        docker image prune -f
                        docker images --format '{{.Repository}}:{{.Tag}}' | grep '${env.IMAGE_NAME}' | sort -r | tail -n +3 | xargs -r docker rmi
                        EOF
                    """
                }
            }
        }

        stage('Send Email Notification') {
            steps {
                emailext subject: "Build Successful: ${env.FULL_IMAGE_TAG}",
                          body: "The back-end service has been successfully built and deployed.\n\nDocker Image: ${env.FULL_IMAGE_TAG}",
                          to: 'ducthse172258@fpt.edu.vn'
            }
        }
    }

    post {
        failure {
            emailext subject: "Build Failed: ${env.IMAGE_NAME}",
                      body: "The build failed. Check the Jenkins logs for more details.",
                      to: 'ducthse172258@fpt.edu.vn'
        }
    }
}
