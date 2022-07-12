pipeline {
  agent {
    node {
      label 'master'
      customWorkspace "E:\\Jenkins\\TargetVectorWorkspace"//use backward slashes to avoid problems with how Windows uses directories!!
    }
  }//^all this is necessary to run the build in a special workspace.
  environment {
    ue5Path = "C:\\dev\\UE_5.0"
    ue5Project = "TargetVector"
    ue5ProjectDisplayName = "Target Vector"
    ueProjectFileName = "${ue5Project}.uproject"
    testSuiteToRun = "TargetVector."//the '.' is used to run all tests inside the prettyname. The automation system searches for everything that has 'Game.' in it, so otherGame.'s tests would run too...
    testReportFolder = "TestsReport"
    testsLogName = "RunTests.log"
    pathToTestsLog = "${env.WORKSPACE}" + "\\TestLogs\\" + "${testsLogName}"
    codeCoverageReportName="CodeCoverageReport.xml"
    tvDiscordWebhook = "${TVDISCORDWEBHOOK}"
  }
  stages {
    stage('Building') {
      steps {
        discordSend description: "${ue5ProjectDisplayName} build ${env.BUILD_DISPLAY_NAME} Started", footer: "Commit ${GIT_COMMIT} started build ${env.BUILD_DISPLAY_NAME} on ${env.BRANCH_NAME} at node ${env.NODE_NAME}", link: env.BUILD_URL, result: "ABORTED", title: ":white_circle:  ${ue5ProjectDisplayName} (${env.BUILD_DISPLAY_NAME}) Build Started", webhookURL: "${tvDiscordWebhook}"

        bat "BuildWithoutCooking.bat \"${ue5Path}\" \"${env.WORKSPACE}\" \"${ueProjectFilename}\""//builds our project
      }
      post {
        success {
          echo 'Build Stage Successful.'
        }
        failure {
          echo 'Build Stage Unsuccessful.'
        }
      }
    }

    stage('Testing') {
      steps {
        echo 'Testing Stage Started.'
        script {
          if(env.BRANCH_NAME == 'main') {
            echo 'Push to master recognized. Starting tests and code coverage.'
            bat "TestRunnerAndCodeCoverage.bat \"${ue5Path}\" \"${env.WORKSPACE}\" \"${ueProjectFilename}\" \"${testSuiteToRun}\" \"${testReportFolder}\" \"${testsLogName}\" \"${codeCoverageReportName}\""//runs the tests and performs code coverage
          }
          else {
            bat "TestRunner.bat \"${ue5Path}\" \"${env.WORKSPACE}\" \"${ueProjectFilename}\" \"${testSuiteToRun}\" \"${testReportFolder}\" \"${testsLogName}\""//runs the tests
          }
        }
      }
      post {
        success {
          echo 'Testing Stage Successful.'
        }
        failure {
          echo 'Testing Stage Unsuccessful.'
        }
      }
    }



  }
  post {
    always{
      echo 'Tests finished, printing log.'
      bat "type ${pathToTestsLog}"
      echo 'Formatting TestsReport from JSon to JUnit XML'
      // formatUnitTests()

      	discordSend description: "Total Tests: ${testReportSummary.totalCount}, Failures: ${testReportSummary.failCount}, Skipped: ${testReportSummary.skipCount}, Passed: ${testReportSummary.passCount}", footer: "${env.BUILD_DISPLAY_NAME} on ${env.BRANCH_NAME}", result: currentBuild.currentResult, link: env.BUILD_URL, title: "${env.BUILD_DISPLAY_NAME} on ${env.BRANCH_NAME}_\n *Tests Report Summary*", webhookURL: "${tvDiscordWebhook}"
      
      script {
      if (env.BRANCH_NAME == 'main') {
          echo "Publish Code Coverage Report."
          cobertura(coberturaReportFile:"${codeCoverageReportName}")
          }
      }

      echo 'Cleaning up workspace:'
      echo '-checking current workspace.'
      powershell label: 'show workspace', script: 'dir $WORKSPACE'
      bat 'git reset --hard'//resets to HEAD, to the commit in the cloned repository.
      bat 'git clean -dffx .'//removes untracked files.
      echo '-checking clean workspace.'
      powershell label: 'show workspace', script: 'dir $WORKSPACE'

      // echo 'Sending build status notification to Discord:'
    }
    success{
    	discordSend description: "${ue5ProjectDisplayName} build ${env.BUILD_DISPLAY_NAME} Successful", footer: "Commit ${GIT_COMMIT} build ${env.BUILD_DISPLAY_NAME} succeeded on ${env.BRANCH_NAME} at node ${env.NODE_NAME}", link: env.BUILD_URL, result: currentBuild.currentResult, title: ":green-circle:  ${ue5ProjectDisplayName} (${env.BUILD_DISPLAY_NAME}) Build Successful", webhookURL: "${tvDiscordWebhook}"
    }
    unstable{
    	discordSend description: "${ue5ProjectDisplayName} build ${env.BUILD_DISPLAY_NAME} Unstable", footer: "Commit ${GIT_COMMIT} build ${env.BUILD_DISPLAY_NAME} unstable on ${env.BRANCH_NAME} at node ${env.NODE_NAME}", link: env.BUILD_URL, result: currentBuild.currentResult, title: ":yellow_circle:  ${ue5ProjectDisplayName} (${env.BUILD_DISPLAY_NAME}) Build Unstable", webhookURL: "${tvDiscordWebhook}"
    }
    failure{
    	discordSend description: "${ue5ProjectDisplayName} build ${env.BUILD_DISPLAY_NAME} Failed", footer: "Commit ${GIT_COMMIT} build ${env.BUILD_DISPLAY_NAME} failed on ${env.BRANCH_NAME} at node ${env.NODE_NAME}", link: env.BUILD_URL, result: currentBuild.currentResult, title: ":red-circle:  ${ue5ProjectDisplayName} (${env.BUILD_DISPLAY_NAME}) Build Failed", webhookURL: "${tvDiscordWebhook}"
    }
  }
}

import groovy.json.JsonSlurper
import groovy.xml.MarkupBuilder

@NonCPS
def jsonParse(text) {
        return new groovy.json.JsonSlurperClassic().parseText(text);
}

def testReportSummary = 'to be populated...'

def formatUnitTests() {
        try {
          convertTestsReport()
          testReportSummary = junit "${testReportFolder}\\junit.xml"
        }
        catch (Exception e){
          echo "couldn't generate JUnit Test Report to ${testReportFolder}..."
        }
}

def convertTestsReport() {
    // def jsonReport = jsonParse(readFile( "${testReportFolder}\\index.json", "UTF-8" ))
    def jsonReport = readFile file: "${testReportFolder}\\index.json", encoding: "UTF-8"
    // Needed because the JSON is encoded in UTF-8 with BOM

    jsonReport = jsonReport.replace( "\uFEFF", "" );

    def xmlContent = transformReport( jsonReport )

    writeFile file: "${testReportFolder}\\junit.xml", text: xmlContent.toString()
}

@NonCPS//atomic method
def transformReport( String jsonContent ) {

    def parsedReport = new JsonSlurper().parseText( jsonContent )
    
    def jUnitReport = new StringWriter()
    def builder = new MarkupBuilder( jUnitReport )

    builder.doubleQuotes = true
    builder.mkp.xmlDeclaration version: "1.0", encoding: "utf-8"

    builder.testsuite( tests: parsedReport.succeeded + parsedReport.failed, failures: parsedReport.failed, time: parsedReport.totalDuration ) {
      for ( test in parsedReport.tests ) {
        builder.testcase( name: test.testDisplayName, classname: test.fullTestPath, status: test.state ) {
          if(test.state == "Fail") {
            for ( entry in test.entries ) { 
              if(entry.event.type == "Error") {
                builder.failure( message: entry.event.message, type: entry.event.type, entry.filename + " " + entry.lineNumber )
              }
            }
          }
        }
      }
    } 

    return jUnitReport.toString()
}