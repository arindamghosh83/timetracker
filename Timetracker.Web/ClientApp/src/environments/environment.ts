// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.

export const environment = {
  production: false,
  appInsights: {
    instrumentationKey: ''
  },
  endpoints: {
    apiBaseUri: 'https://cmresearchinformatics.azure-api.net/timetracker/',
    subscriptionKey: '36d13c5821bf441cb6113bbd8ba0e57d'
  },
  adalConfig: {
    tenant: 'childrensmercy.onmicrosoft.com',
    clientId: '84157a65-a4df-4839-83ab-b9eb7662a98c',
    redirectUri: 'http://localhost:5000/',
    postLogoutRedirectUri: 'http://localhost:5000/logout',
    apiId: '012d6ad8-352d-4350-a330-d46937c54b3f',
    apiIdUri: 'https://childrensmercy.onmicrosoft.com/TimeTracker.API',
    isAngular:true
  }
};
