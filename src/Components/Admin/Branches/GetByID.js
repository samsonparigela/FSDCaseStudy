import React, { useState, useEffect } from 'react';
import './style.css'

export default function GetByID(){

  const [loans, setCustomers] = useState([]);
  const [CustomerID, setCustomerID] = useState({});
  var AdminID = sessionStorage.getItem("AdminID");
  const token = sessionStorage.getItem("Token");
  const [options,setOptions]= useState([]);
  const [selectedOption, setSelectedOption] = useState("");
    
  const handleChange = (event) => {
    setSelectedOption(event.target.value);
    setCustomerID(String(event.target.value))
        
  };

  var [flag,setFlag] = useState(0);

  useEffect(() => {
    var func =async()=>{
        const response2 = await fetch('https://localhost:7075/api/Admin/GetAllBanks', {
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
        const response = await fetch('https://localhost:7075/api/Admin/GetAllBranchesByBankID?ID='+CustomerID, {
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
<div className="container mt-5">
                <div className="row">
                    <div className="col-md-12 mb-4">
                        <div className="card custom-bg-color">
                                    <div className="card-body">
                                    <h1>Branch By ID</h1>
                                    <div className="form-group">
      <div>
                                            <label htmlFor="input1">Bank ID</label>
                                            <br/>
                                            <select value={selectedOption} onChange={handleChange} className="browser-default custom-select">
                                            <option value="">Select an option</option>
                                                {options.map((options) => (
                                                <option key={options.bankID} value={options.bankID}>
                                                    {options.bankID} &ensp;&ensp;{options.bankName}
                                                </option>
                                                ))}
                                            </select>
                                        </div>
                                        </div>
                                        
      <button type="button" className="btn btn-success" data-toggle="button" 
      aria-pressed="false" onClick={flagmethod}>
      Get Branches
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
        {loans.map(loans => (
            <tr key={loans.ifscCode}>
              <td>{loans.bankID}</td>
              <td>{loans.branchName}</td>
              <td>{loans.ifscCode}</td>
              <td>{loans.city}</td>
            </tr>
        ))}
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



     
     
     
     
     
     
     