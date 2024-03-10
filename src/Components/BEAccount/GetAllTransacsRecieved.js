import React, { useState, useEffect } from 'react';
import './style.css'

export default function GetAllTransacsRecieved(){

  var customerID = sessionStorage.getItem("BID");
  const token = sessionStorage.getItem("Token");

  const [options,setOptions]= useState([]);
  const [selectedOption, setSelectedOption] = useState("");
    
  const handleChange = (event) => {
    setSelectedOption(event.target.value);
    setAccountNumber(String(event.target.value))
        
  };

  var [flag,setFlag] = useState(0);

  useEffect(() => {
    var func =async()=>{
        const response2 = await fetch('https://localhost:7075/api/BankEmpAccount/GetAllAccounts', {
        method: 'GET',
        headers: {
            'Authorization': 'Bearer '+token,
            body: JSON.stringify(customerID), 
            'Content-Type': 'application/json'
        }
    })
    .then(response => response.json())
    .then(data => {
      let filteredList = data.filter(obj => obj.status !== "Pending");
    setOptions(filteredList);
    });

    }
    func()
  },[])
    const [transacs, setTransacs] = useState([]);
    const [accountNumber,setAccountNumber] = useState("");
    var [flag2,setFlag2] = useState(0);

    const validateInput = ({ accountNumber }) => {
      if (!accountNumber.trim()) {
        return false;
      }
      return true;
    };


    const fetchTransacs = async () => {

      const validInput = validateInput({accountNumber})
      if(!validInput){
        alert("Account Number Cannot be null");
        return null;
      }

      try {
        const token = sessionStorage.getItem('Token');
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
  
  var flagmethod = (e) =>{
    if(flag2==0){
      fetchTransacs();
      setFlag2(1);
    }
    
  else
  setFlag2(0)
  }
  return (
    <div style={{ width: '100%'}}>



<div>
<div className="container mt-5">
                <div className="row">
                    <div className="col-md-12 mb-4">
                        <div className="card custom-bg-color">
                                    <div className="card-body">
                                        
      <h1>Transactions Recieved</h1>
      <div className="form-group">
      <div>
                                            <label htmlFor="input1">Account Number</label>
                                            <br/>
                                            <select value={selectedOption} onChange={handleChange} className="browser-default custom-select">
                                            <option value="">Select an option</option>
                                                {options.map((options) => (
                                                <option key={options.accountNumber} value={options.accountNumber}>
                                                    {options.accountNumber}
                                                </option>
                                                ))}
                                            </select>
                                        </div>
                                        </div>
      <button type="button" className="btn btn-success" data-toggle="button" 
      aria-pressed="false" onClick={flagmethod}>
      Get all Transactions
      </button>
      {flag2==1? 
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


