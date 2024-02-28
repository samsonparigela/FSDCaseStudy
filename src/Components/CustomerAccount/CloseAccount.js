import pic1 from './j4.jpeg'
import { useState } from "react";
import './style.css'
export default function CloseAccount(){

    var [accountNumber,setAccountNumber] = useState("");
    const validateInput = ({ accountNumber }) => {
        if (!accountNumber.trim()) {
          return false;
        }
        return true;
      };
    var Delete = (e) =>{
        e.preventDefault();
        const validInput = validateInput({accountNumber})
        if(!validInput){
          alert("Account Number Cannot be null");
          return null;
        }
        const token = sessionStorage.getItem('Token');

        const httpHeader = { 
            method:'DELETE',
            body: JSON.stringify(accountNumber),
            headers: {
                'accept' : 'text/plain',
                'Authorization': 'Bearer ' + token
            }
        };
        console.log(httpHeader);
        fetch("https://localhost:7075/api/CustomerAccount/CloseAccount?key="+accountNumber,httpHeader)
        .then(r=>r.json())
        .then(r=>console.log(r))
        .catch(r=>console.log(r))

        alert("Account Close Request Sent")
    };



    return(
        <div style={{ width: '50%', backgroundColor: 'lightblue' }}>
            <div class="container mt-5">
            <div class="row"></div>
            <div class="col-md-12 mb-4">
                <div class="card custom-bg-color">

                    <div class="card-body">
                        <h1 class="card-title">Delete Account</h1>
                        <div class="form-group">
                            <label htmlFor="input3">Account Number</label>
                            <input type="text" class="form-control" id="input3" placeholder="Enter Account Number"
                             value={accountNumber} onChange={(e)=>setAccountNumber(e.target.value)}/>
                            <br/>
                            <button type="submit" class="btn btn-success" onClick={Delete} >Close Account</button>
                        </div>
                    </div>
                </div>
            </div>
            </div>
        </div>
    )
}