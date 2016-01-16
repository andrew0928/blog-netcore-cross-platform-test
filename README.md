
## 如何在 Ubuntu 上面執行? 

最簡單的方式，直接使用 microsoft 提供的 container image 即可。
docker image: microsoft/dotnet:latest

可參考下列的指令:

```
sudo docker pull microsoft/dotnet
sudo docker run --rm -t -i microsoft/dotnet /bin/bash
```

這個 solution 內包含兩個測試 project，分別是 CalcPI 及 MemTest, 兩者使用方式類似，
進入 container 的 shell  之後，接這用這串指令，即可從 github 拉一份 code 下來編譯執行:

```
mkdir work
git clone https://github.com/andrew0928/blog-netcore-cross-platform-test
cd blog-netcore-cross-platform-test/CalcPI
dotnet restore 
dotnet compile 
dotnet run 1 10000
dotnet run 2 10000
```

若要執行 CalcPI:
```
cd blog-netcore-cross-platform-test/CalcPI
dotnet restore 
dotnet compile 
dotnet run 1 10000
dotnet run 2 10000
```

若要執行 MemTest:
```
cd blog-netcore-cross-platform-test/MemTest
dotnet restore 
dotnet compile 
dotnet run
```


