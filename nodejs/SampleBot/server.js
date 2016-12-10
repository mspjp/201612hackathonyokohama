var restify = require('restify');
var builder = require('botbuilder');
var fs = require('fs');
var cognitive = require('./cognitive');
require('date-utils');


function onMessage(session){
    var responses = [];
    for(var rule of rules){
        var message = session.message.text;
        if((new RegExp(rule.pattern).exec(message))){
            if(rule.responses.length == 1){
                responses.push(rule.responses[0]);
            }else{
                var pick = Math.floor( Math.random() * rule.responses.length );
                responses.push(rule.responses[pick]);
            }
            break;
        }
    }

    if(session.message.attachments.length > 0){
        var imageUrl = session.message.attachments[0].contentUrl;
        cognitive.faceDetect(API_KEY.FACE_APIKEY,image,true,true,"smile",function(error,response,body){
            if (!error && response.statusCode == 200) {
                console.log(body);
            } else {
                console.log('error: '+ JSON.stringify(response));
            }
        });
    }

    for(var res of responses){
        if(res.indexOf('{')!=-1&&res.indexOf('}')!=-1){
            var command = res.replace('{','').replace('}','');
            onCommand(session,command);
        }else{
            session.send(res);
        }
    }

    if(responses.length == 0){
        session.send("ルールにヒットしませんでした");
    }
}

function onCommand(session,command){
    if(command === "command1"){
        session.send("execute "+command);
    }
}

var API_KEY = JSON.parse(fs.readFileSync('./apikey.json','utf8'));
var rules = [];
fs.readFileSync('./rule.csv').toString().split('\n').forEach(function (line) {
	if(line.startsWith("//")||line==""){
        return;
    }
    var cols = line.split(',');
    var subCols = cols[1].split(':');
    rules.push({
        pattern:cols[0],
        responses:subCols
    });
});
rules.splice(0,1);

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
        session.beginDialog('/message');
    }catch(ex){
        writeLog(ex);
    }
});

bot.dialog('/message',function(session){
    onMessage(session);
    session.endDialog();
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