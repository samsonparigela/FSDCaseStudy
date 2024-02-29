import axios from "axios";
import { useEffect, useState } from "react"

export default function ApplyLoan(){

    var customerID = sessionStorage.getItem("CID");
    const token = sessionStorage.getItem("Token");
  
    const [options,setOptions]= useState([])

    const [selectedOption, setSelectedOption] = useState(null);
    const [selectedSubOption, setSelectedSubOption] = useState(null);
    const [selectedSubOption2, setSelectedSubOption2] = useState(null);
    const [selectedOption2, setSelectedOption2] = useState(null);
      
    const handleChange = (event) => {
      const val =event.target.value;
      setSelectedOption(event.target.value);
      for(let i=0;i<options.length;i++){
        if(options[i].loanPolicyID==val){
            setSelectedSubOption(options[i].tenureInMonths)
            setSelectedSubOption2(options[i].interest)
            setInterest(options[i].interest)
        }
      }
      setLoanPolicyID(event.target.value)
          
    };

    const handleSubChange = (event) => {
        setSelectedSubOption(event.target.value);
        setLoanPolicyID(event.target.value)
            
      };

    const handleChange2 = (event) => {
        setSelectedOption2(event.target.value);
        setLoanAmount(event.target.value)
            
      };

    const [loanPolicyID,setLoanPolicyID] = useState();
    const [loanPurpose,setLoanPurpose] = useState("");
    const [loanAmount,setLoanAmount] = useState();
    const [loan,setLoan] = useState({});
    const [flag,setFlag] = useState(0);
    var [interest,setInterest] = useState();

        const fetchLoans = async () => {
        const httpHeader = { 
            method:'POST',
            body: JSON.stringify({
                'loanpolicyID':loanPolicyID,
                'customerID':customerID,
                'loanPurpose':loanPurpose,
                'loanAmount':loanAmount
            }),
            headers: {
                'Content-Type':'application/json',
                'accept' : 'text/plain',
                'Authorization': 'Bearer ' + token
            }
        };
        if(isNaN(loanAmount)){
            alert("Enter Number");
            return null;
        }


            await fetch("https://localhost:7075/api/CustomerLoan/ApplyForALoan",httpHeader)
            .then(r=>r.json())
            .then(r=>setLoan(r))
    }

    useEffect(() => {
        var func =async()=>{
            const response2 = await axios.get('https://localhost:7075/api/BankEmpLoan/GetAllLoanPolicies', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        })
        setOptions(response2.data)
        }
        func()
      },[])


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
                            <h1>Apply for a Loan</h1>
                            <div class="form-group">
                                <div>
                                    <label htmlFor="input1">Loan Policy ID</label>
                                    <br/>
                                    <select value={selectedOption} onChange={handleChange} class="browser-default custom-select">
                                    <option value="">Select an option</option>
                                        {options.map((option) => (
                                        <option key={option.tenureInMonths} value={option.loanPoilcyID}>
                                            {option.loanPolicyID}
                                            
                                        </option>
                                        ))}
                                    </select>
                                </div>
                                {selectedOption && (
                                                <div>
                                                <label htmlFor="subOptions">Tenure in months</label>
                                                <select value={selectedSubOption} onChange={handleSubChange} disabled class="browser-default custom-select"> 
                                                    {options.map(option => (
                                                    <option key={option.tenureInMonths} value={option.tenureInMonths}>{option.tenureInMonths}</option>
                                                    ))}
                                                </select>
                                                </div>
                                )} 
                                {selectedOption && (
                                                <div>
                                                <label htmlFor="subOptions">Interest</label>
                                                <select value={selectedSubOption2} onChange={handleSubChange} disabled class="browser-default custom-select"> 
                                                    {options.map(option => (
                                                    <option key={option.tenureInMonths} value={option.interest}>{option.interest}</option>
                                                    ))}
                                                </select>
                                                </div>
                                )}                                 
                                <label htmlFor="input3">Loan Purpose</label>
                                <input type="text" class="form-control" id="input2" placeholder="Enter Loan Purpose"
                                value={loanPurpose} onChange={(e)=>setLoanPurpose(e.target.value)}/>
                
                                <div>
                                    <label htmlFor="input1">Amount</label>
                                    <br/>
                                    <select value={selectedOption2} onChange={handleChange2} class="browser-default custom-select">
                                    <option value="">Select an option</option>
                                        {options.map((options) => (
                                        <option key={options.loanAmount} value={options.loanAmount}>
                                            {options.loanAmount}
                                        </option>
                                        ))}
                                    </select>
                                </div>
                                <br/>
                                <button type="button" class="btn btn-success" data-toggle="button" 
                                aria-pressed="false" onClick={flagmethod}>
                                Apply
                                </button>
                                
                            </div>
                            {flag==1?
                                <table className="table">
                                    <thead>
                                        <tr>
                                        <th>Loan Policy ID</th>
                                        <th>Customer ID</th>
                                        <th>Loan Purpose</th>
                                        <th>Loan Amount</th>
                                        <th>Repay Amount</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        
                                        <tr key={loan.loanPolicyID}>
                                            <td>{loan.loanPolicyID}</td>
                                            <td>{customerID}</td>
                                            <td>{loan.loanPurpose}</td>
                                            <td>{loan.loanAmount}</td>
                                            <td>{loan.loanAmount+(loan.loanAmount*(interest/100))}</td>
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