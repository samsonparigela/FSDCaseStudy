import React, { useState, useEffect } from 'react';
import './style.css'
import LoanTable from './LoanTable';
export default function GetLoansThatNeedApproval(){

  
    const [loans, setLoans] = useState([]);
    var bankEmpID = sessionStorage.getItem("BID");
    var [flag,setFlag] = useState(0);

    const fetchLoans = async () => {
      try {
        const token = sessionStorage.getItem('Token');
        const response = await fetch('https://localhost:7075/api/BankEmpLoan/GetAllLoans', {
          method: 'GET',
          headers: {
            'Authorization': 'Bearer '+token,
            body: JSON.stringify(bankEmpID), // Include your authorization token
            'Content-Type': 'application/json'
          }
        });

        if (response.ok) {
          const loansData = await response.json();
          let filteredList = loansData.filter(obj => obj.status === "Pending" || obj.status === "Extend Request");
          setLoans(filteredList);
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
    <div style={{ width: '100%'}}>


<div>
<div className="container mt-5">
                <div className="row">
                    <div className="col-md-12 mb-4">
                        <div className="card custom-bg-color">
                                    <div className="card-body">
                                        
      <h1>All Loans that need Approval</h1>
      <button type="button" className="btn btn-success" data-toggle="button" 
      aria-pressed="false" onClick={flagmethod}>
      Get
      </button>
      {flag==1? 
            <LoanTable loans={loans}/>
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


