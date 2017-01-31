const electron = require('electron');
//const {app, BrowserWindow} = electron;
const app = electron.app;
const BrowserWindow = electron.BrowserWindow;

var mainWindow = null;



app.on('window-all-closed', function() {

 if (process.platform != 'darwin')

 app.quit();

});



app.on('ready', function() { 

 mainWindow = new BrowserWindow({width: 800, height: 600}); 
 mainWindow.maximize();

 mainWindow.loadURL('file://' + __dirname + '/index.html'); 

 mainWindow.on('closed', function() {

 mainWindow = null;

 }); 

});