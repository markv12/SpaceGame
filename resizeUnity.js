function resizeUnity(){
    var minW = 190;
    var w;

    if(document.all){
       w = document.body.offsetWidth;
    }

    else{
       w = window.innerWidth;
    }  
    w-=200;
             
    if(w < minW) 
    	w = minW;

    var unity = GetUnity();

    unity.style.width = w;

}

function GetUnity () {
  var unity = document.getElementById("UnityObject");
  if(unity == null) {
    unity = document.getElementById("UnityEmbed");
  }
  if(unity == null){
    unity = document.getElementById("unityPlayer");
  }
  return unity;
}