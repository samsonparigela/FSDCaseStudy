import React, { useState, useEffect } from 'react';
import TransactionTable from '../BEAccount/TransactionTable'
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
            if(isNaN(num)){
              alert("Enter Number");
              return null;
          }
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
        <div style={{ width: '100%'}}>
        <div className="container-fluid mt-5">
            <div className="row">
                <div className="col-md-12 mb-4">
                    <div className="card custom-bg-color">
                                <div className="card-body">
    
    <div>
          <h1>All your N Transactions</h1>
          <div className="form-group">
                            <label htmlFor="input1">N Value</label>
                            <input type="text" className="form-control" id="input1" placeholder="Enter Account Number"
                             value={num} onChange={(e)=>setNum(e.target.value)}/>
                             <br/>
          <button type="button" className="btn btn-success" data-toggle="button" 
          aria-pressed="false" onClick={flagmethod}>
          Get all your Transactions
          </button>
          </div>
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