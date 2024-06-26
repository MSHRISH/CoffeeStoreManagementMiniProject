function fetchProfile(){
    let options={
     method:'GET',
     headers:{
         'accept':'text/plain',
         'Authorization':'Bearer '+JSON.parse(localStorage.getItem('CustomerData')).token
     }
    }
    //console.log(options)
 
 
    fetch("http://localhost:5122/api/UserAuthentication/GetMyDetails",options)
    .then(response=>{
     if(!response.ok){
         return response.json().then(error=>{
             throw new Error(JSON.stringify(error));
         });
     }
     return response.json();
    })
    .then(data=>{
         console.log(data);
         const profileContainer=document.getElementById("profile-container");
 
         const profileName=document.getElementById('profile-name');
         profileName.innerText=data.name;
 
         const profileEmail=document.getElementById('profile-email');
         profileEmail.innerText=data.email
 
         const profilePhone=document.getElementById('profile-phone');
         profilePhone.innerText=data.phone;
 
         const profileUserId=document.getElementById('profile-userid');
         profileUserId.innerText=data.userId;
 
         const ProfileRole=document.getElementById('profile-role');
         ProfileRole.innerText=data.role;
    })
    .catch(error=>{
         console.error("Login Error: ", error);
         const errorObject = JSON.parse(error.message);
         alert("Error Code: "+errorObject.errorCode+"\nError Message: "+errorObject.message);
    });
 }
 
 fetchProfile();