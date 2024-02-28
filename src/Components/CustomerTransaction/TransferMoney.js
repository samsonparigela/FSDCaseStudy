import { useEffect, useState } from "react";

export default function TransferMoney(){

    var customerID = sessionStorage.getItem("CID");
    const token = sessionStorage.getItem("Token");

    const [amount,setAmount] = useState(0)
    const [transacs,setTransacs] = useState({})
    const [accountNumber1,setAccountNumber1] = useState("");
    const [accountNumber2,setAccountNumber2] = useState("");

    const [flag,setFlag] = useState(0);

    const [options1,setOptions1]= useState([])
    const [selectedOption1, setSelectedOption1] = useState(null);

    const [options2,setOptions2]= useState([])
    const [selectedOption2, setSelectedOption2] = useState(null);

    const handleChange1 = (event) => {
        setAccountNumber1(String(event.target.value))
        setSelectedOption1(event.target.value);
        
    };

    const handleChange2 = (event) => {
        setAccountNumber2(String(event.target.value))
        setSelectedOption2(event.target.value);
        
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
        setOptions1(data);
        });
        }
        func()
    },[])


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
        setOptions2(data);
        });

        }
        func()
    },[])

    const validateInput = ({ accountNumber,amount }) => {
        if ((accountNumber=="")) {
          return false;
        }
        if (!amount) {
            return false;
        }
        return true;
      };

        const Transfer = async()=>{
            const validInput1 = validateInput({accountNumber1,amount})
            const validInput2 = validateInput({accountNumber2,amount})
            if(!validInput1 && !validInput2){
              alert("Account numbers or amount cannot be null");
              return null;
            }
            const response = await fetch("https://localhost:7075/api/CustomerTransaction/Transfer Money?amount="+amount+"&destAccountID="+accountNumber2+"&accountNumber="+accountNumber1,{
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
        if(flag==0){
            Transfer();
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
        <h1>Transfer Money</h1>
            <div class="form-group">
                <div>
                                            <label htmlFor="input1">Account Number</label>
                                            <br/>
                                            <select value={selectedOption1} onChange={handleChange1} class="browser-default custom-select">
                                                {options1.map((options) => (
                                                <option key={options.accountNumber} value={options.accountNumber}>
                                                    {options.accountNumber}
                                                </option>
                                                ))}
                                            </select>
                                        </div>
            

                                        <div>
                                            <label htmlFor="input1">Destination Account Number</label>
                                            <br/>
                                            <select value={selectedOption2} onChange={handleChange2} class="browser-default custom-select">
                                                {options2.map((options) => (
                                                <option key={options.accountNumber} value={options.accountNumber}>
                                                    {options.accountNumber}
                                                </option>
                                                ))}
                                            </select>
                                        </div>
            
                
                <label htmlFor="input3">Amount</label>
                <input type="text" class="form-control" id="input3" placeholder="Enter Amount"
                value={amount} onChange={(e)=>setAmount(e.target.value)}/>

                <button type="button" class="btn btn-success" data-toggle="button" 
                aria-pressed="false" onClick={flagmethod}>
                Transfer
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
            </div>
    )
}