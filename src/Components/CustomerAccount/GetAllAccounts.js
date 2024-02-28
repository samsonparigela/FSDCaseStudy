import React, { useState, useEffect } from 'react';
import pic1 from './j2.jpg'
export default function GetAllAccounts(){

    const [orders, setOrders] = useState([]);
    var customerID = sessionStorage.getItem("CID");
    var [flag,setFlag] = useState(0);
    useEffect(() => {
      
    const fetchOrders = async () => {
      if(flag==0)
      return;
      try {
        const token = sessionStorage.getItem('Token');
        const response = await fetch('https://localhost:7075/api/CustomerAccount/View All your Accounts?ID='+customerID, {
          method: 'GET',
          headers: {
            'Authorization': 'Bearer '+token,
            body: JSON.stringify(customerID), // Include your authorization token
            'Content-Type': 'application/json'
          }
        });

        if (response.ok) {
          const ordersData = await response.json();
          setOrders(ordersData);
        } else {
          console.error('Failed to fetch orders');
        }
      } catch (error) {
        console.error('Error fetching orders:', error);
      }
    }
    fetchOrders();
  },[flag]);
  var flagmethod = (e) =>{
    if(flag==0)
    setFlag(1)
  else
  setFlag(0)
    console.log(flag);
  }
  return (
    <div style={{ width: '100%', backgroundColor: 'lightblue' }}>
    <div class="container mt-5">
        <div class="row">
            <div class="col-md-12 mb-4">
                <div class="card custom-bg-color">
                            <div class="card-body">


<div>
      <h1>All Accounts</h1>
      <button type="button" class="btn btn-success" data-toggle="button" 
      aria-pressed="false" onClick={flagmethod}>
      Get all your Accounts
      </button>
      {flag==1? 
      <table className="table">
        <thead>
          <tr>
            <th>AccountNumber</th>
            <th>CustomerID</th>
            <th>IFSC Code</th>
            <th>Status</th>
            <th>Account Type</th>
            <th>Balance</th>
            {/* Add more table headers as needed */}
          </tr>
        </thead>
        <tbody>
        {orders.map(acc => (
            <tr key={acc.accountNumber}>
              <td>{acc.accountNumber}</td>
              <td>{customerID}</td>
              <td>{acc.ifscCode}</td>
              <td>{acc.status}</td>
              <td>{acc.accountType}</td>
              <td>{acc.balance}</td>
            </tr>
          ))}
        </tbody>
      </table>
      :<p></p>}
    </div>
    </div>
    </div>
        </div>
        </div>
        </div>
        </div>
  );
}

