pipeline {
    agent any
    environment {
        REPO_URL = 'https://github.com/thduc123/SEP392_CAPSTONE_PROJECT_SPRING2025_ZMSOS_BE.git'
        IMAGE_NAME = 'trhoangduc/zmsos_be'
        DEPLOY_SERVER = '157.66.218.189'  // Change to your VPS IP
        DEPLOY_USER = 'zmsos'  // Change to your SSH user
        DEPLOY_SCRIPT = 'deploy_be.sh'
        SSH_CREDS = credentials("VPS_SSH_Credentials")
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
                    echo "Testing SSH Connection..."
                    sh "ssh -v -o StrictHostKeyChecking=no ${env.DEPLOY_USER}@${env.DEPLOY_SERVER} 'echo SSH connection successful'"

                    def runningImage = sh(script: """
                    ssh -o StrictHostKeyChecking=no ${env.DEPLOY_USER}@${env.DEPLOY_SERVER} docker ps --format "{{.Image}}" | grep ${env.IMAGE_NAME} || echo ""
                    """, returnStdout: true).trim()
            
                    env.CACHE_IMAGE = runningImage ?: ''
                    echo "Using cache from: ${env.CACHE_IMAGE}"
                }
            }
        }

        stage('Generate Docker Tag') {
            steps {
                script {
                    def dateTag = new Date().format('ddMMyy')
                    def buildCount = sh(script: """
                    ssh -o StrictHostKeyChecking=no ${env.DEPLOY_USER}@${env.DEPLOY_SERVER} docker images --format "{{.Tag}}" ${env.IMAGE_NAME} | grep ${dateTag} | awk '{print substr(\$1, 7)}' | sort -rn | head -n 1
                    """, returnStdout: true).trim()
	                echo "Old build: ${buildCount}"
                    def buildNumber = buildCount.isInteger() ? buildCount.toInteger() + 1 : 1
                    env.DOCKER_TAG = "${dateTag}${String.format('%02d', buildNumber)}"
                    env.FULL_IMAGE_TAG = "${env.IMAGE_NAME}:${env.DOCKER_TAG}"
                    echo "Generated Docker Tag: ${env.FULL_IMAGE_TAG}"
                }
            }
        }
        
        stage('Deploy to VPS') {
            steps {
                sshagent(['VPS_SSH_Credentials']) {
                    sh """
                        ssh ${env.DEPLOY_USER}@${env.DEPLOY_SERVER} "bash ${env.DEPLOY_SCRIPT} ${env.CACHE_IMAGE} ${env.FULL_IMAGE_TAG}"
                    """
                }
            }
        }
    }
}
