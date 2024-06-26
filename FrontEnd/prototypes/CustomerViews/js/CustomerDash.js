const Customer = JSON.parse(localStorage.getItem('CustomerData'));
console.log("Data in LocalStorage: "+Customer); 


//Check if token is present
if(!Customer){
    alert("Log in as Customer!");
    window.location.href="../signup/login.html"
}

console.log("Token in LocalStorage: "+Customer.token);
//Check if token is valid
const decodedToken=jwtRipOpen(Customer.token);
if(!validateCutomerToken(decodedToken)){
    alert("Invalid Token Login again");
    localStorage.removeItem('CustomerData');
    window.location.href="../signup/login.html"
}

//LogoutLogic and Page Highlight
document.addEventListener('DOMContentLoaded',()=>{

    //Current Page
    let currentPageId='profile-page';

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
        localStorage.removeItem('CustomerData');
        alert('Logged Out');
        window.location.href='../signup/login.html';
    });

    window.addEventListener('click', (event) => {
        if (event.target === logoutModal) {
            logoutModal.classList.add('hidden');
        }
    });

});
