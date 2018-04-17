# Setting up the environment

The technical environment to set up for the plugin can be quite complex.
It is fairly straightforward to load the plugin directly to an instance of OpenDental (e.g. the [trial version](http://www.opendental.com/trial.html)). 
[These instructions](plugin_installation.md) outline that process.
However, in order to setup a development environment you must follow the steps below.

## Visual Studio Installation

It is advisable to install [Visual Studio IDE 2017](https://www.visualstudio.com) with a minimum .NET 4.5 and C# 7.2

Once this is done, the root`ToothpicExchange folder (the folder directly containing the `ToothpicExchange.sln` file) should be copied or set up in a new folder.
This new folder will contain all of the plugin source code.

## OpenDental Developer Installation

The OpenDental source code must now be downloaded. This is a two step process.

1. The first step involves downloading the trial version of OpenDental and installing it. This installs all of OpenDental's additional dependencies and sets up the MySQL server locally.
Follow the [OpenDental instructions](http://www.opendental.com/trial.html) for advice on how to install OpenDental locally.

2. The next step is to actually download the source code. This can only be done through OpenDental's version control system, Subversion.
Please consult [this webpage](http://www.opendental.com/manual/sourcecode.html) for instructions how to download and set up OpenDental's source code.
It is advised to store the OpenDental source code in the same HEAD folder as the ToothpicExchange folder.

Now, open the `ToothpicExchange.sln` solution file to begin setting up the technical environment.

In the Visual Studio Solution Explorer (top right hand side) there should be three projects:
- OpenDental
- OpenDentBusiness
- ToothpicExchange

1. It is likely that the two OpenDental packages will be indicated as "missing". If that is the case, you must right click and remove them from the solution.

2. Next, you must right click on the solution and click `Add` > `Existing Project`

	Browse to the following and add them: `../OpenDental/OpenDental.csproj` and `../OpenDentBusiness/OpenDentBusiness.csproj`

3. Next, right click on the ToothpicExchange project in Solution Explorer and click `Add` > `Reference`.

	On the right hand side click `Projects` > `Solution` and enable both OpenDental and OpenDentBusiness.

4. Now right click on the ToothpicExchange project in Solution Explorer and click `Properties`.

	Select `Build Events` on the left hand side, and edit the "Post-Build event command line".
	This should reference to a batch file called CopyDllToOd.bat located in the project folder. 
	
	You will need to alter the absolute path in the properties so that it points to the correct file.
	This file takes care of transferring Visual Studio's DLL build of the plugin into the required folder in the OpenDental debug bin.

5. Now, open the `CopyDllToOd.bat` file in the project folder, and edit the absolute path to match the OpenDental `bin/debug` folder.

6. Right click on the OpenDental project in Solution Explorer and set it as the startup project.

7. Finally, try build the project and see if it runs. It could take a while initially.

For troubleshooting, and further info, consult the OpenDental [plugin setup page](http://opendental.com/manual/plugins.html)

## Setting up JSON-Server

1. Setup a folder to contain the server files and navigate to it in the Command Prompt.

2. Json-server runs on Node.js, so you will need to install the Node.js package manager (npm).

	Navigate [here](https://www.npmjs.com/get-npm) to download and install npm

3. Now you can utilise npm to install json-server. Run the following in the Command Prompt:

	`npm install -g json-server`

4. Create a `db.json` file, there is a template included in the project folder.

5. Run the following command:

	`json-server db.json`

	This will start a new local server operating on port 3000. It will respond to GET requests for any objects contained in the `db.json` file, and it will add any new objects to the `db.json` with a POST request.

For further information, consult the [json-server github](https://github.com/typicode/json-server).

## Producing Documentation

Navigate to the [DocFx website](http://dotnet.github.io/docfx/) for information on how to download and setup the latest version of DocFx.

This project utilises the DocFx.console variant which can be downloaded in Visual Studio through the NuGet Package Manager.

There are `docfx.json` and `docfxPDF.json` files in the project directory. These files are used to specify the DocFx configuration. It is recommended these are not changed, unless you are familiar with the DocFx environment