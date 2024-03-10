import React, { useState, useEffect } from 'react';
import './style.css'
import TransactionTable from './TransactionTable'; 
export default function GetTopTransacs(){

    const [transacs, setTransacs] = useState([]);
    var [flag,setFlag] = useState(0);
    var customerID = sessionStorage.getItem("BID");
    useEffect(() => {
    const fetchTransacs = async () => {
      try {
        const token = sessionStorage.getItem('Token');
        const response = await fetch('https://localhost:7075/api/BankEmpAccount/ViewTransactionsWith5HighestAmount', {
          method: 'GET',
          headers: {
            'Authorization': 'Bearer '+token,
            body: JSON.stringify(customerID), // Include your authorization token
            'Content-Type': 'application/json'
          }
        });

        if (response.ok) {
          const TransacsData = await response.json();
          setTransacs(TransacsData);
        } else {
          console.error('Failed to fetch');
        }
      } catch (error) {
        console.error('Error fetching:', error);
      }
    }
    fetchTransacs();
  },[]);

  var flagmethod = (e) =>{
    if(flag==0)
    setFlag(1)
  else
  setFlag(0)
  }
  return (
    <div style={{ width: '100%'}}>



<div>
<div className="container mt-5">
                <div className="row">
                    <div className="col-md-12 mb-4">
                        <div className="card custom-bg-color">
                                    <div className="card-body">
                                        
      <h1>Highest amount Transactions</h1>
      <button type="button" className="btn btn-success" data-toggle="button" 
      aria-pressed="false" onClick={flagmethod}>
      Get all Transactions
      </button>
      {flag==1? 
      <TransactionTable transacs={transacs}/>
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


