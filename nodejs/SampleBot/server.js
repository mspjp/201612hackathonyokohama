var restify = require('restify');
var builder = require('botbuilder');
var fs = require('fs');
var cognitive = require('./cognitive');
require('date-utils');


function onMessage(session){
    if(session.message.attachments.length===0){
            session.send("Hello World "+session.message.text);
    }else{
        var url = session.message.attachments[0].contentUrl;
        cognitive.faceDetect(
            API_KEY.FACE_APIKEY,
            url,
            true,
            true,
            "smile,age",
            function(err,res,body){
                var age = body[0].faceAttributes.age;
                session.send(Math.floor(age)+"歳やろ？");
            }
        );
        
    }
}

var API_KEY = JSON.parse(fs.readFileSync('./apikey.json','utf8'));

fs.writeFileSync('index.html','<h1>This is bot page</h1><br /><br /><h2>errors</h2><br /><br />','utf8');
function writeLog(log){
    var dt = new Date();
    var dateStr = dt.toFormat('MM/DD HH24:MI:SS');
    var logStr = "["+dateStr+"] "+log; 
    console.log(logStr);
    fs.appendFileSync('index.html',logStr,'utf8');
}

var connector = new builder.ChatConnector({
    appId: API_KEY.BOT_APIKEY,
    appPassword: API_KEY.BOT_APISECRET
});
var bot = new builder.UniversalBot(connector);
bot.dialog('/', function (session) {
    try{
        onMessage(session);
    }catch(ex){
        writeLog(ex);
    }
});

var server = restify.createServer();
server.post('/api/messages', connector.listen());

server.get(/.*/, restify.serveStatic({
	'directory': '.',
	'default': 'index.html'
}));

server.listen(process.env.PORT || 3978, function () {
   console.log('%s listening to %s', server.name, server.url); 
});