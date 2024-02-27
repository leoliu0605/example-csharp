# C# 程式碼範例集合

這是一個簡單的 C# 程式碼範例集合。

### 安裝 . NET SDK

* Windows

```bash
choco install -y dotnet-6.0-sdk dotnet-8.0-sdk
```

* macOS / Linux

```bash
# asdf plugin add dotnet https://github.com/hensou/asdf-dotnet.git
asdf plugin add dotnet
asdf install dotnet 6.0.419
asdf install dotnet 8.0.201
asdf global dotnet 6.0.419
asdf local dotnet 6.0.419
dotnet --version
```

### 建立專案

```bash
dotnet new console --framework net6.0 --use-program-main -n <project-name>
```

```bash
dotnet sln add <project-name>/<project-name>.csproj
```

### 執行專案

```bash
dotnet run --project <project-name>/<project-name>.csproj
```
