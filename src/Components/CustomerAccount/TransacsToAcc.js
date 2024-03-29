import React, { useState, useEffect } from 'react';
import TransactionTable from '../BEAccount/TransactionTable';
export default function TransacsToAcc(){
  
  var customerID = sessionStorage.getItem("CID");
  const token = sessionStorage.getItem("Token");

  const [options,setOptions]= useState([])
  const [accountNumbers, setAccountNumbers] = useState([]);
  const [selectedOption, setSelectedOption] = useState("");
    
  const handleChange = (event) => {
    setSelectedOption(event.target.value);
    setAccountNumber(String(event.target.value))
        
  };
    
        const [transacs, setTransacs] = useState([]);
        const [accountNumber,setAccountNumber] = useState(0);
        var customerID = sessionStorage.getItem("CID");
        var [flag,setFlag] = useState(0);

        const validateInput = ({ accountNumber }) => {
          if (!accountNumber.trim()) {
            return false;
          }
          return true;
        };

        useEffect(() => {
          var func =async()=>{
              const response2 = await fetch('https://localhost:7075/api/CustomerAccount/View All your Accounts?ID='+customerID, {
              method: 'GET',
              headers: {
                  'Authorization': 'Bearer '+token,
                  body: JSON.stringify(customerID), 
                  'Content-Type': 'application/json'
              }
          })
          .then(response => response.json())
          .then(data => {
            let filteredList = data.filter(obj => obj.status !== "Pending" && obj.status !== "Account Closing Approved");
            setOptions(filteredList);;
          });
      
          for (let i = 0; i < options.length; i++) {
              const element = options[i].accountNumber;
              const jsonelement = {
                  label : element,
                  value : element
              }
              if (!accountNumbers.includes(element)) {
                  accountNumbers.push(jsonelement)
                  setAccountNumbers(accountNumbers)
                }
              }
          }
          func()
        },[])

        const fetchTransacs = async () => {
          const validInput = validateInput({accountNumber})
          if(!validInput){
            alert("Input Cannot be null");
            return null;
          }
          try {
            const response = await fetch('https://localhost:7075/api/CustomerAccount/View all Transactions to an Account?AID='+accountNumber+'&CID='+customerID, {
              method: 'GET',
              headers: {
                'Authorization': 'Bearer '+token,
                body: JSON.stringify(accountNumber), // Include your authorization token
                'Content-Type': 'application/json'
              }
            });
    
            if (response.ok) {
              const ordersData = await response.json();
              setTransacs(ordersData);
            } else {
              console.error('Failed to fetch orders');
            }
          } catch (error) {
            console.error('Error fetching orders:', error);
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
          <h1>All Transactions To Account</h1>
          <div className="form-group">
                            <div>
                                            <label htmlFor="input1">Account Number</label>
                                            <br/>
                                            <select value={selectedOption} onChange={handleChange} className="browser-default custom-select">
                                            <option value="">Select an option</option>
                                                {options.map((options) => (
                                                <option key={options.accountNumber} value={options.accountNumber}>
                                                    {options.accountNumber}
                                                </option>
                                                ))}
                                            </select>
                                        </div>
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