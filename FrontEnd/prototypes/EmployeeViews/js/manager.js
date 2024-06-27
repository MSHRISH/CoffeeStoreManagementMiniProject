const itemsPerPage = 5;
let currentPage = 1;
let customers=[];

fetchCustomers()

async function fetchCustomers(){
    try{
        let options={
            headers:{
                'accept':'text/plain',
                'Authorization':'Bearer '+JSON.parse(localStorage.getItem("EmployeeData")).token
            }
        }
        console.log(options);
        const response=await fetch('http://localhost:5122/api/UserAuthentication/GetAllManagers',options);
        const data=await response.json();
        customers=data;
        customers=sortCustomersByName(customers)
        displayCustomerbyPage(currentPage,customers);
        pagination(customers.length,customers);
    }catch(error){
        console.error('Error',error);
    }
}

function sortCustomersByName(customers) {
    return customers.sort((a, b) =>  a.name.localeCompare(b.name));
}

function displayCustomerbyPage(page,customers){
    const start = (page - 1) * itemsPerPage;
    const end = start + itemsPerPage;
    const paginatedCustomers = customers.slice(start, end);

    displayCustomers(paginatedCustomers);
}

function displayCustomers(paginatedCustomers){
    const customerContainer=document.getElementById("customer-container");
    customerContainer.innerHTML='';

    paginatedCustomers.forEach(customer => {
        const customerBlock=document.createElement('div');
        customerBlock.className="bg-slate-600 rounded-md   px-2 py-1 flex flex-col  justify-center items-center hover:bg-opacity-40";

        const customerName=document.createElement('span');
        customerName.className="font-bold text-xl";
        customerName.innerText="Name: "+customer.name;

        const customerEmail=document.createElement('span');
        customerEmail.className="font-bold text-xl"
        customerEmail.innerText="Email: "+customer.email;

        const customerPhone=document.createElement('span');
        customerPhone.className="font-bold text-xl";
        customerPhone.innerText="Phone: "+customer.phone;

        const customerId=document.createElement('span');
        customerId.className="font-bold text-xl";
        customerId.innerText="Id: "+customer.userId;
        
        const customerStatus=document.createElement('span');
        customerStatus.className="font-bold text-xl";
        customerStatus.innerText="Status: "+customer.status;
        
        customerBlock.appendChild(customerName);
        customerBlock.appendChild(customerEmail);
        customerBlock.appendChild(customerPhone);
        customerBlock.appendChild(customerId);
        customerBlock.appendChild(customerStatus);

        customerContainer.appendChild(customerBlock);
    });
}

function pagination(totalItems,Customers){
    const paginationBlock=document.getElementById("pagination-block");
    paginationBlock.innerHTML='';
    const totalPages = Math.ceil(totalItems / itemsPerPage);

    for(let i=1;i<=totalPages;i++){
        const pageItem=document.createElement("li");
        const pageLink=document.createElement("a");
        pageLink.className="flex items-center justify-center px-3 h-8 leading-tight text-gray-500 bg-white border border-gray-300 hover:bg-gray-100 hover:text-gray-700  dark:bg-gray-800 dark:border-gray-700 dark:text-gray-400 hover:bg-gray-700 dark:hover:text-white"
        if(i==1){
            pageLink.classList.add("underline", "decoration-2", "decoration-sky-500");
        }
        pageLink.href="#";
        pageLink.id="p"+i;
        pageLink.innerText=i;

        pageLink.addEventListener('click',()=>{
            const currentPageElement=document.getElementById("p"+currentPage);
            currentPageElement.classList.remove("underline", "decoration-2", "decoration-sky-500");
            pageLink.classList.add("underline", "decoration-2", "decoration-sky-500");
            currentPage=i;
            displayCustomerbyPage(currentPage,Customers);
        });
        pageItem.appendChild(pageLink);
        paginationBlock.appendChild(pageItem);
    }
}

function searchMenu(){
    const searchBar=document.getElementById('search-bar').value;
    if(searchBar===''){
        currentPage=1;
        displayCustomerbyPage(currentPage,customers);
        pagination(customers.length,customers);
        return;
    }
    let searchItems=[];

    customers.forEach(item => {
        if(item.name.toLowerCase().includes(searchBar.toLowerCase())){
            searchItems.push(item);
        }
    });

    if(searchItems.length===0){
        alert("No items found");
        return;
    }
    currentPage=1
    displayCustomerbyPage(currentPage,searchItems);
    pagination(searchItems.length,searchItems);
}
