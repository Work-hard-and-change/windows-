@echo off
setlocal enabledelayedexpansion

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
        rem 如果行计数器达到4，则退出循环
        if !lineCount! equ 4 (
            goto :EndLoop
        )
    )
)

:EndLoop
rem 删除临时文件
del temp.txt

endlocal
