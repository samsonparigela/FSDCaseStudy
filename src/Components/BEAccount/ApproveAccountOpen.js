import { useEffect, useState } from "react";
import './style.css'
export default function ApproveAccountOpen(){
    var customerID = sessionStorage.getItem("BID");
    const token = sessionStorage.getItem("Token");
  
    const [options,setOptions]= useState([]);
    const [selectedOption, setSelectedOption] = useState("");
      
    const handleChange = (event) => {
      setSelectedOption(event.target.value);
      setAccountNumber(String(event.target.value))
          
    };
  
    useEffect(() => {
      var func =async()=>{
          const response2 = await fetch('https://localhost:7075/api/BankEmpAccount/GetAllAccounts', {
          method: 'GET',
          headers: {
              'Authorization': 'Bearer '+token,
              body: JSON.stringify(customerID), 
              'Content-Type': 'application/json'
          }
      })
      .then(response => response.json())
      .then(data => {
        let filteredList = data.filter(obj => obj.status === "Pending");
      setOptions(filteredList);
      });
  
      }
      func()
    },[])
    var [accountNumber,setAccountNumber] = useState("");
    var [status,setStatus] = useState({});

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


            const token = sessionStorage.getItem('Token');
            const httpHeader = { 
                method:'GET',
                headers: {
                    'Content-Type':'application/json',
                    'accept' : 'text/plain',
                    'Authorization': 'Bearer ' + token
                }
            };
            fetch('https://localhost:7075/api/BankEmpAccount/ApproveAccountOpening?AID='+accountNumber,httpHeader)
            .then(r=>r.json())
            .then(r=>setStatus(r))
            .catch(r=>console.log(r))

            alert("Account Opened")

    };
    return(
        <div style={{ width: '100'}}>

            <div className="container mt-5">
                <div className="row">
                    <div className="col-md-12 mb-4">
                        <div className="card custom-bg-color">
                                    <div className="card-body">
                                        <h5 className="card-title">Approve Account</h5>
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
                                        </div>

                                        <button type="submit" className="btn btn-primary" onClick={Open}>Open Account</button>
                                        <h3>{status.status}</h3>
                            </div>
                    </div>
                </div>

    </div>

        </div>
        </div>
    )
}