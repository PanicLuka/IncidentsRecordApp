pipeline {  
  agent { label 'master' }
  stages {
  
	stage('Prep') {
		  agent any		  
		  steps {
			git branch: env.BRANCH_NAME, credentialsId: 'gitlab-credentials-danijel.popovic', url: 'http://tiaclab.com:9009/danijel.popovic/praksa2022_01.git'
			// dir("WebApi") {	
			// 	bat "docker login"			
			// 	bat "docker build . -t danijelpopovic/webappprivate:${BRANCH_NAME}-${env.BUILD_NUMBER}"
			// 	bat "docker push danijelpopovic/webappprivate:${BRANCH_NAME}-${env.BUILD_NUMBER}"									
			// }	

			// dir("WebFrontend") {
			// 	bat "docker build . -t danijelpopovic/webapp:${BRANCH_NAME}-${env.BUILD_NUMBER}"
			// 	bat "docker push danijelpopovic/webapp:${BRANCH_NAME}-${env.BUILD_NUMBER}"
			// }
		}
	}
	stage('Build and Test') {
	  parallel {
		stage("Gateway Service - backend") {
		  stages {
			stage("Build") {
			  agent any
			  steps {
				 bat "dotnet restore ${workspace}\\backend\\GatewayService\\GatewayService.sln"
				 bat "dotnet clean ${workspace}\\backend\\GatewayService\\GatewayService.sln"
				 bat "dotnet build ${workspace}\\backend\\GatewayService\\GatewayService.sln"
				 //echo "Build"
			  }
			}
			stage("SonarQube analysis") {
			  agent any
			  steps {
				withSonarQubeEnv('SonarQube') {
					//bat "dotnet sonarscanner begin /key:\"${BRANCH_NAME}-GatewayService\" /d:sonar.cs.xunit.reportsPaths=**/*.coveragexml /d:sonar.exclusions=\"WebApi/Entities/**, WebApi/Helpers/**, WebApi/Migrations/**, WebApi/Models/**\""
					//bat "dotnet build backend/GatewayService/GatewayService.sln"
					//bat "dotnet test WebApiXUTest/WebApiXUTest.csproj --collect:\"Code Coverage\" --results-directory:\"TestResults\""
					//bat "dotnet-coverageconverter --CoverageFilesFolder \"TestResults\""					
					//bat "dotnet sonarscanner end"					
				}
			  }
			}
			stage("Quality gate") {
			  agent any
			  steps {
				//waitForQualityGate abortPipeline: true
				echo "Sonar Analyst"
			  }
			}
			stage("Unit tests") {
			  agent any
			  steps {
				//bat "dotnet test ${workspace}/WebApiXUTest/WebApiXUTest.csproj"
				echo "Unit test"
			  }
			}
			stage("Publish") {
			  agent any
			  steps {
				bat "dotnet publish ${workspace}\\backend\\GatewayService\\GatewayService.sln -c Release -o publish"
				//echo "Publish"
			  }
			}			
		  }
		  post {
			success {
                slackSend(channel: 'jenkins', color: 'good', message:"Gateway Service - Build deployed successfully - ${env.JOB_NAME} ${env.BUILD_NUMBER} (<http://192.168.1.90:8080/blue/organizations/jenkins/praksa/detail/${env.BRANCH_NAME}/${env.BUILD_NUMBER}/pipeline|Open>)")
            }
			failure {
				slackSend(channel: 'jenkins', color: 'red', message:"Gateway Service - Build failed  - ${env.JOB_NAME} ${env.BUILD_NUMBER} (<http://192.168.1.90:8080/blue/organizations/jenkins/praksa/detail/${env.BRANCH_NAME}/${env.BUILD_NUMBER}/pipeline|Open>)")
			}
			always {
				echo "DONE"
			}
		  }
		}
		
		stage("Incident Service - backend") {
		  stages {
			stage("Build") {
			  agent any
			  steps {
				bat "dotnet restore ${workspace}\\backend\\IncidentService\\IncidentService.sln"
				bat "dotnet clean ${workspace}\\backend\\IncidentService\\IncidentService.sln"
				bat "dotnet build ${workspace}\\backend\\IncidentService\\IncidentService.sln"
			  }
			}
			stage("SonarQube analysis") {
			  agent any
			  steps {
				withSonarQubeEnv('SonarQube') {
					//bat "dotnet sonarscanner begin /key:\"${BRANCH_NAME}-GatewayService\" /d:sonar.cs.xunit.reportsPaths=**/*.coveragexml /d:sonar.exclusions=\"WebApi/Entities/**, WebApi/Helpers/**, WebApi/Migrations/**, WebApi/Models/**\""
					//bat "dotnet build backend/GatewayService/GatewayService.sln"
					//bat "dotnet test WebApiXUTest/WebApiXUTest.csproj --collect:\"Code Coverage\" --results-directory:\"TestResults\""
					//bat "dotnet-coverageconverter --CoverageFilesFolder \"TestResults\""					
					//bat "dotnet sonarscanner end"					
				}
			  }
			}
			stage("Quality gate") {
			  agent any
			  steps {
				//waitForQualityGate abortPipeline: true
				echo "Sonar Analyst"
			  }
			}
			stage("Unit tests") {
			  agent any
			  steps {
				//bat "dotnet test ${workspace}/WebApiXUTest/WebApiXUTest.csproj"
				echo "Unit test"
			  }
			}
			stage("Publish") {
			  agent any
			  steps {
				bat "dotnet publish ${workspace}\\backend\\IncidentService\\IncidentService.sln -c Release -o publish"
				echo "Publish"
			  }
			}			
		  }
		  post {
			success {
                slackSend(channel: 'jenkins', color: 'good', message:"Incident Service - Build deployed successfully - ${env.JOB_NAME} ${env.BUILD_NUMBER} (<http://192.168.1.90:8080/blue/organizations/jenkins/praksa/detail/${env.BRANCH_NAME}/${env.BUILD_NUMBER}/pipeline|Open>)")
            }
			failure {
				slackSend(channel: 'jenkins', color: 'red', message:"Incident Service - Build failed  - ${env.JOB_NAME} ${env.BUILD_NUMBER} (<http://192.168.1.90:8080/blue/organizations/jenkins/praksa/detail/${env.BRANCH_NAME}/${env.BUILD_NUMBER}/pipeline|Open>)")
			}
			always {
				echo "DONE"
			}
		  }
		}
		
		stage("Report Service - backend") {
		  stages {
			stage("Build") {
			  agent any
			  steps {
				bat "dotnet restore ${workspace}\\backend\\ReportService\\ReportService.sln"
				bat "dotnet clean ${workspace}\\backend\\ReportService\\ReportService.sln"
				bat "dotnet build ${workspace}\\backend\\ReportService\\ReportService.sln"
			  }
			}
			stage("SonarQube analysis") {
			  agent any
			  steps {
				withSonarQubeEnv('SonarQube') {
					//bat "dotnet sonarscanner begin /key:\"${BRANCH_NAME}-GatewayService\" /d:sonar.cs.xunit.reportsPaths=**/*.coveragexml /d:sonar.exclusions=\"WebApi/Entities/**, WebApi/Helpers/**, WebApi/Migrations/**, WebApi/Models/**\""
					//bat "dotnet build backend/GatewayService/GatewayService.sln"
					//bat "dotnet test WebApiXUTest/WebApiXUTest.csproj --collect:\"Code Coverage\" --results-directory:\"TestResults\""
					//bat "dotnet-coverageconverter --CoverageFilesFolder \"TestResults\""					
					//bat "dotnet sonarscanner end"					
				}
			  }
			}
			stage("Quality gate") {
			  agent any
			  steps {
				//waitForQualityGate abortPipeline: true
				echo "Sonar Analyst"
			  }
			}
			stage("Unit tests") {
			  agent any
			  steps {
				//bat "dotnet test ${workspace}/WebApiXUTest/WebApiXUTest.csproj"
				echo "Unit test"
			  }
			}
			stage("Publish") {
			  agent any
			  steps {
				bat "dotnet publish ${workspace}\\backend\\ReportService\\ReportService.sln -c Release -o publish"
			  }
			}			
		  }
		  post {
			success {
                slackSend(channel: 'jenkins', color: 'good', message:"Report Service - Build deployed successfully - ${env.JOB_NAME} ${env.BUILD_NUMBER} (<http://192.168.1.90:8080/blue/organizations/jenkins/praksa/detail/${env.BRANCH_NAME}/${env.BUILD_NUMBER}/pipeline|Open>)")
            }
			failure {
				slackSend(channel: 'jenkins', color: 'red', message:"Report Service - Build failed  - ${env.JOB_NAME} ${env.BUILD_NUMBER} (<http://192.168.1.90:8080/blue/organizations/jenkins/praksa/detail/${env.BRANCH_NAME}/${env.BUILD_NUMBER}/pipeline|Open>)")
			}
			always {
				echo "DONE"
			}
		  }
		}
		
		stage("User Service - backend") {
		  stages {
			stage("Build") {
			  agent any
			  steps {
				bat "dotnet restore ${workspace}\\backend\\UserService\\UserService.sln"
				bat "dotnet clean ${workspace}\\backend\\UserService\\UserService.sln"
				bat "dotnet build ${workspace}\\backend\\UserService\\UserService.sln"
			  }
			}
			stage("SonarQube analysis") {
			  agent any
			  steps {
				withSonarQubeEnv('SonarQube') {
					//bat "dotnet sonarscanner begin /key:\"${BRANCH_NAME}-GatewayService\" /d:sonar.cs.xunit.reportsPaths=**/*.coveragexml /d:sonar.exclusions=\"WebApi/Entities/**, WebApi/Helpers/**, WebApi/Migrations/**, WebApi/Models/**\""
					//bat "dotnet build backend/GatewayService/GatewayService.sln"
					//bat "dotnet test WebApiXUTest/WebApiXUTest.csproj --collect:\"Code Coverage\" --results-directory:\"TestResults\""
					//bat "dotnet-coverageconverter --CoverageFilesFolder \"TestResults\""					
					//bat "dotnet sonarscanner end"					
				}
			  }
			}
			stage("Quality gate") {
			  agent any
			  steps {
				//waitForQualityGate abortPipeline: true
				echo "Sonar Analyst"
			  }
			}
			stage("Unit tests") {
			  agent any
			  steps {
				//bat "dotnet test ${workspace}/WebApiXUTest/WebApiXUTest.csproj"
				echo "Unit test"
			  }
			}
			stage("Publish") {
			  agent any
			  steps {
				bat "dotnet publish ${workspace}\\backend\\UserService\\UserService.sln -c Release -o publish"
			  }
			}			
		  }
		  post {
			success {
                slackSend(channel: 'jenkins', color: 'good', message:"User Service - Build deployed successfully - ${env.JOB_NAME} ${env.BUILD_NUMBER} (<http://192.168.1.90:8080/blue/organizations/jenkins/praksa/detail/${env.BRANCH_NAME}/${env.BUILD_NUMBER}/pipeline|Open>)")
            }
			failure {
				slackSend(channel: 'jenkins', color: 'red', message:"User Service - Build failed  - ${env.JOB_NAME} ${env.BUILD_NUMBER} (<http://192.168.1.90:8080/blue/organizations/jenkins/praksa/detail/${env.BRANCH_NAME}/${env.BUILD_NUMBER}/pipeline|Open>)")
			}
			always {
				echo "DONE"
			}
		  }
		}
		
		stage("Frotend App") {
		  stages {
			stage("Install Node dependencies") {
			  agent any
			  steps {               
				dir("WebFrontend") {
					sh 'npm install' 
				}				
			  }
			}
			stage("Lint") {
			  agent any
			  steps {
				dir("WebFrontend") {
					//sh 'npm run lint' 
				}
			  }
			}
			stage("Build") {
			  agent any
			  steps {
				dir("WebFrontend") {
					//sh 'npm run buildProd' 
				}
			  }
			}						
		  }
		  post {
		  success {
                slackSend(channel: 'jenkins', color: 'good', message:"Frontend App - Build deployed successfully - ${env.JOB_NAME} ${env.BUILD_NUMBER} (<http://192.168.1.90:8080/blue/organizations/jenkins/praksa/detail/${env.BRANCH_NAME}/${env.BUILD_NUMBER}/pipeline|Open>)")
            }
			failure {
				slackSend(channel: 'jenkins', color: 'red', message:"Frontend App - Build failed  - ${env.JOB_NAME} ${env.BUILD_NUMBER} (<http://192.168.1.90:8080/blue/organizations/jenkins/praksa/detail/${env.BRANCH_NAME}/${env.BUILD_NUMBER}/pipeline|Open>)")
			}
			always {
				echo "DONE"
			}
		  }
		}
	  }
	}
	// stage('Dockerizing and Automation tests') {
	// 	agent any
	// 	steps {
	// 	    bat 'setdocker.bat ${BRANCH_NAME}'	
	// 		sh 'docker-compose -f test-docker-compose.yml build'
	// 		sh 'docker-compose -f test-docker-compose.yml up -d'	
	// 		bat """
	// 			cd WebFrontend
	// 			cd tests
	// 			runTest.bat
	// 			"""	
	// 	}
	//     post {
	// 		always {  
	// 			dir("WebApi") {	
	// 				bat "docker login"			
	// 				bat "docker build . -t danijelpopovic/webappprivate:${BRANCH_NAME}-${env.BUILD_NUMBER}"
	// 				bat "docker push danijelpopovic/webappprivate:${BRANCH_NAME}-${env.BUILD_NUMBER}"									
	// 			}	
	// 			dir("WebFrontend") {
	// 				bat "docker build . -t danijelpopovic/webapp:${BRANCH_NAME}-${env.BUILD_NUMBER}"
	// 				bat "docker push danijelpopovic/webapp:${BRANCH_NAME}-${env.BUILD_NUMBER}"
	// 			}				
	// 			sh 'docker-compose -f test-docker-compose.yml down'
	// 			bat """
	// 				cd WebFrontend
	// 				cd tests
	// 				cd target
	// 				cd surefire-reports				
	// 				copy /b TEST-CucumberRunnerTest.xml +,,
	// 			"""
	// 			junit 'WebFrontend/tests/target/surefire-reports/TEST-CucumberRunnerTest.xml'
	// 		}
	//     }
	// }	
	stage('Deploy to IIS') {
		when {
			branch 'dev'
		}
		parallel {
			stage("Gateway Service") {
				agent any
				steps {
					echo "Deploy Gateway Service"
					bat "msdeploy.exe -verb:sync -source:IisApp='${workspace}/publish' -dest:iisapp='GatewayService',computerName='https://192.168.1.90:8172/msdeploy.axd?site=GatewayService',authType='basic',username='msdeploy',password='msdeploy' -enableRule:AppOffline -allowUntrusted"
				}
			}
			stage("Incident Service") {
				agent any
				steps {
					echo "Deploy Gateway Service"
					bat "msdeploy.exe -verb:sync -source:IisApp='${workspace}/publish' -dest:iisapp='IncidentService',computerName='https://192.168.1.90:8172/msdeploy.axd?site=IncidentService',authType='basic',username='msdeploy',password='msdeploy' -enableRule:AppOffline -allowUntrusted"
				}
			}
			stage("Report Service") {
				agent any
				steps {
					echo "Deploy Gateway Service"
					bat "msdeploy.exe -verb:sync -source:IisApp='${workspace}/publish' -dest:iisapp='ReportService',computerName='https://192.168.1.90:8172/msdeploy.axd?site=ReportService',authType='basic',username='msdeploy',password='msdeploy' -enableRule:AppOffline -allowUntrusted"
				}
			}
			stage("User Service") {
				agent any
				steps {
					echo "Deploy Gateway Service"
					bat "msdeploy.exe -verb:sync -source:IisApp='${workspace}/publish' -dest:iisapp='UserService',computerName='https://192.168.1.90:8172/msdeploy.axd?site=UserService',authType='basic',username='msdeploy',password='msdeploy' -enableRule:AppOffline -allowUntrusted"
				}
			}
			stage("Deploy WebFrontend") {
				agent any
				steps {
					echo "Deploy frontend"
					//bat "msdeploy.exe -verb:sync -source:IisApp='${workspace}/WebFrontend/dist/angular-registration-login-example' -dest:iisapp='WebFrontend',computerName='https://192.168.1.90:8172/msdeploy.axd?site=WebFrontend',authType='basic',username='msdeploy',password='msdeploy' -enableRule:AppOffline -allowUntrusted"
				}
			}
		}
	}
  }   
}
