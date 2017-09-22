# Demos and presentation about Azure AppService, Thessaloniki .NET Meetup, 21.9.2017 #

I had an opportunity to talk at Thessaloniki .NET Meetup via Skype about Azure AppService. As part of presentation we went deep into AppService as such, focusing on:

1. [Azure WebApp](https://azure.microsoft.com/en-us/services/app-service/web/) - we discussed and look into specifics for hosting web application on Microsoft Paas solution on Windows platforms:
	1. creation via portal, exploring settings, deployment options, kudu and [kuduexec.net](https://github.com/projectkudu/KuduExec.NET), AutoHeal, logistics and process explorer and going into bare REST with resource explorer
	2. deployment via Visual Studio and other possibilites, that Visual Studio offers - explaining what is happening behind the scene
	3. how to set custom domain and how to wire everything up
	3. controlling Azure WebApp through [Fluent Management APIs](https://github.com/Azure/azure-sdk-for-net/tree/Fluent)
4. [Azure API App](https://azure.microsoft.com/en-us/services/app-service/api/) - we explore abilities, that API apps offers - sending email via SendGrid via Azure
	1. CORS control
	2. swagger creation and modification
	3. exposing the API to the world and consuming it via AutoRest in Visual Studio
4. [Azure Logic App](https://azure.microsoft.com/en-us/services/logic-apps/) - showing endless options that you can do with designer and how to connect to our own services via HTTPS
	1. connect to services and creating workflow - we connected to twitter service and enable creation of workflow from triggering every 10s twitter and then calling our API app, which then sended an email to us with content in that tweet
2. [Azure Mobile App](https://azure.microsoft.com/en-us/services/app-service/mobile/) - exploring options, how to leverage Easy Tables and data connection to SQL database to leverage MobileServices in mobile clients
	1. we created UWP application, which uses Azure Mobile App behind the scenes to explore the API's
	2. we used Connected Service to add Mobile backend to the app and to register client for communicating with backend
	3. we then used CRUD operations on backend to explore possibilites, that Mobile Services offers
4.[ Azure Functions](https://azure.microsoft.com/en-us/services/functions/) - we checked, what does it take to create some code to react to some events in the cloud without creating the whole infrastructure behind the scene
	1. we focused on creating application in [C# scripting](http://scriptcs.net/) in order to show, how can we leverage in browser coding tools
	2. we went into details, what is behind the scenes, how triggers and output are defined
	3. then we explored, how the tools can be helpful and how to connect different function together in a flow

All of the demos are available [here](http://data.vrhovnik.net/web/Greece%20Meetup%20Demos.zip). Slides are available [here](http://data.vrhovnik.net/web/Azure%20App%20Service.pdf). 

![](http://data.vrhovnik.net/pics/PicOverview.png)

In order to use the demos, please note, that you will need to restore packages. You can find instructions, how to do that, [here](https://docs.microsoft.com/en-us/nuget/consume-packages/package-restore).

You can send me additional questions through my [webpage](https://vrhovnik.net/about) or my [twitter account](https://twitter.com/bvrhovnik).
