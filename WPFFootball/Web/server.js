var express = require('express');
var app = express();

app.use('/Folder', express.static(__dirname + '/Folder'));