function updateWelcomeColour(){
	M.toast({html: 'Changing welcome colour'});
	var url = "login.html";
	var method = "POST";
	var postData = "Some data";
	var shouldBeAsync = true;

	var request = new XMLHttpRequest();
	request.onload = function () {
		var status = request.status; 
		var data = request.responseText; 
		M.toast({html: ''+request.statusText});
	}

	request.open(method, url, shouldBeAsync);

	request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
	request.send(postData);
	
}
function updateAllColours(){
	
}