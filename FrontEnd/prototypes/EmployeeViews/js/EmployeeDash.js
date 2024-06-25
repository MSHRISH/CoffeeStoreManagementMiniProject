function removeToken(){
    localStorage.removeItem('EmployeeData');
}
//window.addEventListener('beforeunload', removeToken);

const Employee = JSON.parse(localStorage.getItem('EmployeeData'));
console.log("Token in LocalStorage: "+Employee);

//Check Token is present
if(!Employee){
    alert("Log in as Employee!");
    window.location.href="../signup/login.html"
}

console.log("Token in LocalStorage: "+Employee.token);
//Check if token is valid
const decodedToken=jwtRipOpen(Employee.token);
if(!validateToken(decodedToken)){
    alert("Invalid Token Login again");
    window.location.href="../signup/login.html"
}

//LogoutLogic and Page Highlight
document.addEventListener('DOMContentLoaded',()=>{

    
    //Current Page
    let currentPageId='menu-page';

    //Menu page
    document.getElementById('menu-page').addEventListener('click',()=>{
        document.getElementById(currentPageId).classList.remove('bg-slate-600', 'bg-opacity-75');
        document.getElementById('menu-page').classList.add('bg-slate-600', 'bg-opacity-75');
        currentPageId='menu-page';
    });

    //Profile Page
    document.getElementById('profile-page').addEventListener('click',()=>{
        document.getElementById(currentPageId).classList.remove('bg-slate-600', 'bg-opacity-75');
        document.getElementById('profile-page').classList.add('bg-slate-600', 'bg-opacity-75');
        currentPageId='profile-page';
    });

    const logoutBtn = document.getElementById('logout-button');
    const logoutModal = document.getElementById('logout-modal');
    const cancelBtn = document.getElementById('cancelBtn');
    const confirmLogoutBtn = document.getElementById('confirmLogoutBtn');

    logoutBtn.addEventListener('click',()=>{
        logoutModal.classList.remove('hidden');
        
    });

    cancelBtn.addEventListener('click',()=>{
        logoutModal.classList.add('hidden');
    });

    confirmLogoutBtn.addEventListener('click',()=>{
        localStorage.removeItem('EmployeeData');
        alert('Logged Out');
        window.location.href='../signup/login.html';
    });

    window.addEventListener('click', (event) => {
        if (event.target === logoutModal) {
            logoutModal.classList.add('hidden');
        }
    });

});


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
    console.log(payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]);
    console.log(payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]);
    console.log(payload["exp"]);
    console.log(JSON.stringify(payload,null,2))
    return payload;
}


function validateToken(decodedToken){
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