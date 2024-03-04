import React, { useState, useEffect } from 'react';

export default function GetAllAppliedLoans(){

    const [loans, setLoans] = useState([]);
    var customerID = sessionStorage.getItem("CID");

    useEffect(() => {
    const fetchLoans = async () => {
      try {
        const token = sessionStorage.getItem('Token');
        const response = await fetch('https://localhost:7075/api/CustomerLoan/GetAllAppliedLoans?ID='+customerID, {
          method: 'GET',
          headers: {
            'Authorization': 'Bearer '+token,
            body: JSON.stringify(customerID), // Include your authorization token
            'Content-Type': 'application/json'
          }
        });

        if (response.ok) {
          const loansData = await response.json();
          setLoans(loansData);
        } else {
          console.error('Failed to fetch Loans');
        }
      } catch (error) {
        console.error('Error fetching loans:', error);
      } 
    }
    fetchLoans();
  },[]);
  var [flag,setFlag] = useState(0);
  var flagmethod = (e) =>{
    if(flag==0)
    setFlag(1)
  else
  setFlag(0)
  }
  return (
    <div>
                  <div className="container mt-5">
                <div className="row">
                    <div className="col-md-12 mb-4">
                        <div className="card p-4 custom-bg-color">
                                    <div className="card-body"></div>


<div>
      <h1>All your applied loans</h1>
      <button type="button" className="btn btn-success" data-toggle="button" 
      aria-pressed="false" onClick={flagmethod}>
      Get all your Loans
      </button>
      {flag==1? 
      <table className="table">
        <thead>
          <tr>
            <th>Loan ID</th>
            <th>Loan Amount</th>
            <th>Loan Purpose</th>
            <th>Status</th>
            <th>Repay Amount</th>
          </tr>
        </thead>
        <tbody>
        {loans.map(l => (
            <tr key={l.loanID}>
              <td>{l.loanID}</td>
              <td>{l.loanAmount}</td>
              <td>{l.loanPurpose}</td>
              <td>{l.status}</td>
              <td>{l.calculateFinalAmount}</td>
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
  );
}


