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


signupForm=document.getElementById("signup-form");
signupForm.addEventListener("submit",function(event){
    event.preventDefault();
    let formData = new FormData(event.target);
    let name=formData.get("name-input");
    let email=formData.get("email-input");
    let phone=formData.get("phone-input");
    let password=formData.get("password-input");

    let signupData={name:name,email:email,phone:phone,password:password};

    let options={
        method:'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body:JSON.stringify(signupData)
    }

    fetch("http://localhost:5122/api/UserAuthentication/RegisterCustomer",options)
    .then(response=>{
        if(!response.ok){
            return response.json().then(error => {
                throw new Error(JSON.stringify(error));
            });
        }
        return response.json();
    })
    .then(data=>{
        //console.log("Signup successfull");
        alert("Signup Successfull. Redirecting to login page.");
        signupForm.reset();
        window.location.href = 'login.html';
    })
    .catch(error=>{
        console.error("Fetch error:", error);
        const errorObject = JSON.parse(error.message);
        alert("Error Code: "+errorObject.errorCode+"\nError Message: "+errorObject.message);
    })
});