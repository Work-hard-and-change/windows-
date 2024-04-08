@echo off
setlocal enabledelayedexpansion

rem ��ȡ��ǰ�ű����ڵ�Ŀ¼
set "script_dir=%~dp0"

rem ���õ�ǰĿ¼Ϊ�ű�����Ŀ¼
cd /d "%script_dir%"

rem ��� secpol.cfg �ļ�
type nul > "%script_dir%secpol.cfg"

rem �������ذ�ȫ���Բ����浽��ǰĿ¼�µ� secpol.cfg �ļ���
secedit /export /cfg "%script_dir%secpol.cfg"

rem ���� net user guest ���������ض�����ʱ�ļ�
net user guest > "%script_dir%temp.txt"

rem ����ʱ�ļ��в����ʻ����õ��в���ֵ��ȡ��׷�ӵ� secpol.cfg �ļ���
for /f "tokens=2" %%i in ('findstr "�ʻ�����" "%script_dir%temp.txt"') do (
    echo %%i >> "%script_dir%secpol.cfg"
)

rem ���� net user administrator ���������ض�����ʱ�ļ�
net user administrator > "%script_dir%temp.txt"

rem ����ʱ�ļ��в����ʻ����õ��в���ֵ��ȡ��׷�ӵ� secpol.cfg �ļ���
for /f "tokens=2" %%i in ('findstr "�ʻ�����" "%script_dir%temp.txt"') do (
    echo %%i >> "%script_dir%secpol.cfg"
)

rem ��ѯ��׷����Ļ������ʱ���Ե� secpol.cfg �ļ���
reg query "HKEY_CURRENT_USER\Control Panel\Desktop" /v ScreenSaveTimeOut >> "%script_dir%secpol.cfg"

rem ��ѯ��׷�Ӳ���ʾ�ϴ��û������Ե� secpol.cfg �ļ���
reg query "HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Policies\System" /v DontDisplayLastUserName >> "%script_dir%secpol.cfg"


rem ִ��route print�������������浽��ʱ�ļ�temp.txt
route print > temp.txt

rem ��ʼ����־����������ָʾ�Ƿ���"����·��"����
set "isPermanent=false"

rem ��ʼ���м�����
set "lineCount=0"

rem ������ʱ�ļ��е�ÿһ��
for /f "tokens=*" %%A in (temp.txt) do (
    rem ����ǰ�����ݴ洢������line��
    set "line=%%A"

    rem ����Ƿ��� "����·��" ����
    if "!line:Persistent Routes=!" neq "!line!" (
        set "isPermanent=true"
    ) else if "!line:����·��=!" neq "!line!" (
        set "isPermanent=true"
    )

    rem �������"����·��"����
    if !isPermanent! equ true (
        echo !line! >> secpol.cfg
        rem �����м�����
        set /a "lineCount+=1"
        rem ����м������ﵽ5�����˳�ѭ��
        if !lineCount! equ 5 (
            goto :EndLoop
        )
    )
)

:EndLoop
rem ɾ����ʱ�ļ�
del temp.txt

endlocal
