import React, { useState } from 'react';
import './style.css';

export default function GetAllAccounts() {
  const [accounts, setAccounts] = useState([]);
  const [flag, setFlag] = useState(false);
  const customerID = sessionStorage.getItem("AdminID");

  const fetchAccounts = async () => {
    try {
      const token = sessionStorage.getItem('Token');
      const response = await fetch('https://localhost:7075/api/BankEmpAccount/GetAllAccounts', {
        method: 'GET',
        headers: {
          'Authorization': 'Bearer ' + token,
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

  const flagMethod = () => {
    setFlag(!flag);
    if (!flag) {
      fetchAccounts();
    }
  }

  return (
    <div style={{ width: '100%', backgroundColor: 'lightblue' }}>
      <div className="container mt-5">
        <div className="row">
          <div className="col-md-12 mb-4">
            <div className="card custom-bg-color">
              <div className="card-body">
                <h1>All Accounts</h1>
                <button type="button" className="btn btn-success" onClick={flagMethod}>
                  {flag ? 'Hide Accounts' : 'Get all Accounts'}
                </button>
                {flag &&
                  <table className="table">
                    <thead>
                      <tr>
                        <th>Account Number</th>
                        <th>Customer ID</th>
                        <th>IFSC Code</th>
                        <th>Status</th>
                        <th>Account Type</th>
                        <th>Balance</th>
                        {/* Add more table headers as needed */}
                      </tr>
                    </thead>
                    <tbody>
                      {accounts.map(acc => (
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
                }
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
