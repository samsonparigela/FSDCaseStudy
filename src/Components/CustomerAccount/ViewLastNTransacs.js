import React, { useState, useEffect } from 'react';
import pic1 from './j4.jpeg'
export default function ViewLastNTransacs(){

    
        const [transacs, setTransacs] = useState([]);
        const [error, setError] = useState([]);
        const [num,setNum] = useState(0);
        var customerID = sessionStorage.getItem("CID");
        var [flag,setFlag] = useState(0);

        const validateInput = ({ num }) => {
          if (!num.trim()) {
            return false;
          }
          return true;
        };

        const fetchTransacs = async () => {
          const validInput = validateInput({num})
          if(!validInput){
            alert("Input Cannot be null");
            return null;
          }
          try {
            const token = sessionStorage.getItem('Token');
            const response = await fetch('https://localhost:7075/api/CustomerAccount/View Last N Transactions?ID='+customerID+'&n=' + num, {
              method: 'GET',
              headers: {
                'Authorization': 'Bearer '+token,
                body: JSON.stringify(num), // Include your authorization token
                'Content-Type': 'application/json'
              }
            });
            if (response.ok) {
              const ordersData = await response.json();
              setTransacs(ordersData);
            } else {
             
              console.error('Failed to fetch');
            }
          } catch (error) {
            setError(error);
            console.error('Error fetching orders:'+ error);
          }
        }
        const flagmethod = (e) =>{
          e.preventDefault()
          if(flag==0){
            fetchTransacs()
            setFlag(1);
          }
          
        else
        setFlag(0)
        }
      
      return (
        <div style={{ width: '100%', backgroundColor: 'lightblue' }}>
        <div class="container mt-5">
            <div class="row">
                <div class="col-md-12 mb-4">
                    <div class="card custom-bg-color">
                                <div class="card-body">
    
    <div>
          <h1>All your N Transactions</h1>
          <div class="form-group">
                            <label htmlFor="input1">N Value</label>
                            <input type="text" class="form-control" id="input1" placeholder="Enter Account Number"
                             value={num} onChange={(e)=>setNum(e.target.value)}/>
          <button type="button" class="btn btn-success" data-toggle="button" 
          aria-pressed="false" onClick={flagmethod}>
          Get all your Transactions
          </button>
          </div>
          {flag==1? 
          <table className="table">
            <thead>
              <tr>
                <th>TransactionID</th>
                <th>Status</th>
                <th>Source Account Number</th>
                <th>Destination Account Number</th>
                <th>Transaction Type</th>
                <th>Amount</th>
                <th>Description</th>
                <th>Transaction Date</th>
              </tr>
            </thead>
            <tbody>
            {transacs.map(tran => (
                <tr key={tran.transactionID}>
                  <td>{tran.transactionID}</td>
                  <td>{tran.status}</td>
                  <td>{tran.sAccountID}</td>
                  <td>{tran.beneficiaryAccountNumber}</td>
                  <td>{tran.transactionType}</td>
                  <td>{tran.amount}</td>
                  <td>{tran.description}</td>
                  <td>{tran.transactionDate}</td>
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