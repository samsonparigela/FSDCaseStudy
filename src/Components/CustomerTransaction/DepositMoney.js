import { useEffect, useState } from "react";
import 'bootstrap/dist/css/bootstrap.min.css';
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap/dist/js/bootstrap.bundle.min.js";
export default function DepositMoney(){

    var customerID = sessionStorage.getItem("CID");
    const token = sessionStorage.getItem("Token");

    const [amount,setAmount] = useState(0)
    const [transacs,setTransacs] = useState({})
    const [accountNumber,setAccountNumber] = useState("");
    const [flag,setFlag] = useState(0);
    
    const [options,setOptions]= useState([])
    const [accountNumbers, setAccountNumbers] = useState([]);
    const [selectedOption, setSelectedOption] = useState(null);

    const handleChange = (event) => {
        setSelectedOption(event.target.value);
        setAccountNumber(String(event.target.value))
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
        setOptions(filteredList);
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

    

    const validateInput = ({ accountNumber,amount }) => {
        if (!accountNumber.trim()) {
          return false;
        }
        if (!amount) {
            return false;
          }
        return true;
      };

      
    const Deposit = async()=>{
        const validInput = validateInput({accountNumber,amount})
        if(!validInput){
            alert("Account number or amount cannot be null");
            return null;
        }
        if(isNaN(amount)){
            alert("Enter Number");
            return null;
        }
        const response = await fetch("https://localhost:7075/api/CustomerTransaction/Deposit Money?accountNumber="+accountNumber+"&amount="+amount,{
            method:'POST',
            headers:{
                'Authorization':'Bearer '+token,
                body: JSON.stringify(customerID), // Include your authorization token
                'Content-Type': 'application/json'
                }
            });

            if(response.ok){
                const data = await response.json();
                setTransacs(data)
            }
        }


    function flagmethod(){
        if(flag==0)
        {
            Deposit();
            setFlag(1);
        }
        else
        setFlag(0);
       
    }

    return(
        <div>
            <div class="container mt-5">
                <div class="row">
                    <div class="col-md-12 mb-4">
                        <div class="card p-4 custom-bg-color">
                            <div class="card-body">
                                <h1>Deposit Money</h1>
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
            
                                        <label htmlFor="input2">Amount</label>
                                        <input type="text" class="form-control" id="input2" placeholder="Enter Amount"
                                        value={amount} onChange={(e)=>setAmount(e.target.value)}/>
                                        <br/>
                                        <button type="button" class="btn btn-success" data-toggle="button" 
                                        aria-pressed="false" onClick={flagmethod}>
                                        Deposit
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
                                                
                                                <tr key={transacs.transactionID}>
                                                    <td>{transacs.transactionID}</td>
                                                    <td>{transacs.status}</td>
                                                    <td>{transacs.sAccountID}</td>
                                                    <td>{transacs.beneficiaryAccountNumber}</td>
                                                    <td>{transacs.transactionType}</td>
                                                    <td>{transacs.amount}</td>
                                                    <td>{transacs.description}</td>
                                                    <td>{transacs.transactionDate}</td>
                                                </tr>
                                            </tbody>
                                        </table>      
                                    :
                                    <p></p>}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}