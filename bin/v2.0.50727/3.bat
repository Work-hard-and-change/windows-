@echo off  
setlocal enabledelayedexpansion  
  
    rem 获取当前脚本所在的目录  
set "script_dir=%~dp0"  
  
REM 定义配置文件路径为当前目录    
set "POLICY_FILE=%script_dir%\policy.cfg"    
set "TEMP_FILE=%temp%\policy_temp.cfg"      
  
REM 导出当前安全策略到配置文件  
secedit /export /cfg "%POLICY_FILE%"  
  
REM 创建临时文件并记录原始配置中的LockoutDuration设置  
echo. >%script_dir%\out.txt  
type "%POLICY_FILE%" | findstr -i "LockoutDuration" >>%script_dir%\out.txt  
  
REM 读取配置文件并替换LockoutDuration设置  
set "REPLACE=0"  
(for /f "tokens=*" %%a in ('type "%POLICY_FILE%"') do (  
    set "LINE=%%a"  
    if "!LINE!"=="LockoutDuration = 10" (  
        set "REPLACE=0"
    ) else if "!LINE!"=="LockoutDuration = 30" (  
        set "LINE=LockoutDuration = 10"  
        set "REPLACE=1"  
    )  
    echo !LINE!  
)) > "%TEMP_FILE%"



  
REM 检查是否进行了替换操作  
if !REPLACE! equ 1 (  
    REM 替换原始配置文件为临时文件  
    move /y "%TEMP_FILE%" "%POLICY_FILE%" >nul  
      
    REM 记录修改后的LockoutDuration设置  
    echo. >>%script_dir%\out.txt  
    type "%POLICY_FILE%" | findstr -i "LockoutDuration" >>%script_dir%\out.txt  
      
    REM 重新应用修改后的安全策略  
    secedit /configure /db "%windir%\security\local.sdb" /cfg "%POLICY_FILE%" /areas SECURITYPOLICY  
      
    echo Password complexity has been disabled.  
) else (  
    echo Password complexity setting was not found in the configuration file.  
)  
  
net accounts /lockoutthreshold:5
  

  
endlocal