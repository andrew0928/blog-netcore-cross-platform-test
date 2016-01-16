
## Using Ubuntu + Docker

最簡單的方式，直接使用 microsoft 提供的 container image 即可。
docker image: microsoft/dotnet:latest

可參考下列的指令:

```
sudo docker pull microsoft/dotnet
sudo docker run --rm -t -i microsoft/dotnet /bin/bash
```

進入 container 的 shell  之後，接這下這串指令，即可從 github 拉一份 code 下來編譯執行:

```
mkdir work
git clone https://github.com/andrew0928/blog-netcore-cross-platform-test
cd blog-netcore-cross-platform-test/CalcPI
dotnet restore --runtime coreclr
dotnet compile --framework dnxcore50
dotnet run 1 10000
dotnet run 2 10000
```
