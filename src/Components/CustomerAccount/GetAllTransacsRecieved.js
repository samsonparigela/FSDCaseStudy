import React, { useState, useEffect } from 'react';
import './style.css'

export default function GetAllTransacsRecieved(){

  var customerID = sessionStorage.getItem("CID");
  const token = sessionStorage.getItem("Token");

  const [options,setOptions]= useState([])
  const [selectedOption, setSelectedOption] = useState(null);
    
  const handleChange = (event) => {
    setAccountNumber(String(event.target.value))
    setSelectedOption(event.target.value);

        
  };
    const [transacs, setTransacs] = useState([]);
    const [accountNumber,setAccountNumber] = useState("");
    var [flag,setFlag] = useState(0);

    const validateInput = ({ accountNumber }) => {
      if (!accountNumber.trim()) {
        return false;
      }
      return true;
    };

    useEffect(() => {
      var func =async()=>{
          const response2 = await fetch('https://localhost:7075/api/CustomerAccount/View All your Accounts?ID='+customerID, {
          method: 'GET',
          headers: {
              'Authorization': 'Bearer '+token,
              body: JSON.stringify(customerID), 
              'Content-Type': 'application/json'
          }
      })
      .then(response => response.json())
      .then(data => {
      setOptions(data);
      });
    }
      func()
    },[])

    const fetchTransacs = async () => {
      const validInput = validateInput({accountNumber})
      if(!validInput){
        alert("Account Number Cannot be null");
        return null;
      }
      try {
        const response = await fetch('https://localhost:7075/api/BankEmpAccount/ViewReceivedTransactions?AID='+accountNumber, {
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
    const flagmethod = (e) =>{
      e.preventDefault()
      if(flag==0){
        fetchTransacs()
        setFlag(1);
      }
      
    else
    setFlag(0)
    }
  return (
    <div style={{ width: '100%', backgroundColor: 'lightblue' }}>



<div>
<div class="container mt-5">
                <div class="row">
                    <div class="col-md-12 mb-4">
                        <div class="card custom-bg-color">
                                    <div class="card-body">
                                        
      <h1>Transactions Recieved</h1>
      <div class="form-group">
      <div>
                                            <label htmlFor="input1">Account Number</label>
                                            <br/>
                                            <select value={selectedOption} onChange={handleChange} class="browser-default custom-select">
                                                {options.map((options) => (
                                                <option key={options.accountNumber} value={options.accountNumber}>
                                                    {options.accountNumber}
                                                </option>
                                                ))}
                                            </select>
                                        </div>
                                        </div>
      <button type="button" class="btn btn-success" data-toggle="button" 
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


