import React, { useState, useEffect } from 'react';
import './style.css'

export default function GetCustomerForAccount(){

    const [profile, setCustomer] = useState({});
    const [accountNumber,setAccountNumber] = useState("");
    var [flag,setFlag] = useState(0);

    const validateInput = ({ accountNumber }) => {
      if (!accountNumber.trim()) {
        return false;
      }
      return true;
    };

    var customerID = sessionStorage.getItem("BID");

    const fetchCustomer = async () => {

      const validInput = validateInput({accountNumber})
      if(!validInput){
        alert("Account Number Cannot be null");
        return null;
      }

      try {
        const token = sessionStorage.getItem('Token');
        const response = await fetch('https://localhost:7075/api/BankEmpAccount/GetCustomerDetailsForAccount?AID='+accountNumber, {
          method: 'GET',
          headers: {
            'Authorization': 'Bearer '+token,
            body: JSON.stringify(accountNumber), // Include your authorization token
            'Content-Type': 'application/json'
          }
        });

        if (response.ok) {
          const customerData = await response.json();
          setCustomer(customerData);
          console.log("Success");
        } else {
          console.error('customerData');
        }
      } catch (error) {
        console.error('Error fetching:', error);
      }
    }
  
  var flagmethod = (e) =>{
    if(flag==0){
      fetchCustomer();
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
                                        
      <h1>Customer for Account</h1>
      <div class="form-group">
                                            <label htmlFor="accountName">Account Number</label>
                                            <input type="text" class="form-control"  id="accountNumber" placeholder="Enter your Account Number"
                                            value={accountNumber} onChange={(e)=>setAccountNumber(e.target.value)}/>
                                        </div>
      <button type="button" class="btn btn-success" data-toggle="button" 
      aria-pressed="false" onClick={flagmethod}>
      Get Customer
      </button>
      {flag==1? 
      <table className="table">
      <thead>
        <tr>
          <th>Customer ID</th>
          <th>Name</th>
          <th>DOB</th>
          <th>Age</th>
          <th>Phone</th>
          <th>Address</th>
          <th>Gender</th>
          <th>Aadhaar Number</th>
          <th>PAN Number</th>
        </tr>
      </thead>
      <tbody>
          <tr key={profile.customerID}>
            <td>{profile.customerID}</td>
            <td>{profile.name}</td>
            <td>{profile.dob}</td>
            <td>{profile.age}</td>
            <td>{profile.phone}</td>
            <td>{profile.address}</td>
            <td>{profile.gender}</td>
            <td>{profile.aadhaar}</td>
            <td>{profile.panNumber}</td>
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


