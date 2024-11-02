az group create --name MyFeeds --location francecentral
az deployment group create --resource-group MyFeeds --template-file main.bicep --parameters appInsightsLocation=francecentral