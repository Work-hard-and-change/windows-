@echo off
setlocal enabledelayedexpansion

rem ����һ����־λ����ʾ�Ƿ��Ѿ����� "����·��:" ������
set "found_persistent_routes=0"

rem ѭ����ȡ route print �����
for /f "tokens=1,2,3,4,5" %%a in ('route print') do (
    rem ����Ƿ��Ѿ��ҵ� "����·��:" �����µ�����
    if "!found_persistent_routes!"=="1" (
        rem ����ҵ�������ȡĿ�����粢ɾ��·��
        if "%%a" neq "" (
            route delete %%a
        )
    ) else (
        rem �����û�ҵ�����·�������µ����ݣ����鵱ǰ���Ƿ��������·�ɵı�־
        if /i "%%a"=="����·��:" if /i "%%b"=="Persistent Routes:" (
            set "found_persistent_routes=1"
        )
    )
)

rem ��ʾɾ���ɹ�
echo All persistent routes have been deleted.

pause
