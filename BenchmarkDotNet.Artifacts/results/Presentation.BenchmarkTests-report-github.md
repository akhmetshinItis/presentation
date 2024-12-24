```

BenchmarkDotNet v0.14.0, macOS Sequoia 15.1.1 (24B91) [Darwin 24.1.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK 8.0.403
  [Host]     : .NET 8.0.10 (8.0.1024.46610), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 8.0.10 (8.0.1024.46610), Arm64 RyuJIT AdvSIMD


```
| Method        | Mean       | Error       | StdDev      | Gen0     | Gen1    | Gen2    | Allocated   |
|-------------- |-----------:|------------:|------------:|---------:|--------:|--------:|------------:|
| EF_Insert     | 8,130.7 μs | 1,142.14 μs | 3,367.64 μs | 216.7969 | 27.3438 | 19.5313 | 15650.03 KB |
| Dapper_Insert |   543.1 μs |     5.89 μs |     4.92 μs |        - |       - |       - |     2.31 KB |
