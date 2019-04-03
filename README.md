# CSharpQRCode
C# 课程实验1 - QRCode 生成

## 功能概述

#### 基本功能

将命令行参数直接编码输出到控制台

![](https://raw.githubusercontent.com/8qwe24657913/CSharpQRCode/master/images/console.png)

#### 进阶功能：

- 使用文件作为数据源
  - `-f` 或 `--file` 指定作为数据源的文件，目前支持 `txt` `xls` `xlsx` 类型文件
  - `-e` 或 `--encoding` 指定输入的 `txt` 文件的编码，默认为 `UTF8` 编码

- 使用数据库作为数据源
  - `-d` 或 `--db-connect` 指定 [connect string](https://www.connectionstrings.com/)，也可参考 [MSDN 文档](https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlconnection.connectionstring)
  - `-c` 或 `--db-command` 指定查询使用的 SQL 语句
  - `-r` 或 `--db-row` 指定使用查询结果中的哪一列作为被编码数据
- `-o` 或 `--output` 指定输出路径
  - 若使用 `-f` 指定了文件作为数据源，则默认路径为同路径下同名文件夹
  - 若使用数据库作为数据源，则默认路径为命令行工作目录
  - 若直接输入数据，默认只输出到控制台，使用 `-o` 选项指定输出目录可使其转为输出到文件
- `-l` 或 `--logo` 可为输出的文件指定一个 Logo，注意直接输出到控制台时无法加入 Logo 故该选项无效
- `--help` 查看帮助
- 注：输出到文件时文件名以信息所在行号三位数+信息的前四个字符构成（若信息不足四个字符则有多少截取多少），由于信息中可能含有不能作为文件名的特殊字符，本程序会自动将其替换为 `-` 字符

![](https://raw.githubusercontent.com/8qwe24657913/CSharpQRCode/master/images/advanced.png)

## 项目特色

☑ 多种数据输入方式

☑ 可加入 Logo

☑ 可指定输出路径

☑ 可指定文件编码

☑ 对文件名中特殊字符的处理，增强健壮性

☑ 应用策略模式与迭代器模式，低耦合易扩展

## 代码总量

270+ 行

## 工作时间

约两晚

## 结论

- C# 的类型系统与 C++ 类似，与 Java 不同，如这次用到的 `IEnumerable<string>` 接口继承于 `IEnumerable` 接口，二者都有 `GetEnumerator()` 方法，但前者返回 `IEnumerator<string>` ，后者返回 `IEnumerator` ，`IEnumerator<string>` 继承于 `IEnumerator` 。如果是 Java 的类型系统，实现 `IEnumerator<string> GetEnumerator()` 方法就可以实现 `IEnumerable` 接口，但 C# 的类型系统在考虑接口实现时认为两个函数的函数签名不同，需要额外实现一个 `IEnumerator GetEnumerator()` 方法，这一点与 C++ 类似。
- 关于 XML 的读取，网上大多数教程都是使用的 `OldDbConnection` ，但 `OldDbConnection` 内部调用了一些微软的 COM 组件，这带来了与微软“全家桶”绑定的问题，导致编写出的代码在事实上难以跨平台运行或移植到其它平台，我使用了其它的包来规避这个问题。
