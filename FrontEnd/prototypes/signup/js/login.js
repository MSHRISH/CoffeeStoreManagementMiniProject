const Customer = JSON.parse(localStorage.getItem('CustomerData'));
if(Customer){
    window.location.href='../CustomerViews/CustomerDash.html';
}



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


loginForm=document.getElementById("login-form");

loginForm.addEventListener("submit",function(event){
    event.preventDefault();
    let formData = new FormData(event.target);

    let email=formData.get("email-input");
    let password=formData.get("password-input");

    let loginData={uemailphone:email,password:password}

    let options={
        method:'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body:JSON.stringify(loginData)
    }

    fetch("http://localhost:5122/api/UserAuthentication/Login",options)
    .then(response=>{
        if(!response.ok){
            return response.json().then(error => {
                throw new Error(JSON.stringify(error));
            });
        }
        return response.json();
    })
    .then(data=>{
        console.log(data);
        console.log(data.token);
        loginForm.reset();
        if(data.role==="Customer"){
            localStorage.setItem('CustomerData', JSON.stringify(data));
            window.location.href = '../CustomerViews/CustomerDash.html';
        }
        else{
            localStorage.setItem('EmployeeData', JSON.stringify(data));
            window.location.href = '../EmployeeViews/EmployeeDash.html';
        }
        
    })
    .catch(error=>{
        console.error("Login Error: ", error);
        const errorObject = JSON.parse(error.message);
        alert("Error Code: "+errorObject.errorCode+"\nError Message: "+errorObject.message);
    })

});