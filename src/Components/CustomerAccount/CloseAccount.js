import React, { useState, useEffect } from 'react';
import './style.css'
import axios from 'axios'
export default function CloseAccount(){

var customerID = sessionStorage.getItem("CID");
const token = sessionStorage.getItem("Token");

  const [options,setOptions]= useState([])
  var [flag,setFlag] = useState(0);
  const [selectedOption, setSelectedOption] = useState(null);
  var [account1,setAccount1] = useState({
    accountNumber:null,
    ifscCode:null,
    accountType:null,
    balance:null,
    customerID:null
});
    
  const handleChange = (event) => {
    setSelectedOption(event.target.value);
    setAccountNumber(String(event.target.value))
        
  };

    var [accountNumber,setAccountNumber] = useState("");
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
          let filteredList = data.filter(obj => obj.status !== "Pending");
          setOptions(filteredList);
        });
    }
        func()
      },[])

    var Delete = async() =>{
        const validInput = validateInput({accountNumber})
        if(!validInput){
          alert("Account Number Cannot be null");
          return null;
        }

        const httpHeader = { 
            method:'DELETE',
            body: JSON.stringify(accountNumber),
            headers: {
                'accept' : 'text/plain',
                'Authorization': 'Bearer ' + token
            }
        };
        var confirmation = window.confirm("Are you sure want to delete the account?")
        if(!confirmation){
          return null;
        }
        try{
        let response = await axios.delete("https://localhost:7075/api/CustomerAccount/CloseAccount?key="+accountNumber,httpHeader)
        setAccount1(response.data);
        if(response.data.status="Account Closing Approved" && response.data.balance==0)
        {
          alert("Successfully closed the account");
        }
        else
        alert("Account Close Request Sent")
        }catch(err){
          alert(err.response.data);
        }

        
    };
    const flagmethod = (e) =>{
        e.preventDefault();
        if(flag==0){
            Delete()
            setFlag(1);
        }
        else
        setFlag(0);
    }


    return(
        <div style={{ width: '50%', backgroundColor: 'lightblue' }}>
            <div class="container mt-5">
            <div class="row"></div>
            <div class="col-md-12 mb-4">
                <div class="card custom-bg-color">

                    <div class="card-body">
                        <h1 class="card-title">Delete Account</h1>
                        <div class="form-group">
                        <div>
                                            <label htmlFor="input1">Account Number</label>
                                            <br/>
                                            <select value={selectedOption} onChange={handleChange} class="browser-default custom-select">
                                            <option value="">Select an option</option>
                                                {options.map((options) => (
                                                <option key={options.accountNumber} value={options.accountNumber}>
                                                    {options.accountNumber}&ensp;&ensp;Balance - {options.balance}
                                                </option>
                                                ))}
                                            </select>
                                        </div>
                            <br/>
                            <button type="submit" class="btn btn-success" onClick={flagmethod} >Close Account</button>
                        </div>
                        {flag==1? 
          <table className="table">
            <thead>
              <tr>
                <th>Account Number</th>
                <th>IFSC Code</th>
                <th>Account Type</th>
                <th>Balance</th>
              </tr>
            </thead>
            <tbody>
                <tr>
                <td>{account1.accountNumber}</td>
                  <td>{account1.ifscCode}</td>
                  <td>{account1.accountType}</td>
                  <td>{account1.balance}</td>

                </tr>
            </tbody>
          </table>
          :<p></p>}
                    </div>
                </div>
            </div>
            </div>
        </div>
    )
}