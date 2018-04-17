
# Welcome to the ToothpicExchange manual.
This HTML website contains the relevant information to setup, to use, and to further develop the ToothpicExchange system which has been created for Toothpic/OralEye Ltd.

This documentation has been created with [DocFx](https://dotnet.github.io/docfx/) (console version 2.23).
It contains static information alongside dynamic content generated from ToothpicExchange's source code.
The source code generated content utilises [C# XML Documentation Comments](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/xmldoc/xml-documentation-comments)
## How to use this website:
There are two main sections. 

The system overview is best for setting up and configuring the plugin. The API documentation may seem daunting at first, however it is only necessary for development of the source code.

### System Overview
This section contains general information about the system. Including:
1. General Introduction
2. How to Setup the Plugin
3. How to use the Plugin
4. How to setup the developer environment

The information in this section is static content contained in the project's [markdown files](https://daringfireball.net/projects/markdown/basics) (.md)

### API Documentation
This section contains a comprehensive reference of the source code. It details the classes, their methods, properties and more. It can be used in tandem with the source code, and it can be updated as the source code changes.

Much of this content is created from the XML tages within the source code. In order to update this content, the XML tags should be edited in the source code and the project rebuilt.

With DocFx.Console configured in Visual Studio, the site will update itself everytime the project is built.

The HTML files are output to a `_site` folder within the project.

Much of the content contained in this section is also visible within Visual Studio's [_IntelliSense_](https://code.visualstudio.com/docs/editor/intellisense). This is extremely convenient as the relevant documentation appears in Visual Studio as you type, there is no need to open a separate reference.

It should be noted that DocFx currently do not support publishing of `private` defined methods or attributes. Thus, these will not appear on the HTML site however they are still documented in Visual Studio through XML comments, and they will show up in IntelliSense.