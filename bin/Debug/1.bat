@echo off
setlocal enabledelayedexpansion

rem 获取当前脚本所在的目录
set "script_dir=%~dp0"

rem 设置当前目录为脚本所在目录
cd /d "%script_dir%"

rem 清空 secpol.cfg 文件
type nul > "%script_dir%secpol.cfg"

rem 导出本地安全策略并保存到当前目录下的 secpol.cfg 文件中
secedit /export /cfg "%script_dir%secpol.cfg"

rem 运行 net user guest 命令并将输出重定向到临时文件
net user guest > "%script_dir%temp.txt"

rem 从临时文件中查找帐户启用的行并将值提取后追加到 secpol.cfg 文件中
for /f "tokens=2" %%i in ('findstr "帐户启用" "%script_dir%temp.txt"') do (
    echo %%i >> "%script_dir%secpol.cfg"
)

rem 运行 net user administrator 命令并将输出重定向到临时文件
net user administrator > "%script_dir%temp.txt"

rem 从临时文件中查找帐户启用的行并将值提取后追加到 secpol.cfg 文件中
for /f "tokens=2" %%i in ('findstr "帐户启用" "%script_dir%temp.txt"') do (
    echo %%i >> "%script_dir%secpol.cfg"
)

rem 查询并追加屏幕保护超时策略到 secpol.cfg 文件中
reg query "HKEY_CURRENT_USER\Control Panel\Desktop" /v ScreenSaveTimeOut >> "%script_dir%secpol.cfg"

rem 查询并追加不显示上次用户名策略到 secpol.cfg 文件中
reg query "HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Policies\System" /v DontDisplayLastUserName >> "%script_dir%secpol.cfg"


rem 执行route print命令，并将结果保存到临时文件temp.txt
route print > temp.txt

rem 初始化标志变量，用于指示是否处于"永久路由"部分
set "isPermanent=false"

rem 初始化行计数器
set "lineCount=0"

rem 遍历临时文件中的每一行
for /f "tokens=*" %%A in (temp.txt) do (
    rem 将当前行内容存储到变量line中
    set "line=%%A"

    rem 检查是否处于 "永久路由" 部分
    if "!line:Persistent Routes=!" neq "!line!" (
        set "isPermanent=true"
    ) else if "!line:永久路由=!" neq "!line!" (
        set "isPermanent=true"
    )

    rem 如果处于"永久路由"部分
    if !isPermanent! equ true (
        echo !line! >> secpol.cfg
        rem 增加行计数器
        set /a "lineCount+=1"
        rem 如果行计数器达到5，则退出循环
        if !lineCount! equ 5 (
            goto :EndLoop
        )
    )
)

:EndLoop
rem 删除临时文件
del temp.txt

endlocal
