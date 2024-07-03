let order=[]
let orderId=0

//Fetch MyOrder
async function fetchMyOrder(){
    try{
        let options={
            headers:{
                'accept':'text/plain',
                'Authorization':'Bearer '+JSON.parse(localStorage.getItem("CustomerData")).token
            }
        }
        const apiURL="http://localhost:5122/api/OrderServices/GetMyOrderDetails/"+orderId;
        const response=await fetch(apiURL,options);
        let myOrder=await response.json();
        console.log(myOrder);

        const orderDetailsContainer=document.getElementById('order-details-container');
        orderDetailsContainer.innerHTML="";

        const orderIdSpan=document.createElement('span')
        orderIdSpan.innerText="OrderID: "+ myOrder.orderId;

        const orderStatusSpan=document.createElement('span');
        orderStatusSpan.innerText="Status: "+myOrder.orderStatus;

        const totalAmountSpan=document.createElement('span');
        totalAmountSpan.innerText="Cost: INR "+myOrder.totalAmount;

        const orderedOnSpan=document.createElement('span');
        orderedOnSpan.innerText="Ordered On: "+myOrder.orderedOn.slice(0,10);

        orderDetailsContainer.appendChild(orderIdSpan);
        orderDetailsContainer.appendChild(orderStatusSpan);
        orderDetailsContainer.appendChild(totalAmountSpan);
        orderDetailsContainer.appendChild(orderedOnSpan);

        const orderItemsList=document.getElementById('order-items-list');
        orderItemsList.innerHTML="";
        
        myOrder.orderItems.forEach(orderItem => {
            const itemRow=document.createElement('div');
            itemRow.className="grid grid-cols-5 gap-2";

            const itemIdSpan=document.createElement('span');
            itemIdSpan.innerText="ItemId: "+orderItem.itemId;

            const itemQtySpan=document.createElement('span');
            itemQtySpan.className="font-bold";
            itemQtySpan.innerText="Qty: "+orderItem.quantity;

            const itemStatusSpan=document.createElement('span');
            itemStatusSpan.className="text-blue-900";
            itemStatusSpan.innerText="Status: "+orderItem.itemStatus;

            const cancellationStatusSpan=document.createElement('span');
            cancellationStatusSpan.className="text-red-500 overflow-x-auto ml-2 sm:ml-0";
            cancellationStatusSpan.innerText="Cancell Status: "+orderItem.cancellationStatus;

            const cancelItemButton=document.createElement('button');
            cancelItemButton.className="border-2 px-2 bg-red-500 rounded-lg hover:border-red-700";
            cancelItemButton.innerText="Cancel Item";

            if(orderItem.cancellationStatus!=="NULL"){
                cancelItemButton.disabled=true;
                cancelItemButton.className="border-2 px-2 bg-slate-500 rounded-lg"
                console.log("Cancelled");
            }
            
            itemRow.appendChild(itemIdSpan);
            itemRow.appendChild(itemQtySpan);
            itemRow.appendChild(itemStatusSpan);
            itemRow.appendChild(cancellationStatusSpan);
            itemRow.appendChild(cancelItemButton);

            orderItemsList.appendChild(itemRow);
        });
    }catch(error){
        console.log("Error",error);
    }
}

document.addEventListener('DOMContentLoaded',async ()=>{
    const orderIdInput=document.getElementById('select-order-id');
    
    //Fetching Order Details
    try{
        let options={
            headers:{
                'accept':'text/plain',
                'Authorization':'Bearer '+JSON.parse(localStorage.getItem("CustomerData")).token
            }
        }

        const response=await fetch('http://localhost:5122/api/OrderServices/GetAllMyOrders',options);
        const data=await response.json();
        orders=data;

        orders.forEach(order => {
            const orderIdOption=document.createElement('option');
            orderIdOption.value=order.orderId;
            orderIdOption.innerText=order.orderId;

            orderIdInput.appendChild(orderIdOption);
        });
    }catch(error){
        console.error("Error:: ",error);
    }

    const getOrderDetailsButton=document.getElementById('get-order-details-button');
    
    getOrderDetailsButton.addEventListener('click',()=>{
        document.getElementById('prompt').classList.add('hidden');

        orderId=orderIdInput.value;
        console.log(orderId);

        fetchMyOrder();
        document.getElementById("main-container").classList.remove("hidden")

    });
});