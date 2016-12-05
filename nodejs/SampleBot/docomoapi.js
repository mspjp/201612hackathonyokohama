var client = function(apiKey){
    this.apiKey = apiKey;

    this.morph = function(){
        console.log("execute morph analyze");
        console.log("your api key is "+this.apiKey);
    }
}

module.exports = client;