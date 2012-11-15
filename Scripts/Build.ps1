Import-Module .\BuildFunctions.psm1

properties {
	$buildDir 	  	  = "$baseDir\Build"
	$releaseDir    	  = "$baseDir\Release"
	$slnFile 	   	  = "$baseDir\Source\RemoteControlledRobot.sln"
}

task default -depends Compile

task Clean {
	Remove-Item $buildDir -Recurse -ErrorAction SilentlyContinue
	Remove-Item $releaseDir -Recurse -ErrorAction SilentlyContinue
}

task Init -depends Clean {
	New-Item -Type Directory $buildDir > $null
	New-Item -Type Directory $releaseDir > $null
}

task Compile -depends Init {
	exec { MSBuild $slnFile /property:Configuration=Release /verbosity:quiet /nologo }
}