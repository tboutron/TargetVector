set ue5path="D:\Dev\UE5\UE_5.0\Engine\Binaries\Win64\"
echo %ue5path%
start "" %ue5path%"UnrealEditor.exe" %~dp0"EOSLearning.uproject" -game -ResX=800 -ResY=900 -WinX=0 -WinY=20 -log