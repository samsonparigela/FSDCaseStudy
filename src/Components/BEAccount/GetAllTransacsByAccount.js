import React, { useState, useEffect } from 'react';
import './style.css'

export default function GetAllTransacsByAccount(){

    const [transacs, setTransacs] = useState([]);
    const [accountNumber,setAccountNumber] = useState("");
    var [flagg,setFlagg] = useState(0);

    const validateInput = ({ accountNumber }) => {
      if (!accountNumber.trim()) {
        return false;
      }
      return true;
    };

    var customerID = sessionStorage.getItem("BID");
    const fetchTransacs = async () => {

      const validInput = validateInput({accountNumber})
      if(!validInput){
        alert("Account Number Cannot be null");
        return null;
      }

      try {
        const token = sessionStorage.getItem('Token');
        const response = await fetch('https://localhost:7075/api/BankEmpAccount/ViewTransactionDetailsByAccount?AID='+accountNumber, {
          method: 'GET',
          headers: {
            'Authorization': 'Bearer '+token,
            body: JSON.stringify(accountNumber), // Include your authorization token
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
  
  var flagmethod = (e) =>{
    if(flagg==0){
      fetchTransacs();
      setFlagg(1);
    }
    
  else
  setFlagg(0)
  }
  return (
    <div style={{ width: '100%', backgroundColor: 'lightblue' }}>



<div>
<div class="container mt-5">
                <div class="row">
                    <div class="col-md-12 mb-4">
                        <div class="card custom-bg-color">
                                    <div class="card-body">
                                        
      <h1>Transactions By An account</h1>
      <div class="form-group">
                                            <label htmlFor="accountName">Account Number</label>
                                            <input type="text" class="form-control"  id="accountNumber" placeholder="Enter your Account Number"
                                            value={accountNumber} onChange={(e)=>setAccountNumber(e.target.value)}/>
                                        </div>
      <button type="button" class="btn btn-success" data-toggle="button" 
      aria-pressed="false" onClick={flagmethod}>
      Get all Transactions
      </button>
      {flagg==1? 
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


