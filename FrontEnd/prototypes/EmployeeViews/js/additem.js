const itemTypeInput=document.getElementById('itemtype-input');
const itemTypeError=document.getElementById('itemtype-error');

itemTypeInput.addEventListener('click',function(event){
    if(itemTypeInput.value===""){
        itemTypeError.classList.remove('hidden');
    }
    else{
        itemTypeError.classList.add('hidden');
    }
});

fetchItemTypes();

async function fetchItemTypes(){
    try{
        const response = await fetch('http://localhost:5122/api/ItemServices/GetAllItemTypes');
        const data = await response.json();

        data.forEach(itemtype => {
            const optionElement=document.createElement('option'); 
            optionElement.value=itemtype.typeName;
            optionElement.innerText=itemtype.typeName;   
            
            itemTypeInput.appendChild(optionElement)
        });

    }catch(error){
        console.error('Error fetching the quotes:', error);
    }
}




const itemNameInput=document.getElementById('itemname-input');
const itemNameError=document.getElementById('itemname-error'); 

itemNameInput.addEventListener('input',function(event){
    if(!itemNameInput.validity.valid){
        itemNameError.classList.remove('hidden');
    }
    else{
        itemNameError.classList.add('hidden');
    }
});

const itemDescriptionInput=document.getElementById('itemdescription-input');
const itemDescriptionError=document.getElementById('itemdescription-error');

itemDescriptionInput.addEventListener('input',function(event){
    if(!itemDescriptionInput.validity.valid){
        itemDescriptionError.classList.remove('hidden');
    }
    else{
        itemDescriptionError.classList.add('hidden');
    }
});

const itemPriceInput=document.getElementById('itemprice-input');
const itemPriceError=document.getElementById('itemprice-error');

itemPriceInput.addEventListener('input',function(event){
    if(!itemPriceInput.validity.valid){
        itemPriceError.classList.remove('hidden');
    }
    else{
        itemPriceError.classList.add('hidden');
    }
});

//localstorage data
let employeeData=JSON.parse(localStorage.getItem("EmployeeData"));

additemForm=document.getElementById("additem-form");
additemForm.addEventListener("submit",function(event){
    event.preventDefault();

    let formData = new FormData(event.target);
    let itemType=formData.get("itemtype-input");
    let itemName=formData.get('itemname-input');
    let itemDescription=formData.get('itemdescription-input');
    let itemPrice=formData.get('itemprice-input');

    let addItemData={ isAvailable: true,itemType:itemType,itemName:itemName,itemDescription:itemDescription,price:parseInt(itemPrice)}

    // console.log(addItemData);
    let options={
        method:'POST',
        headers: {
            'accept':' text/plain',
            'Authorization':'Bearer '+employeeData.token,
            'Content-Type': 'application/json',
        },
        body:JSON.stringify(addItemData)
    }
    
    let apiURL="http://localhost:5122/api/ItemServices/AddAnItem";
    
    fetch(apiURL,options)
    .then(response=>{
        if(!response.ok){
            return response.json().then(error=>{
                throw new Error(JSON.stringify(error));
            });
        }
    }).then(data=>{
        alert("New Item Created!!");
        additemForm.reset();
    })
    .catch(error=>{
        console.error("Login Error: ", error);
        const errorObject = JSON.parse(error.message);
        alert("Error Code: "+errorObject.errorCode+"\nError Message: "+errorObject.message);
    })

});