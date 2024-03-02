import React, { useState, useEffect } from 'react';
export default function ViewAllBranches(){

  var customerID = sessionStorage.getItem("CID");
  const token = sessionStorage.getItem("Token");

  const [options,setOptions]= useState([])
  const [bankID, setBankID] = useState(1);
  const [selectedOption, setSelectedOption] = useState(null);
    
  const handleChange = (event) => {
    setBankID((event.target.value))
    setSelectedOption(event.target.value);
    
        
  };
    
        const [branches, setBranches] = useState([]);
        var [flag,setFlag] = useState(0);

        useEffect(() => {
          var func =async()=>{
              const response2 = await fetch('https://localhost:7075/api/CustomerAccount/View All Banks', {
              method: 'GET',
              headers: {
                  'Authorization': 'Bearer '+token,
                  body: JSON.stringify(customerID), 
                  'Content-Type': 'application/json'
              }
          })
          .then(response => response.json())
          .then(data => {
          setOptions(data);
          });
      
          }
          func()
        },[])
        const fetchBranches = async () => {
            const response = await fetch('https://localhost:7075/api/CustomerAccount/View All Branches?BID='+bankID, {
              method: 'GET',
              headers: {
                body: JSON.stringify(bankID), // Include your authorization token
                'Content-Type': 'application/json'
              }
            })
            .then(r=>r.json())
            .then(r=>setBranches(r));
           
        }
        const flagmethod = (e) =>{
          e.preventDefault()
          if(flag==0){
            fetchBranches()
            setFlag(1);
          }
          
        else
        setFlag(0)
        }

      return (
        <div style={{ width: '50%', backgroundColor: 'lightblue' }}>
        <div class="container mt-5">
            <div class="row">
                <div class="col-md-12 mb-4">
                    <div class="card custom-bg-color">
                                <div class="card-body">
    
    <div>
          <h1>All Branches Available</h1>
          <div class="form-group">
          <div>
                                            <label htmlFor="input1">Account Number</label>
                                            <br/>
                                            <select value={selectedOption} onChange={handleChange} class="browser-default custom-select">
                                            <option value="">Select an option</option>
                                                {options.map((options) => (
                                                <option key={options.bankID} value={options.bankID}>
                                                    {options.bankID}&ensp;&ensp;{options.bankName}
                                                </option>
                                                ))}
                                            </select>
                                        </div>
                                        <br/>
          <button type="button" class="btn btn-success" data-toggle="button" 
          aria-pressed="false" onClick={flagmethod}>
          Get all Branches
          </button>
          </div>
          {flag==1? 
          <table className="table">
            <thead>
              <tr>
                <th>IFSC Code</th>
                <th>Branch Name</th>
                <th>City</th>
                <th>Bank ID</th>
              </tr>
            </thead>
            <tbody>
            {branches.map(b => (
                <tr key={b.ifscCode}>
                  <td>{b.ifscCode}</td>
                  <td>{b.branchName}</td>
                  <td>{b.city}</td>
                  <td>{b.bankID}</td>
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