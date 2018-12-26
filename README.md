# How to display documents from the Report and Dashboard Server in an ASP.NET Core MVC application

This example demonstrates how to use the [Report and Dashboard Server](https://docs.devexpress.com/ReportServer/12432/index)'s API to obtain available documents and display them in the Document/Dashboard Viewers.

The table below lists the controls that display documents depending on their types:

| Document Type | Control | Configuration Options |
|---|---|---|
| Report | [ASP.NET Core Document Viewer](https://docs.devexpress.com/XtraReports/400248/create-end-user-reporting-applications/web-reporting/asp-net-core-reporting/document-viewer) | [WebDocumentViewerBuilder.RemoteSourceSettings](https://docs.devexpress.com/XtraReports/DevExpress.AspNetCore.Reporting.WebDocumentViewer.WebDocumentViewerBuilder.RemoteSourceSettings(Action-RemoteSourceSettings-)) |
| Dashboard | [ASP.NET Core Dashboard Control](https://docs.devexpress.com/Dashboard/115163/building-the-designer-and-viewer-applications/web-dashboard/asp.net-core-dashboard-control) (in the [ViewerOnly](https://docs.devexpress.com/Dashboard/119983/building-the-designer-and-viewer-applications/web-dashboard/asp.net-core-mvc-dashboard-control/designer-and-viewer-modes) mode) | [DashboardBuilder.BackendOptions](https://docs.devexpress.com/Dashboard/DevExpress.DashboardAspNetCore.DashboardBuilder.BackendOptions(System.Action-DevExpress.DashboardAspNetCore.DashboardBackendOptionsBuilder-)) |

Before running the example, perform the following steps:

**1. Configure the Report and Dashboard Server**

* Configure the Report and Dashboard Server to use the HTTPS protocol.

* [Create a user account](https://docs.devexpress.com/ReportServer/14361/administrative-panel/manage-user-accounts-and-grant-security-permissions) with Server authentication and give this account permissions to view documents. The account's credentials will be used to obtain a [Bearer token](https://oauth.net/2/bearer-tokens/), which is required to access the Report and Dashboard Server's API.

* Enable Cross-Origin Resource Sharing (CORS) on the screen with the [General Settings](https://docs.devexpress.com/ReportServer/119485/administrative-panel/manage-server-settings/general-settings) and restart the Report and Dashboard Server to apply the changes.

**2. Download Resources and Specify Server Settings**

* Refer to the [https://nuget.devexpress.com/#feed-url](https://nuget.devexpress.com/#feed-url) page and [obtain the NuGet Feed URL](https://docs.devexpress.com/GeneralInformation/116042/installation/install-devexpress-controls-using-nuget-packages/obtain-your-nuget-feed-url). This URL includes your personal feed authorization key.

* Open the console and navigate to the example's **CS** folder. Run the commands below to restore dependencies from the DevExpress (with the obtained NuGet Feed URL) and default NuGet package sources:

    ``dotnet restore -s https://nuget.devexpress.com/<auth_key>/api``

    ``dotnet restore``

* Use the following command to install all the necessary [npm](https://www.npmjs.com/) packages:

    ``npm ci``

* Open the **appsettings.json** file and assign your Report and Dashboard Server's URI to the **ReportServerBaseUri** property.

* In the console, generate user secrets for the Server account's username and password:

    ``dotnet user-secrets set "ReportServer:UserName" "<username>"``

    ``dotnet user-secrets set "ReportServer:UserPassword" "<password>"``

* Run the command below to trust the HTTPS certificate for ASP.NET Core development:

    ``dotnet dev-certs https --trust``

**Run the Example**

Use the following command to build the example: 

``dotnet run``

Open your browser on _http://localhost:5000/_ or _https://localhost:5001/_ to see the result.


