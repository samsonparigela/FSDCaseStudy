import React, { useState, useEffect } from 'react';
import TransactionTable from '../BEAccount/TransactionTable';
export default function ViewAllYourTransacs(){

    
        const [transacs, setTransacs] = useState([]);
        var customerID = sessionStorage.getItem("CID");

        const fetchTransacs = async () => {
          try {
            const token = sessionStorage.getItem('Token');
            const response = await fetch('https://localhost:7075/api/CustomerAccount/ViewAllYourTransactions?CID=' + customerID, {
              method: 'GET',
              headers: {
                'Authorization': 'Bearer '+token,
                body: JSON.stringify(customerID), // Include your authorization token
                'Content-Type': 'application/json'
              }
            });
    
            if (response.ok) {
              const ordersData = await response.json();
              setTransacs(ordersData);
            } else {
              console.error('Failed to fetch orders');
            }
          } catch (error) {
            console.error('Error fetching orders:', error);
          }
        }
      var [flag,setFlag] = useState(0);
      const flagmethod = (e) =>{
        if(flag==0){
          fetchTransacs()
          setFlag(1)
        }
        
      else
      setFlag(0)
      }
      return (
        <div className="container-fluid mt-5">
        <div className="card custom-bg-color">
          <div className="card-body">
          <h1>All your Transactions</h1>
          <button type="button" className="btn btn-success" data-toggle="button" 
          aria-pressed="false" onClick={flagmethod}>
          Get all your Transactions
          </button>
          {flag==1? 
      <TransactionTable transacs={transacs}/>
      :<p></p>}
        </div>

        </div>
        </div>

      );
    }