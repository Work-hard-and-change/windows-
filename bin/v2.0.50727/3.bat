@echo off  
setlocal enabledelayedexpansion  
  
    rem ��ȡ��ǰ�ű����ڵ�Ŀ¼  
set "script_dir=%~dp0"  
  
REM ���������ļ�·��Ϊ��ǰĿ¼    
set "POLICY_FILE=%script_dir%\policy.cfg"    
set "TEMP_FILE=%temp%\policy_temp.cfg"      
  
REM ������ǰ��ȫ���Ե������ļ�  
secedit /export /cfg "%POLICY_FILE%"  
  
REM ������ʱ�ļ�����¼ԭʼ�����е�LockoutDuration����  
echo. >%script_dir%\out.txt  
type "%POLICY_FILE%" | findstr -i "LockoutDuration" >>%script_dir%\out.txt  
  
REM ��ȡ�����ļ����滻LockoutDuration����  
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



  
REM ����Ƿ�������滻����  
if !REPLACE! equ 1 (  
    REM �滻ԭʼ�����ļ�Ϊ��ʱ�ļ�  
    move /y "%TEMP_FILE%" "%POLICY_FILE%" >nul  
      
    REM ��¼�޸ĺ��LockoutDuration����  
    echo. >>%script_dir%\out.txt  
    type "%POLICY_FILE%" | findstr -i "LockoutDuration" >>%script_dir%\out.txt  
      
    REM ����Ӧ���޸ĺ�İ�ȫ����  
    secedit /configure /db "%windir%\security\local.sdb" /cfg "%POLICY_FILE%" /areas SECURITYPOLICY  
      
    echo Password complexity has been disabled.  
) else (  
    echo Password complexity setting was not found in the configuration file.  
)  
  
net accounts /lockoutthreshold:5
  

  
endlocal