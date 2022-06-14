# Workshop-Market

In this project the server is implemented in C# whereas the client is implemented in type-script using react.

## running the server

- Make sure you have installed:
  - Visual Studio  
  - [.Net Frameowrk 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- Open the [MarketBackend](Server\MarketBackend\MarketBackend.sln) project via Visual Studio as an existing project
- Open the [Solution Explorer](https://docs.microsoft.com/en-us/visualstudio/ide/use-solution-explorer?view=vs-2022) and right-click on the WebAPI project ![WebAPI project](Images\SolutionExplorerScreenshot.jpg).
- Now select "Set as Startup Project".
- Click the "Run" button at the top of the screen.
- After running the project, a browser page named "Swagger" should open automatically. You can ignore that.

## running the client

It will be more comefortable to run the client from Visual Studio Code, but it's not mandatory.

- Make sure you have installed:
  - [npm 8.6.0](https://nodejs.org/en/download/) or higher

- Open a new terminal at [market-client](Client\market-client)
- Type "npm-install" or "npm i" and click enter. This will install all the dependencies for the project
- Now type "npm start" and press enter.
 A browser window should open with the url: <http://localhost:3000/>
 (though it's possible that the port 3000 is already in use, in which case the terminal/CMD will ask you if it's ok to choose another port, but that doesn't really matter).
- That's it! the client is running and kicking.

**Notes:**

- To open a new client just enter the same root url in a new tab
(usually: "http://localhost:3000/").
- We recommend you run the server before running the client in order to avoid an unpleasant alerts.
- Another problem that could pop up is that you don't have react installed in your workspace, in this case you can install react simply with [npm install react@latest](https://www.npmjs.com/package/react)

## running the server tests

- In order to run the server tests, the server directory needs to be opened with Visual Studio, as explained earlier
- Search for the [TestMarketBackend](Server\MarketBackend\TestMarketBackend) directory in the Solution Explorer. Right-click the directory, and  choose "Run Tests".
- The Test Explorer window should open automatically and run the tests.
