# C# 程式碼範例集合

這是一個簡單的 C# 程式碼範例集合。

| Example                                         | Description                                                   | Platform              |
| ----------------------------------------------- | ------------------------------------------------------------- | --------------------- |
| [ImageToBase64](./ImageToBase64/)               | Convert image to base64 string                                | Windows, macOS, Linux |
| [LogHelper](./LogHelper/)                       | Print log to Windows Event Viewer                             | Windows               |
| [xBitmap](./xBitmap/)                           | Convert image to grayscale                                    | Windows               |
| [xEnum](./xEnum/)                               | Demonstrate how to use an attribute to extend an Enum         | Windows, macOS, Linux |
| [xFolderBrowserDialog](./xFolderBrowserDialog/) | Open a folder browser dialog with multi-select                | Windows               |
| [xProgressBar](./xProgressBar/)                 | Progress Bar with Text                                        | Windows               |
| [xThread](./xThread/)                           | Demonstrate how to use Thread                                 | Windows, macOS, Linux |
| [xUSB](./xUSB/)                                 | List USB devices via WMI (Windows Management Instrumentation) | Windows               |

### Quick Start

#### 安裝 . NET SDK

* Windows

```bash
choco install -y dotnet-6.0-sdk
dotnet --list-sdks
```

* macOS

```bash
# https://github.com/isen-ng/homebrew-dotnet-sdk-versions
brew tap isen-ng/dotnet-sdk-versions
brew install --cask dotnet-sdk6-0-400
dotnet --list-sdks
```

* Linux

```bash
# asdf plugin add dotnet https://github.com/hensou/asdf-dotnet.git
asdf plugin add dotnet
asdf install dotnet 6.0.419
asdf local dotnet 6.0.419
dotnet --list-sdks
```

#### 建立專案

```bash
dotnet new console --framework net6.0 --use-program-main --name <project-name>
```

```bash
dotnet sln add <project-name>
```

#### 執行專案

```bash
dotnet run --project <project-name>
```

#### 新增套件

```bash
dotnet add <project-name> package <package-name>
```
