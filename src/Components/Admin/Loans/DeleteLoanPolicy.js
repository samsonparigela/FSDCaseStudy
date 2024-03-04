import React, { useState, useEffect } from 'react';
import './style.css'

export default function DeleteLoanPolicy(){

  const [Customers, setCustomers] = useState({});
  const [CustomerID, setCustomerID] = useState({});

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
        await fetch('https://localhost:7075/api/BankEmpLoan/GetAllLoanPolicies', {
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


    
    const fetchCustomers = async () => {
      try {
        const token = sessionStorage.getItem('Token');
        const response = await fetch('https://localhost:7075/api/Admin/DeleteLoanPolicies?ID='+CustomerID, {
          method: 'Delete',
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
    if(flag===0){
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
                                    <h1>Delete Loan Policy</h1>
                                    <div className="form-group">
      <div>
                                            <label htmlFor="input1">Loan Policy ID</label>
                                            <br/>
                                            <select value={selectedOption} onChange={handleChange} className="browser-default custom-select">
                                            <option value="">Select an option</option>
                                                {options.map((options) => (
                                                <option key={options.loanPolicyID} value={options.loanPolicyID}>
                                                    {options.loanPolicyID}&ensp;&ensp;{options.tenureInMonths} Months&ensp;&ensp;{options.interest}% Interest
                                                </option>
                                                ))}
                                            </select>
                                        </div>
                                        </div>
                                        

      <button type="button" className="btn btn-success" data-toggle="button" 
      aria-pressed="false" onClick={flagmethod}>
      Delete Policy
      </button>
      {flag===1? 
      <table className="table">
        <thead>
          <tr>
            <th>Loan Policy ID</th>
            <th>Loan tenureInMonths</th>
            <th>Interest</th>
          </tr>
        </thead>
        <tbody>
            <tr key={Customers.loanPolicyID}>
              <td>{Customers.loanPolicyID}</td>
              <td>{Customers.tenureInMonths}</td>
              <td>{Customers.interest}</td>
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



     
     
     
     
     
     
     