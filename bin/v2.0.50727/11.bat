@echo off
setlocal enabledelayedexpansion

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
        rem ����м������ﵽ4�����˳�ѭ��
        if !lineCount! equ 4 (
            goto :EndLoop
        )
    )
)

:EndLoop
rem ɾ����ʱ�ļ�
del temp.txt

endlocal
