import axios from "axios";
import { error } from "jquery";
import { useEffect, useState } from "react"

export default function GetLoanAmountToAccount(){

    const [accountNumber,setAccountNumber] = useState();
    const [loanID,setLoanID] = useState();    
    const [transac,setTransac] = useState({'balance':0});
    const [flag,setFlag] = useState(0);

    const token = sessionStorage.getItem("Token");

    var customerID = sessionStorage.getItem("CID");
    
    const url1 = 'https://localhost:7075/api/CustomerLoan/GetAllApprovedLoans?ID='+customerID;
    const url2 ='https://localhost:7075/api/CustomerAccount/View All your Accounts?ID='+customerID;
  
    const [options,setOptions]= useState([])
    const [selectedOption, setSelectedOption] = useState(null);

    const [options2,setOptions2]= useState([])
    const [selectedOption2, setSelectedOption2] = useState(null);
      
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
        setOptions(response.data);
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


    const fetchLoans = async () => {

        try{
            var response2 = await axios.put("https://localhost:7075/api/CustomerLoan/GetLoanAmountToAccount?LoanID="+loanID+"&AccID="+accountNumber, 
            null,{headers: {
                'Authorization': 'Bearer '+token,
                'Content-Type': 'application/json'
            },});
            setTransac(response2.data)
        }catch(err){
            alert(err.response.data)
        }
    }

    function flagmethod(){
        if(flag==0){
            fetchLoans();
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
                                    <div class="card-body"></div>
            <h1>Get Loan Amount Deposited</h1>
            <div class="form-group">
            <div>
                                            <label htmlFor="input1">Loan ID</label>
                                            <br/>
                                            <select value={selectedOption} onChange={handleChange} class="browser-default custom-select">
                                            <option value="">Select an option</option> {/* Default option */}
                                                {options.map((options) => (
                                                <option key={options.loanID} value={options.loanID}>
                                                    {options.loanID}
                                                </option>
                                                ))}
                                            </select>
                                        </div>


                                        <div>
                                            <label htmlFor="input2">Account Number</label>
                                            <br/>
                                            <select value={selectedOption2} onChange={handleChange2} class="browser-default custom-select">
                                            <option value="">Select an option</option> {/* Default option */}
                                                {options2.map((options) => (
                                                <option key={options.accountNumber} value={options.accountNumber}>
                                                    {options.accountNumber}
                                                </option>
                                                ))}
                                            </select>
                                        </div>
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
                    <th>Balance</th>
                  </tr>
                </thead>
                <tbody>
                    <tr>
                      <td>{transac.balance}</td>
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