import React, { useState, useEffect } from 'react';
import './style.css'
import TransactionTable from '../BEAccount/TransactionTable';
export default function GetAllTransacsSent(){

  const token = sessionStorage.getItem("Token");

  const [options,setOptions]= useState([])
  const [selectedOption, setSelectedOption] = useState("");
    
  const handleChange = (event) => {
    setSelectedOption(event.target.value);
    setAccountNumber(String(event.target.value))
        
  };
    const [transacs, setTransacs] = useState([]);
    const [accountNumber,setAccountNumber] = useState(0);
    var [flag,setFlag] = useState(0);

    useEffect(() => {
      var func =async()=>{
          await fetch('https://localhost:7075/api/CustomerAccount/View All your Accounts?ID='+sessionStorage.getItem("CID"), {
          method: 'GET',
          headers: {
              'Authorization': 'Bearer '+token,
              body: JSON.stringify(sessionStorage.getItem("CID")), 
              'Content-Type': 'application/json'
          }
      })
      .then(response => response.json())
      .then(data => {
        let filteredList = data.filter(obj => obj.status !== "Pending" && obj.status !== "Account Closing Approved");
        setOptions(filteredList);;
      });
  
      }
      func()
    },[])

    const fetchTransacs = async () => {
          if(!accountNumber){
            alert("Account Number Cannot be null");
            return null;
          }
      try {
        const response = await fetch('https://localhost:7075/api/BankEmpAccount/ViewSentTransactions?AID='+accountNumber, {
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
    <div style={{ width: '100%'}}>



<div>
<div className="container-fluid mt-5">
                <div className="row">
                    <div className="col-md-12 mb-4">
                        <div className="card custom-bg-color">
                                    <div className="card-body">
                                        
      <h1>Transactions Sent</h1>
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
      {flag==1? 
      <TransactionTable transacs={transacs}/>
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


