import React, { useState, useEffect } from 'react';
import './style.css'

export default function AddBanks(){

  const [Customers, setCustomers] = useState({});
  const [CustomerID, setCustomerID] = useState({});
  var AdminID = sessionStorage.getItem("AdminID");
  const token = sessionStorage.getItem("Token");

  var [flag,setFlag] = useState(0);

    var [flag,setFlag] = useState(0);
    var [bankName,setBankName] = useState("");
    var loanPolicy = {
        "bankName":bankName
    }
    const fetchCustomers = async () => {
      try {
        const token = sessionStorage.getItem('Token');
        const response = await fetch('https://localhost:7075/api/Admin/AddBank', {
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
                                    <h1>Add Bank</h1>
                                    <div className="form-group">
      <div>
                                            <label htmlFor="input1">Bank Name</label>
                            <input type="text" className="form-control" id="input1" placeholder="Enter Bank Name"
                             value={bankName} onChange={(e)=>setBankName(e.target.value)}/>
                            
                                        </div>
                                        </div>
                                        

      <button type="button" className="btn btn-success" data-toggle="button" 
      aria-pressed="false" onClick={flagmethod}>
      Add Bank
      </button>
      {flag==1? 
      <table className="table">
        <thead>
          <tr>
            <th>Bank Name</th>
          </tr>
        </thead>
        <tbody>
            <tr key={Customers.bankID}>
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



     
     
     
     
     
     
     