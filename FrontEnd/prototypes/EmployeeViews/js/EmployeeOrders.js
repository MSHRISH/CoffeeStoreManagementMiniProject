let order=[]
let orderId=0
let orderItemId=0;
const Employee = JSON.parse(localStorage.getItem('EmployeeData'));
const decodedToken=jwtRipOpen(Employee.token);

//Fetch MyOrder
async function fetchOrder(){
    try{
        let options={
            headers:{
                'accept':'text/plain',
                'Authorization':'Bearer '+JSON.parse(localStorage.getItem("EmployeeData")).token
            }
        }
        const apiURL="http://localhost:5122/api/OrderServices/GetOrderById/"+orderId;
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
            itemRow.className="grid grid-cols-6 gap-2";

            const itemIdSpan=document.createElement('span');
            itemIdSpan.innerText=orderItem.itemName;

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

            const changeItemStatusButton=document.createElement('button');
            changeItemStatusButton.className="border-2 px-2 bg-red-500 rounded-lg hover:border-red-700";
            changeItemStatusButton.innerText="Change Status";
            
            //Change Status Modal
            changeItemStatusButton.addEventListener('click',async ()=>{
                document.getElementById('ChangeStatus-modal').classList.remove("hidden");
                console.log(orderItem.orderItemId);
                orderItemId=orderItem.orderItemId;
                let status=['Accepted','Preparation','Deleivered']
                status=status.filter(item=>item!==orderItem.itemStatus);

                const statusSelect=document.getElementById('select-status');
                statusSelect.innerText="";

                status.forEach(s => {
                    const statusOption=document.createElement('option');
                    statusOption.value=s;
                    statusOption.innerText=s;

                    statusSelect.appendChild(statusOption)

                });


            });


            cancelItemButton.addEventListener('click',async ()=>{
                // console.log(orderItem.itemId);
                try{
                    const requestBody={orderItemId:orderItem.orderItemId};
                    let options={
                        method: 'DELETE',
                        headers:{
                            'accept':'text/plain',
                            'Authorization':'Bearer '+JSON.parse(localStorage.getItem("EmployeeData")).token
                            ,'Content-Type':'application/json'
                        }
                        ,body: JSON.stringify(requestBody)
                    }
                    const apiURL="http://localhost:5122/api/OrderServices/CancelOrderItemByStore";
                    
                    const response=await fetch(apiURL,options);
                    let data=await response.json();
                    console.log(data);
                    alert('ItemCancelled');
                    fetchOrder();
                }catch(error){
                    console.log("Erro!:",error);
                }

            });


            if(orderItem.cancellationStatus!=="NULL" || orderItem.itemStatus!=="Accepted"){
                cancelItemButton.disabled=true;
                cancelItemButton.className="border-2 px-2 bg-slate-500 rounded-lg"
                // console.log("Cancelled");
                // changeItemStatusButton.disabled=true;
                // changeItemStatusButton.className="border-2 px-2 bg-slate-500 rounded-lg";
            }

            if(myOrder.orderStatus!="Open" && myOrder.orderStatus!="Closed"){
                console.log(myOrder.orderStatus);
                cancelItemButton.disabled=true;
                cancelItemButton.className="border-2 px-2 bg-slate-500 rounded-lg"
            }

            if(orderItem.cancellationStatus!=="NULL"){
                changeItemStatusButton.disabled=true;
                changeItemStatusButton.className="border-2 px-2 bg-slate-500 rounded-lg";
            }
            
            if(decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]=="Barista"){
                cancelItemButton.disabled=true;
                cancelItemButton.className="border-2 px-2 bg-slate-500 rounded-lg";
            }
            
            itemRow.appendChild(itemIdSpan);
            itemRow.appendChild(itemQtySpan);
            itemRow.appendChild(itemStatusSpan);
            itemRow.appendChild(cancellationStatusSpan);
            itemRow.appendChild(cancelItemButton);
            itemRow.appendChild(changeItemStatusButton);

            orderItemsList.appendChild(itemRow);

            //Cancell Order By Store
            const cancelOrderButton=document.getElementById('cancel-order-button');
            
            if(myOrder.orderStatus!="Open"||decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]==="Barista"){
                cancelOrderButton.disabled=true;
                cancelOrderButton.className="border-2 px-2 bg-slate-500 rounded-lg"
            }
            else{
                cancelOrderButton.disabled=false;
                cancelOrderButton.className="border-2 px-2 bg-red-500 rounded-lg";
            }
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
                'Authorization':'Bearer '+JSON.parse(localStorage.getItem("EmployeeData")).token
            }
        }

        const response=await fetch('http://localhost:5122/api/OrderServices/GetAllOrders',options);
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

        fetchOrder();
        document.getElementById("main-container").classList.remove("hidden")

    });


    //Cancel Change Status
    document.getElementById('cancelBtn').addEventListener('click',()=>{
        document.getElementById('ChangeStatus-modal').classList.add("hidden");
    })


    //Change Status
    document.getElementById('confirmChangeStatusBtn').addEventListener('click',async ()=>{
        console.log(orderItemId);
        
        try{
            let options={
                method: 'PUT',
                headers:{
                    'accept':'text/plain',
                    'Authorization':'Bearer '+JSON.parse(localStorage.getItem("EmployeeData")).token
                }
            }
            
            let apiUrl="http://localhost:5122/api/OrderServices/"
            if(document.getElementById('select-status').value==="Accepted"){
                apiUrl=apiUrl+"AcceptedOrderItem/"+orderItemId;
            }
            if(document.getElementById('select-status').value==="Preparation"){
                apiUrl=apiUrl+"PreparationStartedOrderItem/"+orderItemId;
            }
            if(document.getElementById('select-status').value==="Deleivered"){
                apiUrl=apiUrl+"DeleiveredOrderItem/"+orderItemId;
                console.log(apiUrl);
            }

            const response=await fetch(apiUrl,options);
            const data=await response.json();
            console.log(data);
            alert("Item Status Changed");
            document.getElementById('ChangeStatus-modal').classList.add("hidden");
            fetchOrder();

        }catch(error){
            console.log("Error!", error);
        }
        
    });

    window.addEventListener('click', (event) => {
        if (event.target === document.getElementById('ChangeStatus-modal')) {
            document.getElementById('ChangeStatus-modal').classList.add("hidden");
        }
    });


    ////CancelOrderButton
    const cancelOrderButton=document.getElementById('cancel-order-button');



    
    cancelOrderButton.addEventListener('click',async ()=>{
        console.log(orderId);
        try{
            
            let options={
                method: 'DELETE',
                headers:{
                    'accept':'text/plain',
                    'Authorization':'Bearer '+JSON.parse(localStorage.getItem("EmployeeData")).token
                }
            }
            const apiURL="http://localhost:5122/api/OrderServices/CancelOrderByStore/"+orderId;

            const response=await fetch(apiURL,options);
            let data=await response.json();
            console.log(data);
            alert('OrderCancelled');
            fetchOrder();

        }catch(error){
            console.log("error! :",error);
        }
    });

});