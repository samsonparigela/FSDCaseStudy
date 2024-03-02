import React, { useState, useEffect } from 'react';
import './style.css'
export default function GetAll(){
    
    const [loans, setLoans] = useState([]);
    var bankEmpID = sessionStorage.getItem("AdminID");
    var [flag,setFlag] = useState(0);

    const fetchLoans = async () => {
      try {
        const token = sessionStorage.getItem('Token');
        const response = await fetch('https://localhost:7075/api/Admin/GetAllBranches', {
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
    if(flag==0){
      fetchLoans();
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
                                        
      <h1>All Branches</h1>
      <button type="button" class="btn btn-success" data-toggle="button" 
      aria-pressed="false" onClick={flagmethod}>
      Get
      </button>
      {flag==1? 
      <table className="table">
        <thead>
          <tr>
            <th>Bank ID</th>
            <th>Branch Name</th>
            <th>IFSC Code</th>
            <th>City</th>
          </tr>
        </thead>
        <tbody>
        {loans.map(loans => (
            <tr key={loans.ifscCode}>
              <td>{loans.bankID}</td>
              <td>{loans.branchName}</td>
              <td>{loans.ifscCode}</td>
              <td>{loans.city}</td>
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


