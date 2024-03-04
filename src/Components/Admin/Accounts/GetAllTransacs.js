import React, { useState, useEffect } from 'react';
import './style.css'

export default function GetAllTransacs(){

    const [transacs, setTransacs] = useState([]);
    var customerID = sessionStorage.getItem("AdminID");

    const fetchTransacs = async () => {
      try {
        const token = sessionStorage.getItem('Token');
        const response = await fetch('https://localhost:7075/api/Admin/GetAllTransactions', {
          method: 'GET',
          headers: {
            'Authorization': 'Bearer '+token,
            body: JSON.stringify(customerID), // Include your authorization token
            'Content-Type': 'application/json'
          }
        });

        if (response.ok) {
          const TransacsData = await response.json();
          setTransacs(TransacsData);
        } else {
          console.error('Failed to fetch');
        }
      } catch (error) {
        console.error('Error fetching:', error);
      }
    }

  var [flag,setFlag] = useState(0);
  var flagmethod = (e) =>{
    if(flag==0){
      setFlag(1);
      fetchTransacs();
    }
    
  else
  setFlag(0)
  }
  return (
    <div style={{ width: '100%', backgroundColor: 'lightblue' }}>



<div>
<div className="container mt-5">
                <div className="row">
                    <div className="col-md-12 mb-4">
                        <div className="card custom-bg-color">
                                    <div className="card-body">
                                        
      <h1>All Transactions</h1>
      <button type="button" className="btn btn-success" data-toggle="button" 
      aria-pressed="false" onClick={flagmethod}>
      Get all Transactions
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


