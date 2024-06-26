// Array containing the two texts
const texts = [
    "Kappi Kadai",
    "காப்பி கடை"
  ];
  
  // Index to keep track of current text
  let currentIndex = 0;
  
  let intervalId = setInterval(updateText, 5000);
  // Function to update the text
  function updateText() {
    try{
        document.getElementById("store-name").innerText = texts[currentIndex];
        currentIndex = (currentIndex + 1) % texts.length; // Toggle between 0 and 1
    }
    catch{
        clearInterval(intervalId);
    }
}
  
  // Update text initially
  updateText();
  
  // Set interval to update text every 5 seconds
  



  
//JWT Token Rip-Open Test
function jwtRipOpen(token){
    const parts = token.split('.');

    if (parts.length !== 3) {
        alert('Invalid JWT.');
        return;
    }

    const base64Url = parts[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    const jsonPayload = decodeURIComponent(atob(base64).split('').map(c => {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    const payload = JSON.parse(jsonPayload);
    // console.log(payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]);
    // console.log(payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]);
    // console.log(payload["exp"]);
    // console.log(JSON.stringify(payload,null,2))
    return payload;
}

//Validating a Customer Token
function validateCutomerToken(decodedToken){
    if(decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]!='Customer'){
        return false;
    }
    const currentTime = Math.floor(Date.now() / 1000);

    if (decodedToken['exp'] && currentTime < decodedToken['exp']) {
        return true;
    } else {
        return false;
    }
}


//Validating a Employee Token
function validateEmployeeToken(decodedToken){
    if(decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]=='Customer'){
        return false;
    }
    const currentTime = Math.floor(Date.now() / 1000);

    if (decodedToken['exp'] && currentTime < decodedToken['exp']) {
        return true;
    } else {
        return false;
    }
}
  
