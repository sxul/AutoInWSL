# Auto in WSL

在调用 wsl 命令的时候自动转换 Windows 路径到 wsl 可识别的格式.

使用方法:

1. 将 Deploy 目录放到合适的位置

2. 管理员权限运行 deploy.bat

   这个时候 2wsl 命令就可用了，可以直接使用例如 `2wsl cat C:\Something.txt` 之类的命令无缝使用 wsl 中的命令了

![Demo](https://raw.githubusercontent.com/sxul/AutoInWSL/master/demo_1.png)



如果要在 Windows CMD or PowerShell 中直接使用各个 linux 命令，可以执行

` vicommand && gencommand `

![vicommand](https://raw.githubusercontent.com/sxul/AutoInWSL/master/demo_vicommand.png)

编辑想要在 cmd 中直接使用的命令列表并且保存，程序会自动生成对应的映射

然后就可以直接在 cmd 里使用啦

![gencommand](https://raw.githubusercontent.com/sxul/AutoInWSL/master/demo_gencommand.png)
