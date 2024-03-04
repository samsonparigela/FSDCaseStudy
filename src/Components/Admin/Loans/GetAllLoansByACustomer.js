import React, { useState, useEffect } from 'react';
import './style.css'
export default function GetAllLoansbyACustomer(){

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
        await fetch('https://localhost:7075/api/Customer/GetAll', {
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
    const [loans, setLoans] = useState([]);
    var [customerID,setCustomerID] =useState("");

    const validateInput = ({ customerID }) => {
      if (!customerID.trim()) {
        return false;
      }
      return true;
    };

    const fetchLoans = async () => {

      const validInput = validateInput({customerID})
      if(!validInput){
        alert("Customer ID cannot be null");
        return null;
      }


      try {
        const token = sessionStorage.getItem('Token');
        const response = await fetch('https://localhost:7075/api/BankEmpLoan/GetAllLoansbyACustomer?CID='+customerID, {
          method: 'GET',
          headers: {
            'Authorization': 'Bearer '+token,
            'Content-Type': 'application/json'
          }
        });

        if (response.ok) {
          const loansData = await response.json();
          setLoans(loansData);
        } else {
          console.error('Failed to fetch');
        }
      } catch (error) {
        console.error('Error fetching:', error);
      }
    }
  
  var flagmethod = (e) =>{
    if(flag===0){
      fetchLoans();
      setFlag(1);
    }
    
  else
  setFlag(0)
  }
  return (
    <div style={{ width: '50%', backgroundColor: 'lightblue' }}>


<div>
<div className="container mt-5">
                <div className="row">
                    <div className="col-md-12 mb-4">
                        <div className="card custom-bg-color">
                                    <div className="card-body">
                                        
      <h1>All Loans by Customer</h1>
      <div className="form-group">
      <div>
                                            <label htmlFor="input1">Customer ID</label>
                                            <br/>
                                            <select value={selectedOption} onChange={handleChange} className="browser-default custom-select">
                                            <option value="">Select an option</option>
                                                {options.map((options) => (
                                                <option key={options.customerID} value={options.customerID}>
                                                    {options.customerID}&ensp;&ensp;{options.name}&ensp;&ensp;{options.aadhaar}
                                                </option>
                                                ))}
                                            </select>
                                        </div>
                                </div>
      <button type="button" className="btn btn-success" data-toggle="button" 
      aria-pressed="false" onClick={flagmethod}>
      Get
      </button>
      {flag===1? 
      <table className="table">
        <thead>
          <tr>
            <th>Loan ID</th>
            <th>Customer ID</th>
            <th>Loan Amount</th>
            <th>Status</th>
            <th>Loan Purpose</th>
          </tr>
        </thead>
        <tbody>
        {loans.map(loans => (
            <tr key={loans.loanID}>
              <td>{loans.loanID}</td>
              <td>{loans.customerID}</td>
              <td>{loans.loanAmount}</td>
              <td>{loans.status}</td>
              <td>{loans.loanPurpose}</td>
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


