import React, { useState} from 'react';
import './style.css'
export default function GetAllLoans(){

    const [loans, setLoans] = useState([]);
    var [flag,setFlag] = useState(0);

    const fetchLoans = async () => {
      try {
        const token = sessionStorage.getItem('Token');
        const response = await fetch('https://localhost:7075/api/BankEmpLoan/GetAllLoans', {
          method: 'GET',
          headers: {
            'Authorization': 'Bearer '+token,
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
    if(flag===0){
      fetchLoans();
      setFlag(1);
    }
    
  else
  setFlag(0)
  }
  return (
    <div style={{ width: '50%', backgroundColor: 'lightblue' }}>


<div>
<div className="container mt-5">
                <div className="row">
                    <div className="col-md-12 mb-4">
                        <div className="card custom-bg-color">
                                    <div className="card-body">
                                        
      <h1>All Loans</h1>
      <button type="button" className="btn btn-success" data-toggle="button" 
      aria-pressed="false" onClick={flagmethod}>
      Get
      </button>
      {flag===1? 
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


