# About APIClient.CalculatorAPI

This is a solution to explore creating multiple versions of calculator API clients using .NET Standard 2.0.

## v1

Will consume a calculator SOAP API service: http://www.dneonline.com/calculator.asmx

## v2

Will consume a calculator REST API service: https://api.mathjs.org/

## Step by implementation

### Project creation

- Create a new project based on the _Class Library (.NET Standard) C#_ template and set the project name to _APIClient.CalculatorAPI_.
- Create this _README.md_ file in the solution folder.
- Right click in the solution and select _Add -> New Solution Folder_. Rename it to _Documents_.
- Right click in the _Documents_ folder and select _Add -> Existing Item..._. Select the _README.md_ file.

### Client version 1 creation

- Add _v1_ and  _v1/Schemas_ folders to the project.
- If you have the _WSDL_ _URL_ and don´t have the _XSD_ files you can use the [Microsoft Web Services Discovery Tool](https://docs.microsoft.com/en-us/previous-versions/dotnet/netframework-4.0/cy2a3ybs(v=vs.100)?redirectedfrom=MSDN) to discover the wsdl and generate the _XSD_ files manually:
  - Open the _Terminal_ window and _discover_ the wsdl with this script: ```disco /o:APIClient.CalculatorAPI/v1/Schemas http://www.dneonline.com/calculator.asmx?WSDL```
  - Now generate manually a new _XSD_ file for each _definitions -> types -> schema -> element_.
- Create the classes that correspond to the specific schema using the [Microsoft XML Schema Definition Tool](https://docs.microsoft.com/en-us/previous-versions/dotnet/netframework-1.1/x6c1kb0s(v=vs.71)):
  - Create a the _v1/Schemas/generate.bat_ file.
  - Add the script.
  - Ejecute the _bat_ file.
- Generate the _common_ folder.
- Generate the _common/Serializer.cs_ class.
- Generate the _v1/Client.cs_ class.

### Client version 2 creation

- Add _v2_ folder to the _APIClient.CalculatorAPI_ project.
- Generate the _v2/Client.cs_ class.
- Add _v2/Models_ folder to the _APIClient.CalculatorAPI_ project.
- Add _OperationResult_ and _OperationRequest_ classes for data transfer.
- Add _Add_, _Divide_, _Multiply_ and _Subtract_ model classes.

### Project test creation

- Create a new project based on the _MSTest Test Project (.NET Core)_ template and set the project name to _APIClient.CalculatorAPI.Tests_.
- Add the _v1Tests.cs_ and _v2Tests.cs_ classes to the test project.