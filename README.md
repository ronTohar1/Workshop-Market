# Workshop-Market
In this project the server is implemented in C# whereas the client is implemented in type-script using react.   

## running the server

- Make sure you have installed:
    - Visual Studio  
    - .Net Frameowrk 6.0 
- [a relative link] (README.md)
- Open "Workshop-Market\Server\MarketBackend\MarketBackend.sln" with Visual Studio as an existing project. 
- now that you're inside the solution, look at the upper tool bar and search for the 'run' button, make sure that the button is connected to the WebAPI Directory (in most versions the name of the directory you'll run is displayed next to 'run' button). If the button isn't connected to the WebAPI directory you can simply connect it by right-clicking the WebAPI directory (In the solution explorer), and choose "Set As Startup Project".
- after running the project, a browser page named "Swagger" should open automatically. you can ignore that, The WebAPI interface allows you to send requests to the server from that browser. 
- That's it! the server is running and kicking.  

## running the client 
it will be more comefortable to run the client from Visual Studio Code, but it's not mandatory. 
- in order to run the client firstly you'll want to navigate to the path directory: "Workshop-Market\Client\market-client" which hold the client's source code. Then open the CMD, or if your'e using Visual Studio Code open a terminal and try to run "npm start" (if the command fails see the next bullet). 
- if the command fails it's probably because you don't have npm installed in your workspace, so now try to type in the CMD/terminal "npm i", then try previous bullet once again.
- if you managed to run "npm run", a browser window should open usually with url: "http://localhost:3000/" (though it's possible that the port 3000 is already in use, in which case the terminal/CMD will ask you if it's ok to choose another port, but that's doesn't really matter). the opened window is that of the client side. 
- That's it! the client is running and kicking. 

**Notes:**
- If the terminal/CMD is running and you would like to open a second access to the client, you don't need to run another terminal/CMD. You can simply open a new tab at your browser, and enter the same root url that you recieved in the first time you ran the client 
(usually: "http://localhost:3000/"). 
- We recommend to run the server before running the client in order to avoid an unpleasant alerts.
- Another problem that could pop up is that you don't have react installed in your workspace, you can install react simply with "npm install react@latest"

 ### running the server tests 
- in order to run the server tests, the server directory needs to be opened with Visual Studio (not visual Studio Code). 
- after opening the directory make sure you enter the 'MarketBackend.sln' solution (you can simply enter to the solution by double-clicking 
the file).
-  Search for the "TestMarketBackend" directory in the Solution Explorer. right-click the directory, and  choose "Run Tests".
- The Test Explorer window should open automatically and run the tests. 