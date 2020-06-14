AspNetCoreConfig
================

### Overview

AspNetCoreConfig is a .Net Core 3.1 application that demonstrates different aspects of configuration in an ASP.NET Core Web Application. It starts with a basic IConfiguration page, then pages for IOptions, IOptionsSnapshot, IOptionMoniter, then validation and finally options forward via an interface.

### Compiling

This was built in Visual Studio Community 2019 on a windows machine. Perform the following steps to compile the project:
1.	Clone the repo.
1.	Open the solution (AspNetCoreConfig.sln) file in Visual Studio
1.	Hit F6 to build the project

Alternatively, you can compile from the command line:
1.	Clone the repo.
1.	Open a console window and navigate to the root of the project (same location as the AspNetCoreConfig.sln file)
1.	Run the following:
```
    dotnet restore
    dotnet build
```

### Running

From Visual Studio:
1.  Hit F5 to run the project.
2.  A browser should open, and the project’s home page should be displayed.

From a console window:
1.  Type the following:
```
    dotnet run --project .\AspNetCoreConfig\AspNetCoreConfig.csproj
```
2.  Open a browser and set the address to  http://localhost:5000

### Demonstration

By default, there is a hierarchy to overriding appSettings configuration.
1.	Load configuration from appSettings.json
1.	Load configuration from appSettings.{environment}.json
1.	When environment is ‘Development’, load configuration form user-secrets
1.	Load configuration from environment variables
1.	Load configuration from dotnet command-line variables

 We can start by looking at the folowing configuration in appSettings.json.

 ```
 "MyStuff": {
  "MyValues": {
    "Value1": "Default1",
    "Value2": "11",
    "Value3": true,
    "About": {
      "Author": "Richard",
      "Version": "1.1.1"
    }
  }
},
 ```

Because projects default to run in a **Development** environment, open the **AspNetCoreConfig\Properties\launchSettings.json** file and change the **ASPNETCORE_ENVIRONMENT** to **“Local"** (change it in two locations in the file). Now run the project. You should see the home page displaying the the following:
<div align="center"> <h1>Configuration</h1>

Default1

11

True

Richard

1.1.1
</div>

Stop the project. Change the **ASPNETCORE_ENVIRONMENT** variable, in the **AspNetCoreConfig\Properties\launchSettings.json** file, back to **“Development"**. Run the project again. You should now see the following on the homepage, notice that the first three settings are now being pulled from the **appsettings.Development.json** file:
<div align="center"> <h1>Configuration</h1>

Dev1

3

True

Richard

1.1.1
</div>

With the **ASPNETCORE_ENVIRONMENT** variable still set to **“Development”**, stop the project and enter the following at the command line or in the **Package Management Console** in Visual Studio:

```
dotnet user-secrets init --project .\AspNetCoreConfig\AspNetCoreConfig.csproj
dotnet user-secrets set "MyStuff:MyValues:Value1" "Secret1" --project .\AspNetCoreConfig\AspNetCoreConfig.csproj
```
Run the project again, you should now see the following with the first setting changed to **Secret1**:
<div align="center"> <h1>Configuration</h1>

Secret1

3

True

Richard

1.1.1
</div>

Stop the project again. From the **Package Management Console** in Visual Studio, run the following to set an environment variable:
```
$env:MyStuff__MyValues__Value1 = "Env1"
```

Run the project again. You should now see the following with the first setting changed to **Env1**:
<div align="center"> <h1>Configuration</h1>

Env1

3

True

Richard

1.1.1
</div>

Stop the project again. Open a command window and run the following:
```
dotnet run MyStuff:MyValues:Value1=Cmd1 --project AspNetCoreConfig\AspNetCoreConfig.csproj
```
Open a browser and you should now see the following with the first setting changed to **Cmd1**:

<div align="center"> <h1>Configuration</h1>

Cmd1

3

True

Richard

1.1.1
</div>

These are the overrides that are provided out-of-the-box, but be aware that it is possible to install NuGet packages to pull configuration settings from **Azure Key Vault**, **Azure App Settings**, **AWS Parameter Store** or you can even write you own provider. For further, information your can check out [Steve Gordon’s PluralSight course](https://app.pluralsight.com/library/courses/dotnet-core-aspnet-core-configuration-options/table-of-contents).


### IConfiguration (Home page)

The **AspNetCoreConfig\Controllers\HomeController.cs** file contains an example of accessing configuration settings via **IConfiguration** which is injected in the constructor.

### IOptions

The **AspNetCoreConfig\Controllers\OptionsController.cs** file contains an example of accessing configuration setting via **IOptions** which is injected in the constructor.

Note that for the options to be injected the following was added in the **Startup.cs** file to set up the **IOptions** object:
```
services.Configure<MyValuesOptions>(Configuration.GetSection("MyStuff:MyValues"));
```

### IOptionsSnapshot

The AspNetCoreConfig\Controllers\OptionsSnapshotController.cs file contains an example of accessing configuration settings via **IOptionsSnapshot** which is injected in the constructor.

Note that for the options to be injected the following was added in the **Startup.cs** file:
```
services.Configure<MyValuesOptions>(Configuration.GetSection("MyStuff:MyValues"));
```

### IOptionsMonitor

The **AspNetCoreConfig\Controllers\OptionsMonitorController.cs** file contains an example of accessing configuration settings via **IOptionsMonitor** which is injected in the constructor.

Note that for the options to be injected the following was added in the **Startup.cs** file:
```
services.Configure<MyValuesOptions>(Configuration.GetSection("MyStuff:MyValues"));
```

### Named Options
The **AspNetCoreConfig\Controllers\ NamedOptionsController.cs** file contains an example of accessing configuration settings via string names that are set up in the **Startup.cs** file.

Note the following in the **Startup.cs** file:
```
services.Configure<SiteOption>("Site1", Configuration.GetSection("Credentials:Site1"));
services.Configure<SiteOption>("Site2", Configuration.GetSection("Credentials:Site2"));
```

### Annotation Validation
The **AspNetCoreConfig\Controllers\AnnotationValidationController.cs** file contains an example of validating configuration.

Note there are three different commented sections of in the **Startup.cs** file which you can try uncommenting, one at a time, and testing the different scenarios.


### Options Forward Via Interface
The **AspNetCoreConfig\Controllers\ OptionsForwardViaInterfaceController.cs** file contains an example of how you can forward an **IOption** into an interface and then get the interface injected into your constructor.

Note the following in the **Startup.cs** file:
```
services.Configure<MyValuesConfiguration>(Configuration.GetSection("MyStuff:MyValues"));
services.AddSingleton<IMyValuesConfiguration>(s =>
    s.GetRequiredService<IOptions<MyValuesConfiguration>>().Value);
```


## Some Links:
https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-3.1
https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=windows
https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-3.1


## Reset Demo Environment
```
$env:MyStuff__MyValues__Value1 = ""
dotnet user-secrets remove "MyStuff:MyValues:Value1"  --project .\AspNetCoreConfig\AspNetCoreConfig.csproj
```