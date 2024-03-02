import React, { useState, useEffect } from 'react';
import './style.css'

export default function GetByID(){

  const [Customers, setCustomers] = useState({});
  const [CustomerID, setCustomerID] = useState({});
  var AdminID = sessionStorage.getItem("AdminID");
  const token = sessionStorage.getItem("Token");
  const [options,setOptions]= useState([]);
  const [selectedOption, setSelectedOption] = useState(null);
    
  const handleChange = (event) => {
    setSelectedOption(event.target.value);
    setCustomerID(String(event.target.value))
        
  };

  var [flag,setFlag] = useState(0);

  useEffect(() => {
    var func =async()=>{
        const response2 = await fetch('https://localhost:7075/api/Admin/GetAllCustomers', {
        method: 'GET',
        headers: {
            'Authorization': 'Bearer '+token,
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

    var [flag,setFlag] = useState(0);
    
    const fetchCustomers = async () => {
      try {
        const token = sessionStorage.getItem('Token');
        const response = await fetch('https://localhost:7075/api/Admin/GetCustomerByID?ID='+CustomerID, {
          method: 'GET',
          headers: {
            'Authorization': 'Bearer '+token,
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
<div class="container mt-5">
                <div class="row">
                    <div class="col-md-12 mb-4">
                        <div class="card custom-bg-color">
                                    <div class="card-body">
                                    <h1>Customer By ID</h1>
                                    <div class="form-group">
      <div>
                                            <label htmlFor="input1">Customer ID</label>
                                            <br/>
                                            <select value={selectedOption} onChange={handleChange} class="browser-default custom-select">
                                            <option value="">Select an option</option>
                                                {options.map((options) => (
                                                <option key={options.customerID} value={options.customerID}>
                                                    {options.customerID}&ensp;&ensp;{options.name}&ensp;&ensp;{options.aadhaar}
                                                </option>
                                                ))}
                                            </select>
                                        </div>
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



     
     
     
     
     
     
     