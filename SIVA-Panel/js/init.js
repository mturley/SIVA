(function($){
  $(function(){

    $('.sidenav').sidenav();
	$(".dropdown-trigger").dropdown();
	$(".modal").modal();
    $('.tooltipped').tooltip();
	  M.Tooltip.getInstance(document.getElementById("guildselect")).open();
        
	  console.error("DO NOT PASTE ANYTHING IN HERE!");
	  console.info("Please, for your own sake, do not paste any code into the SIVA administration panel. Chances are, you'll mess up your server configuration, or, if you obtained the code online, it can give a hacker access to your Discord account. ");
	
  }); // end of document ready
})(jQuery); // end of jQuery name space