import React, { useState, useEffect } from 'react';
import './style.css'
export default function GetAllBeneficiaries(){

    const [beneficiary, setBeneficiary] = useState([]);
    var customerID = sessionStorage.getItem("CID");

    useEffect(() => {
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
          setBeneficiary(beneficiaryData);
        } else {
          console.error('Failed to fetch Loans');
        }
      } catch (error) {
        console.error('Error fetching loans:', error);
      } 
    }
    fetchBeneficiaries();
  },[]);
  var [flag,setFlag] = useState(0);
  var flagmethod = (e) =>{
    if(flag==0)
    setFlag(1)
  else
  setFlag(0)
    console.log(flag);
  }
  return (
    <div style={{ width: '100%', backgroundColor: 'lightblue' }}>
    <br/>

    <div>
    <div class="container mt-5">
                    <div class="row">
                    <div className="col-md-12 mb-4">
                            <div class="card custom-bg-color">
                                <div class="card-body">

      <h1>All your Beneficiaries</h1>
      <button type="button" class="btn btn-success" data-toggle="button" 
      aria-pressed="false" autocomplete="off" onClick={flagmethod}>
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


