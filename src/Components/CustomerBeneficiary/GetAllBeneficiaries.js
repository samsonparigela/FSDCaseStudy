import React, { useState, useEffect } from 'react';
import './style.css'
export default function GetAllBeneficiaries(){

    const [beneficiary, setBeneficiary] = useState([]);
    var customerID = sessionStorage.getItem("CID");

    const fetchBeneficiaries = async () => {
      try {
        const token = sessionStorage.getItem('Token');
        const response = await fetch('https://localhost:7075/api/CustomerBeneficiary/GetAllBeneficiary?CID='+customerID, {
          method: 'GET',
          headers: {
            'Authorization': 'Bearer '+token,
            body: JSON.stringify(customerID), // Include your authorization token
            'Content-Type': 'application/json'
          }
        });

        if (response.ok) {
          const beneficiaryData = await response.json();
          let filteredData = beneficiaryData.filter(obj => obj.beneficiaryName !== "Self");
          setBeneficiary(filteredData);
        } else {
          console.error('Failed to fetch Loans');
        }
      } catch (error) {
        console.error('Error fetching loans:', error);
      } 
    }
    
  var [flag,setFlag] = useState(false);
  useEffect(() => {
    if (flag) {
        fetchBeneficiaries();
    }
}, [flag]);
function flagmethod(){
  if(!flag)
  setFlag(true);
else
setFlag(false);
}
  return (
    <div style={{ width: '100%', backgroundColor: 'lightblue' }}>
    <br/>

    <div>
    <div className="container mt-5">
                    <div className="row">
                    <div className="col-md-12 mb-4">
                            <div className="card custom-bg-color">
                                <div className="card-body">

      <h1>All your Beneficiaries</h1>
      <button type="button" className="btn btn-success" data-toggle="button" 
      aria-pressed="false" onClick={flagmethod}>
      Get all your Beneficiaries
      </button>
      {flag==1? 
      <table className="table">
        <thead>
          <tr>
            <th>Beneficiary Account Number</th>
            <th>Beneficiary Name</th>
            <th>IFSC Code</th>
          </tr>
        </thead>
        <tbody>
        {beneficiary.map(l => (
            <tr key={l.beneficiaryAccountNumber}>
              <td>{l.beneficiaryAccountNumber}</td>
              <td>{l.beneficiaryName}</td>
              <td>{l.ifscCode}</td>
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


