import React, { useState, useEffect } from 'react';
import './style.css'

export default function GetAll(){

    const [Customers, setCustomers] = useState([]);
    var AdminID = sessionStorage.getItem("AdminID");
    var [flag,setFlag] = useState(0);
    
    const fetchCustomers = async () => {
      try {
        const token = sessionStorage.getItem('Token');
        const response = await fetch('https://localhost:7075/api/Admin/GetAllCustomers', {
          method: 'GET',
          headers: {
            'Authorization': 'Bearer '+token,
            body: JSON.stringify(AdminID), // Include your authorization token
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
<div className="container mt-5">
                <div className="row">
                    <div className="col-md-12 mb-4">
                        <div className="card custom-bg-color">
                                    <div className="card-body">
                                        
      <h1>All Customers</h1>
      <button type="button" className="btn btn-success" data-toggle="button" 
      aria-pressed="false" onClick={flagmethod}>
      Get all Customers
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
        {Customers.map(Customers => (
            <tr key={Customers.customerID}>
              <td>{Customers.customerID}</td>
              <td>{Customers.name}</td>
              <td>{Customers.dob}</td>
              <td>{Customers.age}</td>
              <td>{Customers.phone}</td>
              <td>{Customers.address}</td>
              <td>{Customers.gender}</td>
              <td>{Customers.aadhaar}</td>
              <td>{Customers.panNumber}</td>
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



     
     
     
     
     
     
     