# C# 程式碼範例集合

這是一個簡單的 C# 程式碼範例集合。

| Example                                             | Description                                                   | Platform              |
| --------------------------------------------------- | ------------------------------------------------------------- | --------------------- |
| [CrossImageToBase64](./CrossImageToBase64/)         | Convert image to base64 string                                | Windows, macOS, Linux |
| [CrossRawToDng](./CrossRawToDng/)                   | Convert RAW image to DNG format                               | Windows, macOS, Linux |
| [CrossUSB](./CrossUSB/)                             | Control USB device across platform                            | Windows, macOS, Linux |
| [DemoEnum](./DemoEnum/)                             | Demonstrate how to use an attribute to extend an Enum         | Windows, macOS, Linux |
| [DemoThread](./DemoThread/)                         | Demonstrate how to use Thread                                 | Windows, macOS, Linux |
| [WinBitmap](./WinBitmap/)                           | Convert image to grayscale                                    | Windows               |
| [WinFolderBrowserDialog](./WinFolderBrowserDialog/) | Open a folder browser dialog with multi-select                | Windows               |
| [WinLogHelper](./WinLogHelper/)                     | Print log to Windows Event Viewer                             | Windows               |
| [WinProgressBar](./WinProgressBar/)                 | Progress Bar with Text                                        | Windows               |
| [WinUSB](./WinUSB/)                                 | List USB devices via WMI (Windows Management Instrumentation) | Windows               |

### 系統需求

- 對於 Windows，需要 [Chocolatey](https://chocolatey.org/install) 包管理器
- 對於 macOS，需要 [Homebrew](https://brew.sh/)
- 對於 Linux (Debian/Ubuntu)，需要 apt-get

#### 安裝 . NET SDK

根據您的操作系統，自動安裝 . NET 6.0 SDK：

```bash
make install
```

#### 新增專案

新增一個名為 `<project-name>` 的 . NET 控制台專案：

```bash
make new NAME=<project-name>
```

#### 刪除專案

刪除一個名為 `<project-name>` 的專案，包括它的所有檔案和目錄：

```bash
make remove NAME=<project-name>
```

#### 列出 SDK 和專案

列出已安裝的 . NET SDK 版本和解決方案中的專案：

```bash
make list
```

#### 運行專案

運行一個名為 `<project-name>` 的專案：

```bash
make run NAME=<project-name>
```

#### 添加套件

向名為 `<project-name>` 的專案添加名為 `<package-name>` 的套件：

```bash
make add NAME=<project-name> PACK=<package-name>
```

#### 注意

- 確保在使用任何命令之前，您已經安裝了所有必要的依賴項。
- 專案名稱 (`NAME`) 和套件名稱 (`PACK`) 是必須手動指定的參數。
- 如果未在命令行指定 `NAME`，系統將嘗試從 `.env` 檔案讀取或提示您輸入。

## 待新增

- https://github.com/Tichau/FileConverter
- https://github.com/mini-software/MiniExcel
- https://github.com/JamesNK/Newtonsoft.Json
- https://github.com/spectreconsole/spectre.console
- https://github.com/gui-cs/Terminal.Gui
- https://github.com/ScottPlot/ScottPlot
- https://github.com/dorssel/usbipd-win
- https://github.com/thomhurst/TUnit
- https://github.com/m4rs-mt/ILGPU
