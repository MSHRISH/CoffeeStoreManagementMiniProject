const idInput=document.getElementById('id-input');
const idError=document.getElementById('id-error');

idInput.addEventListener('input',function(event){
    if(!idInput.validity.valid){
        idError.classList.remove('hidden');
    }
    else{
        idError.classList.add('hidden');
    }
});

const changeStatusForm=document.getElementById('changestatus-form')
changeStatusForm.addEventListener('submit',function(event){
    event.preventDefault();
    let formData = new FormData(event.target);

    let itemid=formData.get('id-input');

    let query="itemid="+itemid;
    let apiURL="http://localhost:5122/api/ItemServices/ChangeAvailabilityofItem?"+query;

    let options={
        method:'POST',
        headers:{
            'accept':'text/plain',
            'Authorization':'Bearer '+JSON.parse(localStorage.getItem("EmployeeData")).token
        }
    }
    fetch(apiURL,options)
    .then(response=>{
        
        if(!response.ok){
            return response.json().then(error => {
                throw new Error(JSON.stringify(error));
            });
        }
        return response.json();
    })
    .then(data=>{
        changeStatusForm.reset();
        if(data.isAvailable){
            alert("ItemId "+data.itemId+" changed to Available");
        }
        else{
            alert("ItemId "+data.itemId+" changed to Not Available");
        }
        
    })
    .catch(error=>{
        console.error("Login Error: ", error);
        const errorObject = JSON.parse(error.message);
        alert("Error Code: "+errorObject.errorCode+"\nError Message: "+errorObject.message);
    })
});