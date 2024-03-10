import React, { useState} from 'react';
import './style.css'

export default function GetAllAccountsForCloseRequest(){

    const [accounts, setAccounts] = useState([]);
    const [flag,setFlag] = useState(0);
    function AccountRow({ account }) {
      return (
          <tr key={account.accountNumber}>
              <td>{account.accountNumber}</td>
              <td>{account.customerID}</td>
              <td>{account.ifscCode}</td>
              <td>{account.status}</td>
              <td>{account.accountType}</td>
              <td>{account.balance}</td>
          </tr>
      );
  }
    const fetchAccounts = async () => {
      try {
        const token = sessionStorage.getItem('Token');
        const response = await fetch('https://localhost:7075/api/BankEmpAccount/GetAllAccountsForCloseRequest', {
          method: 'GET',
          headers: {
            'Authorization': 'Bearer '+token,
            'Content-Type': 'application/json'
          }
        });

        if (response.ok) {
          const accountsData = await response.json();
          setAccounts(accountsData);
        } else {
          console.error('Failed to fetch');
        }
      } catch (error) {
        console.error('Error fetching:', error);
      }
    }
  const flagmethod = (e) =>{
    if(flag==0){
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
                                       
                                    <h1 className="text-center">All Accounts for Closing</h1>
                            <div className="text-center"> 
                            <button type="button" className="btn btn-success mb-3" onClick={flagmethod}>
                            Get all Accounts
                            </button>
                            </div>
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


