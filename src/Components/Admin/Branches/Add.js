import React, { useState, useEffect } from 'react';
import './style.css'

export default function AddBanks(){

  const [loans, setCustomers] = useState({});
  const token = sessionStorage.getItem("Token");
  const [options,setOptions]= useState([]);
  const [selectedOption, setSelectedOption] = useState("");
    
  const handleChange = (event) => {
    setSelectedOption(event.target.value);
    setBankID((event.target.value))
        
  };

  var [flag,setFlag] = useState(0);

  useEffect(() => {
    var func =async()=>{
        const response2 = await fetch('https://localhost:7075/api/BankAndBranch/GetAllBanks', {
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

  const [ifscCode, setIfscCode] = useState("");
  const [branchName, setBranchName] = useState("");
  const [city, setCity] = useState("");
  const [bankID, setBankID] = useState(0);
  var AdminID = sessionStorage.getItem("AdminID");

  var [flag,setFlag] = useState(0);

  var loanPolicy = {
    "ifscCode": ifscCode,
    "branchName": branchName,
    "city": city,
    "bankID": bankID
  }
    const fetchCustomers = async () => {
      try {


        const token = sessionStorage.getItem('Token');
        const response = await fetch('https://localhost:7075/api/BankAndBranch/AddBranch', {
          method: 'POST',
          'body':JSON.stringify(loanPolicy),
          headers: {
            'body':JSON.stringify(loanPolicy),
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
                                    <h1>Add Branch</h1>
                                    <div className="form-group">
      <div>

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

                                            <label htmlFor="input1">IFSC Code</label>
                            <input type="text" className="form-control" id="input1" placeholder="Enter IFSC Code"
                             value={ifscCode} onChange={(e)=>setIfscCode(e.target.value)}/>

<label htmlFor="input1">Branch Name</label>
                            <input type="text" className="form-control" id="input1" placeholder="Enter Branch Name"
                             value={branchName} onChange={(e)=>setBranchName(e.target.value)}/>

<label htmlFor="input1">City</label>
                            <input type="text" className="form-control" id="input1" placeholder="Enter City"
                             value={city} onChange={(e)=>setCity(e.target.value)}/>
                            
                                        </div>
                                        </div>
                                        

      <button type="button" className="btn btn-success" data-toggle="button" 
      aria-pressed="false" onClick={flagmethod}>
      Add Branch
      </button>
      {flag==1? 
      <table className="table">
        <thead>
          <tr>
            <th>IFSC Code</th>
            <th>Branch Name</th>
            <th>City</th>
          </tr>
        </thead>
        <tbody>
          <tr>
          <td>{loans.ifscCode}</td>
          <td>{loans.branchName}</td>
          <td>{loans.city}</td>
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



     
     
     
     
     
     
     