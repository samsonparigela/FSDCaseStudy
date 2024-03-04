import { useState } from "react";
import { useEffect } from "react";
import axios from 'axios'

export default function OpenAccount(){

    const [options,setOptions]= useState([])
    const [selectedOption, setSelectedOption] = useState("");
    
    const [subOptions,setSubOptions]= useState([])
    const [selectedSubOption, setSelectedSubOption] = useState("");

    const [bankID, setBankID] = useState(0);
    const [branchID, setBranchID] = useState(0);

    var customerID = sessionStorage.getItem("CID");
    const token = sessionStorage.getItem("Token");
    useEffect(()=>{
    const func = async() =>{
        const response2 = await axios.get('https://localhost:7075/api/CustomerAccount/View All Branches?BID='+bankID, {
                method: 'GET',
                headers: {
                    'Authorization': 'Bearer '+token,
                    body: JSON.stringify(customerID), 
                    'Content-Type': 'application/json'
                }
            })
            
            setSubOptions(response2.data);
    } 
    func()
},[selectedOption]);
    const handleChange = (event) => {
        event.preventDefault()
        let val = (event.target.value);
        setSelectedOption(val);
        setBankID(val);
        // var func =async()=>{
        //     const response2 = await fetch('https://localhost:7075/api/CustomerAccount/View All Branches?BID='+bankID, {
        //     method: 'GET',
        //     headers: {
        //         'Authorization': 'Bearer '+token,
        //         body: JSON.stringify(customerID), 
        //         'Content-Type': 'application/json'
        //     }
        // })
        // .then(response => response.json())
        // .then(data => {
        // setSubOptions(data);
        // });
    
        // }
        //     func();
      };


    const handleSubChange = (event) => {
        event.preventDefault();
        setSelectedSubOption(event.target.value);
        setIfscCode(event.target.value)   
    };


    var [flag,setFlag] = useState(0);
    

    var [accountNumber,setAccountNumber] = useState("");
    var [flag,setFlag] = useState(0);

    var [account1,setAccount1] = useState({
        accountNumber:null,
        ifscCode:null,
        accountType:null,
        balance:null,
        customerID:null
    });

    var [ifscCode,setIfscCode] = useState("");
    var [accountType,setAccountType] = useState("");
    var [balance,setBalance] = useState(0);

    useEffect(() => {
        var func =async()=>{
            const response = await axios('https://localhost:7075/api/CustomerAccount/View All Banks', {
            method: 'GET',
            headers: {
                'Authorization': 'Bearer '+token,
                body: JSON.stringify(customerID), 
                'Content-Type': 'application/json'
            }
        })
        
        setOptions(response.data);
    
        }
        func()
    },[])

    const validateInput = ({ accountNumber,ifscCode,accountType,balance }) => {
        if (!ifscCode.trim()) {
            alert(ifscCode+"ifsc")
            return false;
        }
        if (!accountType.trim()) {
            alert(accountType+"accountTYpe")
            return false;
        }
        if (balance==null) {
            alert(balance+"balance")
            return false;
        }
        return true;
      };

    var account = {}

    var Open = async() =>{

        const validInput = validateInput({ accountNumber,ifscCode,accountType,balance })
        if(!validInput){
            alert("One or more values are null");

            return null;
        }
        if(accountType!="Savings" && balance==0){
            alert("Only savings account initial balance can be 0. "+accountType+" should have minimum amount deposited.");
            return null;
        }

        account.accountNumber=0;
        account.ifscCode=ifscCode;
        account.accountType=accountType;
        account.balance=balance;
        account.customerID=customerID;


        const httpHeader = { 
            method:'POST',
            body: JSON.stringify(account),
            headers: {
                'Content-Type':'application/json',
                'accept' : 'text/plain',
                'Authorization': 'Bearer ' + token
                }
            };
            try{
                if(isNaN(balance)){
                    alert("Enter Number");
                    return null;
                }
            var response = 
            await fetch('https://localhost:7075/api/CustomerAccount/OpenAccount',httpHeader)
            .then(r=>r.json())
            .then(r=>setAccount1(r))
            }catch(error){
                alert(error)
            }

            alert("Account Open Request Sent")

    };
    
    const flagmethod = (e) =>{
        e.preventDefault();
        if(flag==0){
            Open()
            setFlag(1);
        }
        else
        setFlag(0);
    }
    
    return(
        <div style={{ width: '50%', backgroundColor: 'lightblue' }}>
            <div className="container mt-5">
                <div className="row">
                    <div className="col-md-12 mb-4">
                        <div className="card custom-bg-color">
                                <div className="card-body">
                                    <h1 className="card-title">Open Account</h1>
                                    <div className="form-group">
                                        <label htmlFor="category">BankID:</label>
                                            <select value={selectedOption} onChange={handleChange} className="browser-default custom-select">
                                            <option value="">Select an option</option>
                                                {options.map(options => (
                                                <option key={options.bankID} value={options.bankID}>
                                                    {options.bankID}&ensp;&ensp;{options.bankName}
                                                    </option>
                                                ))}
                                            </select>
                                            </div>
                                            

                                            {selectedOption && (
                                                <div>
                                                <label htmlFor="subOptions">Branches</label>
                                                <select value={selectedSubOption} onChange={handleSubChange} className="browser-default custom-select">
                                                <option value="">Select an option</option>
                                                    {subOptions.map(option => (
                                                    <option key={option.ifscCode} value={option.ifscCode}>
                                                        {option.ifscCode}&ensp;&ensp;{option.branchName}
                                                        </option>
                                                    ))}
                                                </select>
                                                </div>
                                            )}                                                        
                                                                
                                        <div className="form-group">
                                            <label htmlFor="accountType">Account Type</label>
                                            <input type="text" className="form-control" id="accountType" placeholder="Enter Account Type"
                                            value={accountType} onChange={(e)=>setAccountType(e.target.value)} autoComplete="on"/>
                                        </div>

                                        <div className="form-group">
                                            <label htmlFor="initialDeposit">Initial Deposit</label>
                                            <input type="number" className="form-control" id="initialDeposit" placeholder="Enter Initial Deposit Amount"
                                            value={balance} onChange={(e)=>setBalance(e.target.value)}/>
                                        </div>

                                        <button type="submit" className="btn btn-success" onClick={flagmethod}>Open Account</button>
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