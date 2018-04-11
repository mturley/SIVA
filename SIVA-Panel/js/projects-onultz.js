function updateWelcomeColour(){
	M.toast({html: 'Changing welcome colour'});
	var url = "welcomecolour.action";
	var method = "POST";
	var postData = document.getElementById("guildId").value + "," + document.getElementById("welcomecolor").value;
	var shouldBeAsync = true;

	var request = new XMLHttpRequest();
	request.onload = function () {
		var status = request.status; 
		var data = request.responseText; 
		M.toast({html: ''+request.statusText});
		if (status == 200){
			location.reload();
		}
	}

	request.open(method, url, shouldBeAsync);

	request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
	request.setRequestHeader("X-Siva-Token", document.getElementById("auth").value);
	request.send(postData);
	
}
function updateAllColours(){
	
}
function updateLevels(){
		M.toast({html: 'Updating levels...'});
	var url = "levels.action";
	var method = "POST";
	var postData = document.getElementById("guildId").value;
	var shouldBeAsync = true;

	var request = new XMLHttpRequest();
	request.onload = function () {
		var status = request.status; 
		var data = request.responseText; 
		M.toast({html: ''+request.statusText});
		if (status == 200){
			location.reload();
		}
	}

	request.open(method, url, shouldBeAsync);

	request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
	request.setRequestHeader("X-Siva-Token", document.getElementById("auth").value);
	request.send(postData);
}
function updateTod(){
		M.toast({html: 'Updating truth or dare...'});
	var url = "tod.action";
	var method = "POST";
	var postData = document.getElementById("guildId").value;
	var shouldBeAsync = true;

	var request = new XMLHttpRequest();
	request.onload = function () {
		var status = request.status; 
		var data = request.responseText; 
		M.toast({html: ''+request.statusText});
		if (status == 200){
			location.reload();
		}
	}

	request.open(method, url, shouldBeAsync);

	request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
	request.setRequestHeader("X-Siva-Token", document.getElementById("auth").value);
	request.send(postData);
}
function updateAntilink(){
		M.toast({html: 'Updating levels...'});
	var url = "antilink.action";
	var method = "POST";
	var postData = document.getElementById("guildId").value;
	var shouldBeAsync = true;

	var request = new XMLHttpRequest();
	request.onload = function () {
		var status = request.status; 
		var data = request.responseText; 
		M.toast({html: ''+request.statusText});
		if (status == 200){
			location.reload();
		}
	}

	request.open(method, url, shouldBeAsync);

	request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
	request.setRequestHeader("X-Siva-Token", document.getElementById("auth").value);
	request.send(postData);
}
function updateMessages(){
		M.toast({html: 'Updating levels...'});
	var url = "tod.action";
	var method = "POST";
	var postData = document.getElementById("guildId").value;
	var shouldBeAsync = true;

	var request = new XMLHttpRequest();
	request.onload = function () {
		var status = request.status; 
		var data = request.responseText; 
		M.toast({html: ''+request.statusText});
		if (status == 200){
			location.reload();
		}
	}

	request.open(method, url, shouldBeAsync);

	request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
	request.setRequestHeader("X-Siva-Token", document.getElementById("auth").value);
	request.send(postData);
}