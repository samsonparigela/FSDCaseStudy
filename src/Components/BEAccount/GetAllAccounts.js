import React, { useState, useEffect } from 'react';
import './style.css'
import AccountRow from './AccountRow';
export default function GetAllAccounts(){

    const [accounts, setAccounts] = useState([]);
    var customerID = sessionStorage.getItem("BID");
    var [flag,setFlag] = useState(0);
    
    const fetchAccounts = async () => {
      try {
        const token = sessionStorage.getItem('Token');
        const response = await fetch('https://localhost:7075/api/BankEmpAccount/GetAllAccounts', {
          method: 'GET',
          headers: {
            'Authorization': 'Bearer '+token,
            body: JSON.stringify(customerID), // Include your authorization token
            'Content-Type': 'application/json'
          }
        });

        if (response.ok) {
          const accountsData = await response.json();
          setAccounts(accountsData);
        } 
      } catch (error) {
        console.error('Error fetching:', error);
      }
    }
  
  var flagmethod = (e) =>{
    if(flag===0){
      setFlag(1)
      fetchAccounts()
    }
    
  else
  setFlag(0)
  }
  return (
    <div style={{ width: '100%'}}>



<div>
<div className="container mt-5">
                <div className="row">
                    <div className="col-md-12 mb-4">
                        <div className="card custom-bg-color">
                                    <div className="card-body">
                                        
      <h1>All Accounts</h1>
      <button type="button" className="btn btn-success" data-toggle="button" 
      aria-pressed="false" onClick={flagmethod}>
      Get all Accounts
      </button>
      {flag==1? 
      <div className="table-responsive">
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
        {accounts.map(account => (
                                                    <AccountRow key={account.accountNumber} account={account} />
                                                ))}
        </tbody>
      </table>
      </div>
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


