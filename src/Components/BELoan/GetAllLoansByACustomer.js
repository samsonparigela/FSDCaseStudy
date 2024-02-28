import React, { useState, useEffect } from 'react';
import './style.css'
export default function GetAllLoansbyACustomer(){

    const [loans, setLoans] = useState([]);
    var bankEmpID = sessionStorage.getItem("BID");
    var [flag,setFlag] = useState(0);
    var [customerID,setCustomerID] =useState("");

    const validateInput = ({ customerID }) => {
      if (!customerID.trim()) {
        return false;
      }
      return true;
    };

    const fetchLoans = async () => {

      const validInput = validateInput({customerID})
      if(!validInput){
        alert("Customer ID cannot be null");
        return null;
      }


      try {
        const token = sessionStorage.getItem('Token');
        const response = await fetch('https://localhost:7075/api/BankEmpLoan/GetAllLoansbyACustomer?CID='+customerID, {
          method: 'GET',
          headers: {
            'Authorization': 'Bearer '+token,
            body: JSON.stringify(bankEmpID), // Include your authorization token
            'Content-Type': 'application/json'
          }
        });

        if (response.ok) {
          const loansData = await response.json();
          setLoans(loansData);
        } else {
          console.error('Failed to fetch');
        }
      } catch (error) {
        console.error('Error fetching:', error);
      }
    }
  
  var flagmethod = (e) =>{
    if(flag==0){
      fetchLoans();
      setFlag(1);
    }
    
  else
  setFlag(0)
  }
  return (
    <div style={{ width: '50%', backgroundColor: 'lightblue' }}>


<div>
<div class="container mt-5">
                <div class="row">
                    <div class="col-md-12 mb-4">
                        <div class="card custom-bg-color">
                                    <div class="card-body">
                                        
      <h1>All Loans by Customer</h1>
      <div class="form-group">
                                    <label htmlFor="accountName">Customer ID</label>
                                    <input type="text" class="form-control"  id="accountNumber" placeholder="Enter CustomerID"
                                    value={customerID} onChange={(e)=>setCustomerID(e.target.value)}/>
                                </div>
      <button type="button" class="btn btn-success" data-toggle="button" 
      aria-pressed="false" onClick={flagmethod}>
      Get
      </button>
      {flag==1? 
      <table className="table">
        <thead>
          <tr>
            <th>Loan ID</th>
            <th>Customer ID</th>
            <th>Loan Amount</th>
            <th>Status</th>
            <th>Loan Purpose</th>
          </tr>
        </thead>
        <tbody>
        {loans.map(loans => (
            <tr key={loans.loanID}>
              <td>{loans.loanID}</td>
              <td>{loans.customerID}</td>
              <td>{loans.loanAmount}</td>
              <td>{loans.status}</td>
              <td>{loans.loanPurpose}</td>
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


