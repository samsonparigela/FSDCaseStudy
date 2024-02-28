import { useState } from "react";
import './style.css'
export default function ApproveAccountOpen(){
    
    var [accountNumber,setAccountNumber] = useState("");
    var [status,setStatus] = useState({});
    var customerID = sessionStorage.getItem("BID");

    const validateInput = ({accountNumber}) =>{
        if(!accountNumber.trim()){
            return false;
        }
        return true;
    }

    var account = {}

    var Open = (e) =>{
        e.preventDefault();

        const validInput = validateInput({accountNumber});
        if(!validInput){
            alert("Account number cannot be null");
            return null;
        }


        console.log(account);

            const token = sessionStorage.getItem('Token');
            const httpHeader = { 
                method:'GET',
                headers: {
                    'Content-Type':'application/json',
                    'accept' : 'text/plain',
                    'Authorization': 'Bearer ' + token
                }
            };
            console.log(httpHeader);
            fetch('https://localhost:7075/api/BankEmpAccount/ApproveAccountOpening?AID='+accountNumber,httpHeader)
            .then(r=>r.json())
            .then(r=>setStatus(r))
            .catch(r=>console.log(r))

            alert("Account Opened")

    };
    return(
        <div style={{ width: '50%', backgroundColor: 'lightblue' }}>

            <div class="container mt-5">
                <div class="row">
                    <div class="col-md-12 mb-4">
                        <div class="card custom-bg-color">
                                    <div class="card-body">
                                        <h5 class="card-title">Approve Account</h5>
                                        <div class="form-group">
                                            <label htmlFor="accountName">Account Number</label>
                                            <input type="text" class="form-control"  id="accountNumber" placeholder="Enter your Account Number"
                                            value={accountNumber} onChange={(e)=>setAccountNumber(e.target.value)}/>
                                        </div>

                                        <button type="submit" class="btn btn-primary" onClick={Open}>Open Account</button>
                                        <h3>{status.status}</h3>
                            </div>
                    </div>
                </div>

    </div>

        </div>
        </div>
    )
}