import { useState } from "react";
import pic1 from './j2.jpg'
export default function OpenAccount(){
    
    var [accountNumber,setAccountNumber] = useState("");
    var [ifscCode,setIfscCode] = useState("");
    var [accountType,setAccountType] = useState("");
    var [balance,setBalance] = useState(0);
    var customerID = sessionStorage.getItem("CID");

    const validateInput = ({ accountNumber,ifscCode,accountType,balance }) => {
        if (!accountNumber.trim()) {
          return false;
        }
        if (!ifscCode.trim()) {
            return false;
        }
        if (!accountType.trim()) {
            return false;
        }
        if (balance==null) {
            return false;
        }
        return true;
      };

    var account = {}

    var Open = (e) =>{
        e.preventDefault();

        const validInput = validateInput({ accountNumber,ifscCode,accountType,balance })
      if(!validInput){
        alert("One or more values are null");
        return null;
      }
      if(accountType!="Savings" && balance==0){
        alert("Only savings account initial balance can be 0. "+accountType+" should have minimum amount deposited.");
        return null;
      }

        account.accountNumber=accountNumber;
        account.ifscCode=ifscCode;
        account.accountType=accountType;
        account.balance=balance;
        account.customerID=customerID;

        console.log(account);

            const token = sessionStorage.getItem('Token');
            const httpHeader = { 
                method:'POST',
                body: JSON.stringify(account),
                headers: {
                    'Content-Type':'application/json',
                    'accept' : 'text/plain',
                    'Authorization': 'Bearer ' + token
                }
            };
            console.log(httpHeader);
            fetch('https://localhost:7075/api/CustomerAccount/OpenAccount',httpHeader)
            .then(r=>r.json())
            .then(r=>console.log(r))
            .catch(r=>console.log(r))

            alert("Account Open Request Sent")

    };
    return(
        <div style={{ width: '50%', backgroundColor: 'lightblue' }}>
            <div class="container mt-5">
                <div class="row">
                    <div class="col-md-12 mb-4">
                        <div class="card custom-bg-color">
                                    <div class="card-body">
                                        <h1 class="card-title">Open Account</h1>
                                        <div class="form-group">
                                            <label htmlFor="accountNumber">Account Number</label>
                                            <input type="text" class="form-control"  id="accountNumber" placeholder="Enter your Account Number"
                                            value={accountNumber} onChange={(e)=>setAccountNumber(e.target.value)}/>
                                        </div>
                                                        
                                        <div class="form-group">
                                            <label htmlFor="IFSCCode">IFSC Code</label>
                                            <input type="text" class="form-control" id="IFSCCode" placeholder="Enter IFSC Code"
                                            value={ifscCode} onChange={(e)=>setIfscCode(e.target.value)} autoComplete="on"/>
                                        </div>
                                                                
                                        <div class="form-group">
                                            <label htmlFor="accountType">Account Type</label>
                                            <input type="text" class="form-control" id="accountType" placeholder="Enter Account Type"
                                            value={accountType} onChange={(e)=>setAccountType(e.target.value)} autoComplete="on"/>
                                        </div>

                                        <div class="form-group">
                                            <label htmlFor="initialDeposit">Initial Deposit</label>
                                            <input type="number" class="form-control" id="initialDeposit" placeholder="Enter Initial Deposit Amount"
                                            value={balance} onChange={(e)=>setBalance(e.target.value)}/>
                                        </div>

                                        <button type="submit" class="btn btn-success" onClick={Open}>Open Account</button>
                            </div>
                    </div>
                </div>

    </div>

        </div>
        </div>
    )
}