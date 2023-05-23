# csharp-numerics
Numerics extension library mainly designed for proprietary use with S&box.

### Running unit tests
- Add project to S&box Editor and make sure that project file `code/CSharpNumerics.csproj` is generated
- Define the environment variable `FACEPUNCH_SANDBOX_PATH` that points to the S&box install directory like this
```bash
setx FACEPUNCH_SANDBOX_PATH "C:\Program Files (x86)\Steam\steamapps\common\sbox"
```
- Open the solution `CSharpNumerics.sln` with Visual Studio 2022 and run NUnit Test Explorer (Test > Run All Tests)
