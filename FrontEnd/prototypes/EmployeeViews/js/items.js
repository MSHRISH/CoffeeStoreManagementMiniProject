const itemsPerPage = 5;
let currentPage = 1;
let menu = [];
let itemTypes=[]
const itemTypeMap = new Map();
var cart={}



fetchItemTypes()

async function fetchMenu(){
    try{
        const response = await fetch('http://localhost:5122/api/ItemServices/GetAllItems');
        const data = await response.json();
        menu=data;
        menu=sortItems(menu);
        displayMenubyPage(currentPage,menu);
        pagination(menu.length,menu);
    }catch(error){
        console.error('Error fetching the quotes:', error);
    }
}

async function fetchItemTypes(){
    try{
        const response = await fetch('http://localhost:5122/api/ItemServices/GetAllItemTypes');
        const data = await response.json();
        itemTypes=data;
        //console.log(itemTypes)
        itemTypes.forEach(type => {
            itemTypeMap.set(type.typeName, type.id);
        });
        fetchMenu();
    }catch(error){
        console.error('Error fetching the quotes:', error);
    }
}

function displayMenubyPage(page, menu){
    const start = (page - 1) * itemsPerPage;
    const end = start + itemsPerPage;
    const paginatedMenu = menu.slice(start, end);

    displayMenu(paginatedMenu);
}


function displayMenu(paginatedMenu){
    const menuContainer=document.getElementById("menu-container");
    menuContainer.innerHTML='';
    paginatedMenu.forEach(item => {
        const itemBlock=document.createElement('div');
        itemBlock.className="bg-slate-600 rounded-md   px-2 py-1 flex flex-col  justify-center items-center hover:bg-opacity-40";

        const itemId=document.createElement('span');
        itemId.className="font-bold text-xl";
        itemId.innerText="ID:"+item.itemId;

        const itemName=document.createElement('span');
        itemName.className="font-bold text-xl";
        itemName.innerText=item.itemName;

        const itemAvailable=document.createElement('span');
        itemAvailable.className="font-bold text-xl";
        if(item.isAvailable){
            itemAvailable.innerText="Available";    
        }
        else{
            itemAvailable.innerText="Not Available";
        }

        const itemDescription=document.createElement('span');
        itemDescription.className="italic";
        itemDescription.innerText=item.itemDescription;

        const itemType=document.createElement('span');
        itemType.innerText=item.itemType;
        
        const itemPrice=document.createElement('span');
        itemPrice.innerText=item.price+" INR";

        
        
        itemBlock.appendChild(itemId);
        itemBlock.appendChild(itemName);
        itemBlock.appendChild(itemAvailable);
        itemBlock.appendChild(itemDescription);
        itemBlock.appendChild(itemType);
        itemBlock.appendChild(itemPrice);
        
        
       

        menuContainer.appendChild(itemBlock);
    });
}

function pagination(totalItems,Menu){
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
            displayMenubyPage(currentPage,Menu);
        });
        pageItem.appendChild(pageLink);
        paginationBlock.appendChild(pageItem);
    }
}

function searchMenu(){
    const searchBar=document.getElementById('search-bar').value;
    if(searchBar===''){
        currentPage=1;
        displayMenubyPage(currentPage,menu,1);
        pagination(menu.length,menu);
        return;
    }
    let searchItems=[];

    menu.forEach(item => {
        if(item.itemName.toLowerCase().includes(searchBar.toLowerCase())||item.itemType.toLowerCase().includes(searchBar.toLowerCase())){
            searchItems.push(item);
        }
    });

    if(searchItems.length===0){
        alert("No items found");
        return;
    }
    currentPage=1
    displayMenubyPage(currentPage,searchItems,1);
    pagination(searchItems.length,searchItems);
}

function sortItems(items){
    return items.sort((a, b) => {
        const typeIdA = itemTypeMap.get(a.itemType);
        const typeIdB = itemTypeMap.get(b.itemType);

        if (typeIdA === typeIdB) {
            return a.itemName.localeCompare(b.itemName);
        } else {
            return typeIdA - typeIdB;
        }
    });
}



