function removeToken(){
    localStorage.removeItem('EmployeeData');
}

window.addEventListener('beforeunload', removeToken);



const Employee = JSON.parse(localStorage.getItem('EmployeeData'));
//console.log("Token in LocalStorage: "+Employee);

//Check Token is present
if(!Employee){
    alert("Log in as Employee!");
    window.location.href="../signup/login.html"
}

console.log("Token in LocalStorage: "+Employee.token);
//Check if token is valid
const decodedToken=jwtRipOpen(Employee.token);
if(!validateEmployeeToken(decodedToken)){
    alert("Invalid Token Login again");
    window.location.href="../signup/login.html"
}

//LogoutLogic and Page Highlight
document.addEventListener('DOMContentLoaded',()=>{
    //Hide Endpoints
    const Employee = JSON.parse(localStorage.getItem('EmployeeData'));
    const decodedToken=jwtRipOpen(Employee.token);

    //Manager and Barista Access Denial
    if(decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]=="Manager" || decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]=="Barista"){
        document.getElementById("changestatus-page").classList.add('hidden');
        document.getElementById("manager-page").classList.add('hidden');
    }

    //Barista Access Denial
    if(decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]=="Barista"){
        document.getElementById("addemployee-page").classList.add('hidden');
        document.getElementById("customer-page").classList.add('hidden');
        document.getElementById("barista-page").classList.add('hidden');
        document.getElementById("additem-page").classList.add('hidden');
    }




    //Sidebar 
    document.getElementById("sidebar-icon").addEventListener('click',()=>{
        document.getElementById('sidebar').classList.remove('hidden');
        document.getElementById('sidebar').classList.add('absolute');
    });

    document.getElementById('close-sidebar').addEventListener('click',()=>{
        document.getElementById('sidebar').classList.add('hidden');
        document.getElementById('sidebar').classList.remove('absolute');
    });

    //Current Page
    let currentPageId='orders-page';

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

    //AddEmployee page
    document.getElementById('addemployee-page').addEventListener('click',()=>{
        document.getElementById(currentPageId).classList.remove('bg-slate-600', 'bg-opacity-75');
        document.getElementById('addemployee-page').classList.add('bg-slate-600', 'bg-opacity-75');
        currentPageId='addemployee-page';
    });

    //ChangeStatus Page
    document.getElementById("changestatus-page").addEventListener('click',()=>{
        document.getElementById(currentPageId).classList.remove('bg-slate-600', 'bg-opacity-75');
        document.getElementById('changestatus-page').classList.add('bg-slate-600', 'bg-opacity-75');
        currentPageId='changestatus-page';
    });

    //Customer Page
    document.getElementById("customer-page").addEventListener('click',()=>{
        document.getElementById(currentPageId).classList.remove('bg-slate-600', 'bg-opacity-75');
        document.getElementById('customer-page').classList.add('bg-slate-600', 'bg-opacity-75');
        currentPageId='customer-page';
    });

     //Manager Page
     document.getElementById("manager-page").addEventListener('click',()=>{
        document.getElementById(currentPageId).classList.remove('bg-slate-600', 'bg-opacity-75');
        document.getElementById('manager-page').classList.add('bg-slate-600', 'bg-opacity-75');
        currentPageId='manager-page';
    });

    //Barista Page
    document.getElementById("barista-page").addEventListener('click',()=>{
        document.getElementById(currentPageId).classList.remove('bg-slate-600', 'bg-opacity-75');
        document.getElementById('barista-page').classList.add('bg-slate-600', 'bg-opacity-75');
        currentPageId='barista-page';
    });

    //Barista Page
    document.getElementById("additem-page").addEventListener('click',()=>{
        document.getElementById(currentPageId).classList.remove('bg-slate-600', 'bg-opacity-75');
        document.getElementById('additem-page').classList.add('bg-slate-600', 'bg-opacity-75');
        currentPageId='additem-page';
    });
     document.getElementById("additem-page").addEventListener('click',()=>{
        document.getElementById(currentPageId).classList.remove('bg-slate-600', 'bg-opacity-75');
        document.getElementById('additem-page').classList.add('bg-slate-600', 'bg-opacity-75');
        currentPageId='additem-page';
    });

    //Orders Page
    document.getElementById("orders-page").addEventListener('click',()=>{
        document.getElementById(currentPageId).classList.remove('bg-slate-600', 'bg-opacity-75');
        document.getElementById('orders-page').classList.add('bg-slate-600', 'bg-opacity-75');
        currentPageId='orders-page';
    });



    //Logout button
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



