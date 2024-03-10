import React, { useState, useEffect } from 'react';
import './style.css'

export default function GetCustomerForAccount(){

  var customerID = sessionStorage.getItem("BID");
  const token = sessionStorage.getItem("Token");

  const [options,setOptions]= useState([]);
  const [selectedOption, setSelectedOption] = useState("");
    
  const handleChange = (event) => {
    setSelectedOption(event.target.value);
    setAccountNumber(String(event.target.value))
        
  };

  var [flag,setFlag] = useState(0);

  useEffect(() => {
    var func =async()=>{
        await fetch('https://localhost:7075/api/BankEmpAccount/GetAllAccounts', {
        method: 'GET',
        headers: {
            'Authorization': 'Bearer '+token,
            body: JSON.stringify(customerID), 
            'Content-Type': 'application/json'
        }
    })
    .then(response => response.json())
    .then(data => {
      let filteredList = data;
    setOptions(filteredList);
    });

    }
    func()
  },[])
    const [profile, setCustomer] = useState({});
    const [accountNumber,setAccountNumber] = useState("");

    const validateInput = ({ accountNumber }) => {
      if (!accountNumber.trim()) {
        return false;
      }
      return true;
    };


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
        } else {
          console.error('customerData');
        }
      } catch (error) {
        console.error('Error fetching:', error);
      }
    }
  
  var flagmethod = (e) =>{
    if(flag===0){
      fetchCustomer();
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
                                        
      <h1>Customer for Account</h1>
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
      Get Customer
      </button>
      {flag===1? 
      <div className="table-responsive">
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
    </div>
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


