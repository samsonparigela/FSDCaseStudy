import { useEffect, useState } from "react"
import axios from "axios";
export default function RepayLoan(){

    const [accountNumber,setAccountNumber] = useState();   

    const token = sessionStorage.getItem("Token");
    var customerID = sessionStorage.getItem("CID");
    
    const url1 = 'https://localhost:7075/api/CustomerLoan/GetAllAppliedLoans?ID='+customerID;
    const url2 ='https://localhost:7075/api/CustomerAccount/View All your Accounts?ID='+customerID;
  
    const [options,setOptions]= useState([])
    const [selectedOption, setSelectedOption] = useState("");

    const [options2,setOptions2]= useState([])
    const [selectedOption2, setSelectedOption2] = useState("");
      
    const handleChange = (event) => {
      setSelectedOption(event.target.value);
      setLoanID(parseInt(event.target.value))
          
    };

    const handleChange2 = (event) => {
        setSelectedOption2(event.target.value);
        setAccountNumber(String(event.target.value))
            
    };

    useEffect(() => {
        var func =async()=>{
            const response = await axios.get(url1, {headers: {
                'Authorization': 'Bearer '+token,
                'Content-Type': 'application/json'
            },});
            let filteredList = response.data.filter(obj => obj.status == "Deposited");
        setOptions(filteredList);
        }
        func()
    },[]);

    useEffect(() => {
        var func =async()=>{
            const response = await axios.get(url2, {headers: {
                'Authorization': 'Bearer '+token,
                'Content-Type': 'application/json'
            },});
            let filteredList = response.data.filter(obj => obj.status == "Approved");
        setOptions2(filteredList);
        }
        func()
    },[])

    const [amount,setAmount] = useState(0);
    const [loanID,setLoanID] = useState();    
    const [loan,setLoan] = useState({});
    const [flag,setFlag] = useState(0);

        const fetchLoans = async () => {
        const token = sessionStorage.getItem('Token');
        const httpHeader = { 
            method:'PUT',
            body: JSON.stringify({
                
            }),
            headers: {
                'Content-Type':'application/json',
                'accept' : 'text/plain',
                'Authorization': 'Bearer ' + token
            }
        };
        if(isNaN(amount)){
            alert("Enter Number");
            return null;
        }

            fetch("https://localhost:7075/api/CustomerLoan/RepayLoan?loanID="+loanID+"&accountNumber="+accountNumber+"&amount="+amount,
            httpHeader)
            .then(r=>r.json())
            .then(r=>setLoan(r))
    }

    function flagmethod(){
        if(flag==0){
            fetchLoans();
            setFlag(1);
        }
        

        else
        {
            setFlag(false);
            window.location.reload(false);
        }
    }

    return(
        <div>
                        <div className="container mt-5">
                <div className="row">
                    <div className="col-md-12 mb-4">
                        <div className="card p-4 custom-bg-color">
                                    <div className="card-body"></div>
            <h1>Repay the loan</h1>
            <div className="form-group">
            <div>
                                            <label htmlFor="input1">Loan ID</label>
                                            <br/>
                                            <select value={selectedOption} onChange={handleChange} className="browser-default custom-select">
                                            <option value="">Select an option</option> {/* Default option */}
                                                {options.map((options) => (
                                                    
                                                <option key={options.loanID} value={options.loanID}>
                                                    {options.loanID}&ensp;&ensp;
                                                    Repay Amount - {options.calculateFinalAmount}
                                                </option>
                                                ))}
                                            </select>
                                        </div>


                                        <div>
                                            <label htmlFor="input2">Account Number</label>
                                            <br/>
                                            <select value={selectedOption2} onChange={handleChange2} className="browser-default custom-select">
                                            <option value="">Select an option</option>
                                                {options2.map((options) => (
                                                <option key={options.accountNumber} value={options.accountNumber}>
                                                    {options.accountNumber}&ensp;&ensp;
                                                    Balance - {options.balance}
                                                </option>
                                                ))}
                                            </select>
                                        </div>
                                        <br/>


                <label htmlFor="input3">Amount</label>
                <input type="text" className="form-control" id="input2" placeholder="Enter Amount"
                value={amount} onChange={(e)=>setAmount(e.target.value)}/>
                <br/>
                <button type="button" className="btn btn-success" data-toggle="button" 
                aria-pressed="false" onClick={flagmethod}>
                Repay
                </button>
            </div>
            {flag==1?
                <table className="table">
                <thead>
                  <tr>
                    <th>Status</th>
                    <th>Amount Pending</th>
                  </tr>
                </thead>
                <tbody>
                    <tr key={loan.status}>
                      <td>{loan.status}</td>
                      <td>{loan.calculateFinalAmount}</td>
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