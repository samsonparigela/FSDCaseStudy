import axios from "axios";
import { useEffect, useState } from "react"

export default function ApplyLoan(){

    var customerID = sessionStorage.getItem("CID");
    const token = sessionStorage.getItem("Token");
  
    const [options,setOptions]= useState([])

    const [selectedOption, setSelectedOption] = useState("");
    const [selectedSubOption, setSelectedSubOption] = useState("");
    const [selectedSubOption2, setSelectedSubOption2] = useState("");
    const [selectedOption2, setSelectedOption2] = useState("");
      
    const handleChange = (event) => {
      const val =event.target.value;

      setSelectedOption(event.target.value);
      setLoanPolicyID(event.target.value)

      for(let i=0;i<options.length;i++){
        if(options[i].loanPolicyID===parseInt(val)){

            setSelectedSubOption(options[i].tenureInMonths)
            setSelectedSubOption2(options[i].interest)
            setInterest(options[i].interest)
        }
      }

          
    };

    const handleChange2 = (event) => {
        setSelectedOption2(event.target.value);
        setLoanAmount(event.target.value)
            
      };

    const [loanPolicyID,setLoanPolicyID] = useState();
    const [loanPurpose,setLoanPurpose] = useState(0);
    const [loanAmount,setLoanAmount] = useState(0);
    const [loan,setLoan] = useState({});
    const [flag,setFlag] = useState(0);
    var fullAmount;
    var [interest,setInterest] = useState(0);

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
            const response2 = await axios.get('https://localhost:7075/api/CustomerLoan/GetDifferentLoanPolicies', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            }
        })
        setOptions(response2.data)
        }
        func()
      },[])


    function flagmethod(){
        if(flag===0){
            fetchLoans();
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
                            <h1>Apply for a Loan</h1>
                            <div className="form-group">
                                <div>
                                    <label htmlFor="input1">Loan Policy ID</label>
                                    <br/>
                                    <select value={selectedOption} onChange={handleChange} className="browser-default custom-select">
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
                                                <select value={selectedSubOption} disabled className="browser-default custom-select"> 
                                                    {options.map(option => (
                                                    <option key={option.tenureInMonths} value={option.tenureInMonths}>{option.tenureInMonths}</option>
                                                    ))}
                                                </select>
                                                </div>
                                )} 
                                {selectedOption && (
                                                <div>
                                                <label htmlFor="subOptions">Interest</label>
                                                <select value={selectedSubOption2} onChange={handleChange2} disabled className="browser-default custom-select"> 
                                                    {options.map(option => (
                                                    <option key={option.interest} value={option.interest}>{option.interest}</option>
                                                    ))}
                                                </select>
                                                </div>
                                )}                                 
                                <label htmlFor="input3">Loan Purpose</label>
                                <input type="text" className="form-control" id="input4" placeholder="Enter Loan Purpose"
                                value={loanPurpose} onChange={(e)=>setLoanPurpose(e.target.value)}/>
                
                                <div>
                                    <label htmlFor="input4">Amount</label>
                                    <br/>
                                    <select value={selectedOption2} onChange={handleChange2} className="browser-default custom-select">
                                    <option value="">Select an option</option>
                                        {options.map((options) => (
                                        <option key={options.loanAmount} value={options.loanAmount}>
                                            {options.loanAmount}
                                        </option>
                                        ))}
                                    </select>
                                </div>
                                <br/>
                                <button type="button" className="btn btn-success" data-toggle="button" 
                                aria-pressed="false" onClick={flagmethod}>
                                Apply
                                </button>
                                
                            </div>
                            {flag===1?
                                <table className="table">
                                    <thead>
                                        <tr>
                                        <th>Loan Policy ID</th>
                                        <th>Customer ID</th>
                                        <th>Loan Amount</th>
                                        <th>Repay Amount</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr key={loan.loanPolicyID}>
                                            <td>{loan.loanPolicyID}</td>
                                            <td>{customerID}</td>
                                            <td>{loan.loanAmount}</td>
                                            <td>{fullAmount = parseInt(loan.loanAmount)+(parseInt(loan.loanAmount)*(parseInt(interest)/100))}</td>
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