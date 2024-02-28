import React, { useState, useEffect } from 'react';
import './style.css'
export default function CheckCreditWorthiness(){

    const [customerID, setCustomerID] = useState();
    var [worthy, setWorthy] = useState("");

    var bankEmpID = sessionStorage.getItem("BID");
    var [flag,setFlag] = useState(0);

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
                    body: JSON.stringify(customerID), // Include your authorization token
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
    if(flag==0){
        fetchLoans();
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
                                                    
                                <h1>Check Creditworthy</h1>
                                <div class="form-group">
                                    <label htmlFor="accountName">Customer ID</label>
                                    <input type="text" class="form-control"  id="accountNumber" placeholder="Enter CustomerID"
                                    value={customerID} onChange={(e)=>setCustomerID(e.target.value)}/>
                                </div>
                                <button type="button" class="btn btn-success" data-toggle="button" 
                                aria-pressed="false" onClick={flagmethod}>
                                Check
                                </button>
                                {flag==1 ? 
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


