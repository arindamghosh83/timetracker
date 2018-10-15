export const environment = {
  production: true,
  appInsights: {
    instrumentationKey: '17186df4-ae39-4221-bcce-ac449e838de2'
  },
  endpoints: {
    apiBaseUri: 'https://cmresearchinformatics.azure-api.net/timetracker/',
    subscriptionKey: '36d13c5821bf441cb6113bbd8ba0e57d'
  },
  adalConfig: {
    tenant: 'childrensmercy.onmicrosoft.com',
    clientId: '84157a65-a4df-4839-83ab-b9eb7662a98c',
    redirectUri: 'https://rctimetracker.azurewebsites.net',
    postLogoutRedirectUri: 'https://rctimetracker.azurewebsites.net/logout',
    apiId: '012d6ad8-352d-4350-a330-d46937c54b3f',
    apiIdUri: 'https://childrensmercy.onmicrosoft.com/TimeTracker.API',
    isAngular: true
  }
};
