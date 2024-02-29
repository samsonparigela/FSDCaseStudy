import React, { useState, useEffect } from 'react';
import pic1 from './j4.jpeg'
export default function TransacsToAcc(){
  
  var customerID = sessionStorage.getItem("CID");
  const token = sessionStorage.getItem("Token");

  const [options,setOptions]= useState([])
  const [accountNumbers, setAccountNumbers] = useState([]);
  const [selectedOption, setSelectedOption] = useState(null);
    
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
        <div style={{ width: '100%', backgroundColor: 'lightblue' }}>
        <div class="container mt-5">
            <div class="row">
                <div class="col-md-12 mb-4">
                    <div class="card custom-bg-color">
                                <div class="card-body">
    
    <div>
          <h1>All Transactions To Account</h1>
          <div class="form-group">
                            <div>
                                            <label htmlFor="input1">Account Number</label>
                                            <br/>
                                            <select value={selectedOption} onChange={handleChange} class="browser-default custom-select">
                                            <option value="">Select an option</option>
                                                {options.map((options) => (
                                                <option key={options.accountNumber} value={options.accountNumber}>
                                                    {options.accountNumber}
                                                </option>
                                                ))}
                                            </select>
                                        </div>
                                        <br/>
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
                {/* Add more table headers as needed */}
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