import React, { useState, useEffect } from 'react';
import pic1 from './j4.jpeg'
export default function ThisMonthTransacs(){

    
        const [transacs, setTransacs] = useState([]);
        var customerID = sessionStorage.getItem("CID");

        useEffect(() => {
        const fetchTransacs = async () => {
          try {
            const token = sessionStorage.getItem('Token');
            const response = await fetch('https://localhost:7075/api/CustomerAccount/View Transactions in Last month?CID=' + customerID, {
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
        fetchTransacs();
      },[]);
      var [flag,setFlag] = useState(0);
      var flagmethod = (e) =>{
        if(flag==0)
        setFlag(1)
      else
      setFlag(0)
      }
      return (
        <div style={{ width: '100%', backgroundColor: 'lightblue' }}>
        <div class="container mt-5">
            <div class="row">
                <div class="col-md-12 mb-4">
                    <div class="card custom-bg-color">
                                <div class="card-body">
    
    <div>
          <h1>All your last month Transactions</h1>
          <button type="button" class="btn btn-success" data-toggle="button" 
          aria-pressed="false" onClick={flagmethod}>
          Get all your Transactions
          </button>
          {flag==1? 
          <table className="table">
            <thead>
              <tr>
                <th>TransactionID</th>
                <th>Status</th>
                <th>Source Account Number</th>
                <th>Destination Account Number</th>
                <th>Transaction Type</th>
                <th>Amount</th>
                <th>Description</th>
                <th>Transaction Date</th>
                {/* Add more table headers as needed */}
              </tr>
            </thead>
            <tbody>
            {transacs.map(tran => (
                <tr key={tran.transactionID}>
                  <td>{tran.transactionID}</td>
                  <td>{tran.status}</td>
                  <td>{tran.sAccountID}</td>
                  <td>{tran.beneficiaryAccountNumber}</td>
                  <td>{tran.transactionType}</td>
                  <td>{tran.amount}</td>
                  <td>{tran.description}</td>
                  <td>{tran.transactionDate}</td>
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