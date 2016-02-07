var AlchemyChatServer = {};

function connectServer(){
	AlchemyChatServer = new Alchemy({ 
	    Server: "localhost",
	    Port; 81,
	    Action: "chat",
	    DebugMode: true
	});

	AlchemyChatServer.Connected = function(){
	    console.log("connected");
	};

	AlchemyChatServer.Disconnected = function(){
	    console.log("disconnected");
	};

	AlchemyChatServer.MessageReceived = function(event){
	    console.log("Data: " + event.data);
	};

	AlchemyChatServer.Start();
}

function sendData(data){
	AlchemyChatServer.Send(data);
}
function stopConnection(){
	AlchemyChatServer.Stop();
}