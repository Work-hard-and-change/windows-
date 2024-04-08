@echo off
setlocal enabledelayedexpansion

rem 设置一个标志位来表示是否已经到达 "Persistent Routes:" 行以下
set "found_persistent_routes=0"

rem 循环读取 route print 的输出
for /f "tokens=1,2,3,4,5" %%a in ('route print') do (
    rem 检查是否已经找到 "Persistent Routes:" 行以下的内容
    if "!found_persistent_routes!"=="1" (
        rem 如果找到，则提取目标网络并删除路由
        if "%%a" neq "" (
            route delete %%a
        )
    ) else (
        rem 如果还没找到 "Persistent Routes:" 行以下的内容，则检查当前行是否包含该标志
        if "%%a"=="Persistent" if "%%b"=="Routes:" (
            set "found_persistent_routes=1"
        )
    )
)

rem 提示删除成功
echo All persistent routes have been deleted.

