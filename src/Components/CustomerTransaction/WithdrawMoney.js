import axios from "axios";
import { data } from "jquery";
import { useEffect, useState } from "react";

export default function WithdrawMoney(){

    var customerID = sessionStorage.getItem("CID");
    const token = sessionStorage.getItem("Token");

    const [amount,setAmount] = useState(0)
    const [transacs,setTransacs] = useState({})
    const [accountNumber,setAccountNumber] = useState("");
    const [flag,setFlag] = useState(0);

    const [options,setOptions]= useState([])
    const [accountNumbers, setAccountNumbers] = useState([]);
    const [selectedOption, setSelectedOption] = useState("");


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
    
    const validateInput = ({ accountNumber,amount }) => {
        if (!accountNumber.trim()) {
          return false;
        }
        if (!amount) {
            return false;
          }
        return true;
      };
        const Withdraw = async()=>{
            const validInput = validateInput({accountNumber,amount})
            if(!validInput){
              alert("Account number or amount cannot be null");
              return null;
            }
            if(isNaN(amount)){
                alert("Enter Number");
                return null;
            }
            if(!window.confirm(`Do you want to withdraw ${amount} rupees from ${accountNumber} ?`)){
                return null;
            }
            try{
            const response = await axios.post("https://localhost:7075/api/CustomerTransaction/Withdraw Money?amount="+amount+"&accountID="+accountNumber,
            null,{
                headers:{
                    'Authorization':'Bearer '+token,
                    'Content-Type': 'application/json'
                }
            }
  
            );
            setTransacs(response.data)
        }catch(e){
            alert(e.response.data)
        }

            
        }


    function flagmethod(){
        if(flag==0){
            Withdraw();
            setFlag(1);
        }
        

        else
        setFlag(0);
    }

    return(
        <div>
            <div className="container mt-5">
                <div className="row">
                    <div className="col-md-12 mb-4">
                        <div className="card p-4 custom-bg-color">
                                    <div className="card-body"></div>
        <h1>Withdraw Money</h1>
            <div className="form-group">
            <div>
                                            <label htmlFor="input1">Account Number</label>
                                            <br/>
                                            <select value={selectedOption} onChange={handleChange} className="browser-default custom-select">
                                            <option value="">Select an option</option>
                                                {options.map((options) => (
                                                <option key={options.accountNumber} value={options.accountNumber}>
                                                    {options.accountNumber}&ensp;&ensp;Balance - {options.balance}
                                                </option>
                                                ))}
                                            </select>
                                        </div>
                
                <label htmlFor="input2">Amount</label>
                <input type="text" className="form-control" id="input2" placeholder="Enter Amount"
                value={amount} onChange={(e)=>setAmount(e.target.value)}/>
                <br/>
                <button type="button" className="btn btn-success" data-toggle="button" 
                aria-pressed="false" onClick={flagmethod}>
                Withdraw
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
                <p></p>
            }
        </div>
        </div>
            </div>
            </div>
            </div>
    )
}