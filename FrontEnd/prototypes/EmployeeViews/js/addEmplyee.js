const nameInput=document.getElementById('name-input');
    const nameError=document.getElementById('name-error'); 

    nameInput.addEventListener('input',function(event){
        if(!nameInput.validity.valid){
            nameError.classList.remove('hidden');
        }
        else{
            nameError.classList.add('hidden');
        }
    });


    const emailInput=document.getElementById('email-input');
    const emailError=document.getElementById('email-error');

    emailInput.addEventListener('input',function(event){
        if(!emailInput.validity.valid){
            emailError.classList.remove('hidden');
        }
        else{
            emailError.classList.add('hidden');
        }
    });

    const phoneInput = document.getElementById('phone-input');
    const phoneError=document.getElementById('phone-error')
     
    phoneInput.addEventListener('input',function(event){
        if(!phoneInput.validity.valid){
            phoneError.classList.remove('hidden');
        }
        else{
            phoneError.classList.add('hidden');
        }
    });

    const passwordInput=document.getElementById('password-input');
    const passwordError=document.getElementById('password-error');

    passwordInput.addEventListener('input',function(event){
        if(!passwordInput.validity.valid){
            passwordError.classList.remove('hidden');
        }
        else{
            passwordError.classList.add('hidden');
        }
    });

    const roleInput=document.getElementById('role-input');
    const roleError=document.getElementById('role-error');

    roleInput.addEventListener('click',function(event){
        if(roleInput.value===""){
            roleError.classList.remove('hidden');
        }
        else{
            roleError.classList.add('hidden');
        }
    });

//localstorage data
let employeeData=JSON.parse(localStorage.getItem("EmployeeData"));

let employeeRoles=["Barista","Manager"];


let decodedToken=jwtRipOpen(employeeData.token);
if(decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']=='Manager'){
    employeeRoles.pop();
}

employeeRoles.forEach(role => {
    const roleSelect=document.getElementById('role-input');

    const optionElement=document.createElement('option');
    optionElement.value=role;
    optionElement.innerText=role;

    roleSelect.appendChild(optionElement);
});

//Submit form
addemployeeForm=document.getElementById("addemployee-form");
addemployeeForm.addEventListener("submit",function(event){
    event.preventDefault();
 
    let formData = new FormData(event.target);
    let role=formData.get("role-input");
    let name=formData.get("name-input");
    let email=formData.get("email-input");
    let phone=formData.get("phone-input");
    let password=formData.get("password-input");

    let addEmployeeData={role:role,name:name,email:email,phone:phone,password:password};
    //console.log(signupData);

    let options={
        method:'POST',
        headers: {
            'accept':' text/plain',
            'Authorization':'Bearer '+employeeData.token,
            'Content-Type': 'application/json',
        },
        body:JSON.stringify(addEmployeeData)
    }

    //console.log(options);

    let apiURL="http://localhost:5122/api/UserAuthentication/RegisterBarista";
    if(role==='Manager'){
        apiURL="http://localhost:5122/api/UserAuthentication/RegisterManager";
    }

    fetch(apiURL,options)
    .then(response=>{
        if(!response.ok){
            return response.json().then(error=>{
                throw new Error(JSON.stringify(error));
            });
        }
    }).then(data=>{
        alert("New Employee Created!!");
        addemployeeForm.reset();
    })
});