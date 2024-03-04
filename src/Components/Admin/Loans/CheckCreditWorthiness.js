import React, { useState, useEffect } from 'react';
import './style.css'
export default function CheckCreditWorthiness(){

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

    const [customerID, setCustomerID] = useState();
    var [worthy, setWorthy] = useState("");


    const validateInput = ({ customerID }) => {
        if (customerID==null) {
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
            const response = await fetch('https://localhost:7075/api/BankEmpLoan/CheckCustomerCreditworthiness?CID='+customerID, {
                method: 'GET',
                headers: {
                    'Authorization': 'Bearer '+token,
                    'Content-Type': 'application/json'
                }
                });

            if (response.ok) {
            const loansData = await response.json();
            console.log(loansData);
            setWorthy(loansData);
            } 
            else {
            console.error('Failed to fetch');
            }
        }catch (error) {
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
            <div className="container mt-5">
                <div className="row">
                    <div className="col-md-12 mb-4">
                        <div className="card custom-bg-color">
                            <div className="card-body">
                                                    
                                <h1>Check Creditworthy</h1>
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
                                Check
                                </button>
                                {flag===1 ? 
                                <table className="table">
                                    <thead>
                                    <tr>
                                        <th>Credit Worthy</th>
                                    </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                        <td>{String(worthy)}</td>
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
  );
}


