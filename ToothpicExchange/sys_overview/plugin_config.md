# Configuring the Plugin

There are a few minor configuration items to tackle before deploying the plugin.

### API Base Url

In the `APISettings.settings` file contained within the project folder, there is a property labelled `ToothpicAPIBaseURI`. This value is the API's base URL.

This property is referenced in `ToothpicAPI.cs`. The `APISettings.settings` file is compiled when the project is built, so this setting must be configured before building and deploying the plugin.

### API Endpoints

In `ToothpicAPI.cs` there are two private variables `authEndpoint` and `userEndpoint`. These can be changed as necessary.


_Currently there is no other configuration required._