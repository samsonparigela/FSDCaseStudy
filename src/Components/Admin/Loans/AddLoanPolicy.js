import React, { useState, useEffect } from 'react';
import './style.css'

export default function AddLoanPolicy(){

  const [Customers, setCustomers] = useState({});
  const [CustomerID, setCustomerID] = useState({});
  var AdminID = sessionStorage.getItem("AdminID");
  const token = sessionStorage.getItem("Token");

  var [flag,setFlag] = useState(0);

    var [flag,setFlag] = useState(0);
    var [tenureInMonths,setTenureInMonths] = useState(0);
    var [interest,setInterest] = useState(0);
    var [amount,setAmount] = useState(0);
    var loanPolicy = {
        "loanPolicyID": 0,
        "tenureInMonths": tenureInMonths,
        "interest": interest,
        "loanAmount": amount
    }
    const fetchCustomers = async () => {
      try {
        const token = sessionStorage.getItem('Token');
        const response = await fetch('https://localhost:7075/api/Admin/AddLoanPolicies', {
          method: 'POST',
          'body':JSON.stringify(loanPolicy),
          headers: {
            'body':JSON.stringify(loanPolicy),
            'Authorization': 'Bearer '+token,
            'Content-Type': 'application/json'
          }
        });

        if (response.ok) {
          const accountsData = await response.json();
          setCustomers(accountsData);
        } else {
          console.error('Failed to fetch');
        }
      } catch (error) {
        console.error('Error fetching:', error);
      }
    }
  
  var flagmethod = (e) =>{
    if(flag==0){
      fetchCustomers()
      setFlag(1)

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
                                    <h1>Add Loan Policy</h1>
                                    <div class="form-group">
      <div>
                                            <label htmlFor="input1">Tenure In Months</label>
                            <input type="text" class="form-control" id="input1" placeholder="Enter Tenure in months"
                             value={tenureInMonths} onChange={(e)=>setTenureInMonths(e.target.value)}/>
                             <br/>
                             <label htmlFor="input1">Interest</label>
                            <input type="text" class="form-control" id="input1" placeholder="Enter Interest"
                             value={interest} onChange={(e)=>setInterest(e.target.value)}/>
                             <br/><label htmlFor="input1">Amount</label>
                            <input type="text" class="form-control" id="input1" placeholder="Enter Amount"
                             value={amount} onChange={(e)=>setAmount(e.target.value)}/>
                             <br/>
                                        </div>
                                        </div>
                                        

      <button type="button" class="btn btn-success" data-toggle="button" 
      aria-pressed="false" onClick={flagmethod}>
      Add Loan Policy
      </button>
      {flag==1? 
      <table className="table">
        <thead>
          <tr>
            <th>Loan Policy ID</th>
            <th>Loan tenureInMonths</th>
            <th>Interest</th>
            <th>Amount</th>
          </tr>
        </thead>
        <tbody>
            <tr key={Customers.loanPolicyID}>
              <td>{Customers.loanPolicyID}</td>
              <td>{Customers.tenureInMonths}</td>
              <td>{Customers.interest}</td>
              <td>{Customers.loanAmount}</td>
            </tr>
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



     
     
     
     
     
     
     