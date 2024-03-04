import React, { useState, useEffect } from 'react';
import './style.css'

export default function Update(){
    const [Customers, setCustomers] = useState({});
    const [CustomerID, setCustomerID] = useState({});
    var AdminID = sessionStorage.getItem("AdminID");
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
  var AdminID = sessionStorage.getItem("AdminID");

  var [flag,setFlag] = useState(0);

    var [bankName,setBankName] = useState("");
    var [bankID,setBankID] = useState(0);
    var bank = {
        "bankName": bankName,
        "id": bankID
      }
    const fetchCustomers = async () => {
      try {
        const token = sessionStorage.getItem('Token');
        const response = await fetch('https://localhost:7075/api/Admin/UpdateBank', {
          method: 'PUT',
          'body':JSON.stringify(bank),
          headers: {
            'body':JSON.stringify(bank),
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
                                    <h1>Update Bank</h1>
                                    <div className="form-group">
      <div>
      <div>
                                            <label htmlFor="input1">Bank ID</label>
                                            <br/>
                                            <select value={selectedOption} onChange={handleChange} className="browser-default custom-select">
                                            <option value="">Select an option</option>
                                                {options.map((options) => (
                                                <option key={options.bankID} value={options.bankID}>
                                                    {options.bankID}&ensp;&ensp;{options.bankName}
                                                </option>
                                                ))}
                                            </select>
                                        </div>
                             <label htmlFor="input1">Bank Name</label>
                            <input type="text" className="form-control" id="input1" placeholder="Enter Bank Name"
                             value={bankName} onChange={(e)=>setBankName(e.target.value)}/>
                             <br/>
                                        </div>
                                        </div>
                                        

      <button type="button" className="btn btn-success" data-toggle="button" 
      aria-pressed="false" onClick={flagmethod}>
      Update Bank
      </button>
      {flag==1? 
      <table className="table">
        <thead>
          <tr>
            <th>Bank ID</th>
            <th>Bank Name</th>
          </tr>
        </thead>
        <tbody>
            <tr key={Customers.bankID}>
              <td>{bankID}</td>
              <td>{Customers.bankName}</td>
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



     
     
     
     
     
     
     