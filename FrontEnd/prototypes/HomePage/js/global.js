var toggle=false;
$(document).ready(()=>{
    $("#menu-button").click(()=>{
        $("#drop-panel").slideToggle(500);
        if(toggle){
            toggle=false;
        }
        else{
            toggle=true;
        }
    });

    $(window).resize(function(){
        var screenWidth = $(window).width();

        //sm-breakpoint
        if (screenWidth >= 640) {
            $("#drop-panel").css('display','');
        }
    });
});