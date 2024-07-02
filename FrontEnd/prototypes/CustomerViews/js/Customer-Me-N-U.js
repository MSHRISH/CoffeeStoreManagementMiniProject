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
        displayMenubyPage(currentPage,menu,0);
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

function displayMenubyPage(page, menu,flag){
    const start = (page - 1) * itemsPerPage;
    const end = start + itemsPerPage;
    const paginatedMenu = menu.slice(start, end);

    displayMenu(paginatedMenu,flag);
}

//flag-0 initial load flag-1 paginated loads
function displayMenu(paginatedMenu,flag){
    const menuContainer=document.getElementById("menu-container");
    menuContainer.innerHTML='';
    paginatedMenu.forEach(item => {
        const itemBlock=document.createElement('div');
        itemBlock.className="bg-slate-600 rounded-md   px-2 py-1 flex flex-col  justify-center items-center hover:bg-opacity-40";

        const itemName=document.createElement('span');
        itemName.className="font-bold text-xl";
        itemName.innerText=item.itemName;

        const itemDescription=document.createElement('span');
        itemDescription.className="italic";
        itemDescription.innerText=item.itemDescription;

        const itemType=document.createElement('span');
        itemType.innerText=item.itemType;
        
        const itemPrice=document.createElement('span');
        itemPrice.innerText=item.price+" INR";

        const buttonsDiv=document.createElement('div');
        buttonsDiv.className="flex flex-cols gap-2";

        const addButton=document.createElement('button')
        addButton.innerText="Add";
        
        addButton.className="bg-green-200 rounded-lg px-2 py-1"
        addButton.id=item.itemId;
        addButton.name="add-button-"+item.itemId

        const plusButton=document.createElement('button')
        plusButton.innerText="+";
        plusButton.className="bg-green-500 hidden py-1 px-2 rounded-lg";
        plusButton.id=item.itemId;

        const minusButton=document.createElement("button")
        minusButton.innerText="-";
        minusButton.className="bg-red-500 hidden py-1 px-2 rounded-lg"
        minusButton.id=item.itemId;

        if(flag===1){
            if(item.itemId in cart){
                addButton.innerText=cart[item.itemId];
                addButton.disabled=true;
                plusButton.classList.remove("hidden");
                minusButton.classList.remove("hidden");
            }   
        }
        
        //Cart Items Modal
        const cartItemsModal=document.getElementById("cart-items-modal");
        
        //CartItem
        const cartItem=document.createElement("div");
        //CartItemName
        const cartItemName=document.createElement("span");
        //CartItemMinusButton
        const cartItemMinusButton=document.createElement("button");
        //CartItemQty
        const cartItemQuantity=document.createElement("span");
        //CartAddButton
        const cartItemAddButton=document.createElement("button");

        addButton.addEventListener('click',function(event){
            // console.log(addButton.id)
            cart[addButton.id]=1
            addButton.disabled=true;
            addButton.innerText=cart[addButton.id];
            minusButton.classList.remove("hidden");
            plusButton.classList.remove("hidden");

            
            //CartItem
            cartItem.id="cart-item-"+addButton.id;
            cartItem.className="flex flex-row gap-2 border-2 px-2 py-1";

            //CartItem Name
            cartItemName.innerText=item.itemName;
            
            //CartItemMinusButton
            cartItemMinusButton.innerText="-";
            cartItemMinusButton.className="bg-red-500 rounded-lg px-2";

            cartItemMinusButton.addEventListener("click",function(event){
                cart[addButton.id]=cart[addButton.id]-1;
                if(cart[addButton.id]==0){
                    minusButton.classList.add("hidden");
                    plusButton.classList.add("hidden");
                    addButton.innerText="Add";
                    addButton.disabled=false;
                    delete cart[addButton.id];
                    cartItemsModal.removeChild(cartItem);
                }
                else{
                    addButton.innerText=cart[addButton.id];
                    cartItemQuantity.innerText=cart[addButton.id];
                }
            }); 

            //CartItemQty
            cartItemQuantity.innerText=cart[addButton.id];
            cartItemQuantity.id="qty"+addButton.id;

            //CartAddButton
            cartItemAddButton.innerText="+";
            cartItemAddButton.className="bg-green-500 rounded-lg px-2";
            
            cartItemAddButton.addEventListener("click",function(event){
                cart[addButton.id]=cart[addButton.id]+1;
                cartItemQuantity.innerText=cart[addButton.id];
                // const exAddButton=document.getElementsByName("add-button-"+)
                addButton.innerText=cart[addButton.id];
            });


            cartItem.appendChild(cartItemName);
            cartItem.appendChild(cartItemMinusButton);
            cartItem.appendChild(cartItemQuantity);
            cartItem.appendChild(cartItemAddButton);

            cartItemsModal.appendChild(cartItem)
        });

        plusButton.addEventListener('click',function(event){
            cart[addButton.id]=cart[addButton.id]+1;
            addButton.innerText=cart[addButton.id];

            //Cart Changes
            let qty=cartItemsModal.querySelector("#cart-item-"+addButton.id).querySelector("#qty"+addButton.id)
            qty.innerText=cart[addButton.id];

        });

        minusButton.addEventListener('click',function(event){
            cart[addButton.id]=cart[addButton.id]-1;
            if(cart[addButton.id]==0){
                minusButton.classList.add("hidden");
                plusButton.classList.add("hidden");
                addButton.innerText="Add";
                addButton.disabled=false;
                cartItemsModal.removeChild(cartItemsModal.querySelector("#cart-item-"+addButton.id));
                delete cart[addButton.id]
            }
            else{
                addButton.innerText=cart[addButton.id];
                
                    
                //Cart Changes
                let qty=cartItemsModal.querySelector("#cart-item-"+addButton.id).querySelector("#qty"+addButton.id)
                qty.innerText=cart[addButton.id];
            }
        })

        itemBlock.appendChild(itemName);
        itemBlock.appendChild(itemDescription);
        itemBlock.appendChild(itemType);
        itemBlock.appendChild(itemPrice);
        
        buttonsDiv.appendChild(minusButton);
        buttonsDiv.appendChild(addButton);
        buttonsDiv.appendChild(plusButton);

        itemBlock.appendChild(buttonsDiv);

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
            displayMenubyPage(currentPage,Menu,1);
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


//Order Data
let orders=[]

//gets all order details of the customer
async function fetchOrders(){
  
}


//Loading checkout modal
document.addEventListener('DOMContentLoaded',()=>{
    const orderItemsModal=document.getElementById('order-items-modal');
    document.getElementById('add-to-order-button').addEventListener('click',function(event){
        if(Object.keys(cart).length===0){
            alert("Add some items to order!!");
        }
        else{    
            orderItemsModal.classList.remove('hidden');
        }
    });

    const cancelButton=document.getElementById("cancel-button");
    const NewOrderButton=document.getElementById("new-order-button");
    const addToOrderButton=document.getElementById("order-button");

    addToOrderButton.addEventListener("click",function(event){
        console.log(cart);
    });

    cancelButton.addEventListener('click',function(event){
        orderItemsModal.classList.add("hidden");
    });

    window.addEventListener('click', (event) => {
        if (event.target === orderItemsModal) {
            orderItemsModal.classList.add('hidden');
        }
    });
})