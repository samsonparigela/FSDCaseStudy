import React, { useState, useEffect } from 'react';
import pic1 from './j4.jpeg'

export default function ViewAllTransacsByAcc(){

  const customerID = sessionStorage.getItem("CID");
  const token = sessionStorage.getItem("Token");

  const [options, setOptions] = useState([]);
  const [accountNumber, setAccountNumber] = useState("");
  const [transacs, setTransacs] = useState([]);
  const [flag, setFlag] = useState(false);

  useEffect(() => {
    const fetchAccounts = async () => {
      try {
        const response = await fetch(`https://localhost:7075/api/CustomerAccount/View All your Accounts?ID=${customerID}`, {
          method: 'GET',
          headers: {
            'Authorization': `Bearer ${token}`,
            'Content-Type': 'application/json'
          }
        });

        if (response.ok) {
          const data = await response.json();
          const filteredList = data.filter(obj => obj.status !== "Pending" && obj.status !== "Account Closing Approved");
          setOptions(filteredList);
        } else {
          console.error('Failed to fetch accounts');
        }
      } catch (error) {
        console.error('Error fetching accounts:', error);
      }
    };
    fetchAccounts();
  }, [customerID, token]);

  const handleChange = (event) => {
    setAccountNumber(event.target.value);
  };

  const fetchTransacs = async () => {
    if (!accountNumber.trim()) {
      alert("Account Number cannot be empty");
      return;
    }
    try {
      const response = await fetch(`https://localhost:7075/api/CustomerAccount/ViewAllYourTransactionsByAccount?AID=${accountNumber}`, {
        method: 'GET',
        headers: {
          'Authorization': `Bearer ${token}`,
          'Content-Type': 'application/json'
        }
      });

      if (response.ok) {
        const data = await response.json();
        setTransacs(data);
        setFlag(true);
      } else {
        console.error('Failed to fetch transactions');
      }
    } catch (error) {
      console.error('Error fetching transactions:', error);
    }
  }
  var flagmethod = (e) =>{
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
          <h1>All your Transactions By Account</h1>
          <div className="form-group">
            <label htmlFor="input1">Account Number</label>
            <select value={accountNumber} onChange={handleChange} className="browser-default custom-select">
              <option value="">Select an option</option>
              {options.map(option => (
                <option key={option.accountNumber} value={option.accountNumber}>
                  {option.accountNumber}
                </option>
              ))}
            </select>
          </div>
          <button type="button" className="btn btn-success" onClick={flagmethod}>
            Get all your Transactions
          </button>
          {flag==1?(
            <div className="table-responsive">
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
            </div>
          ):<p></p>}
        </div>
      </div>
    </div>
  );
}
