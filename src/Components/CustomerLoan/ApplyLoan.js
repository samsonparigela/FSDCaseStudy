import { useEffect, useState } from "react"

export default function ApplyLoan(){

    var customerID = sessionStorage.getItem("CID");
    const token = sessionStorage.getItem("Token");
  
    const [options,setOptions]= useState([])
    const [options2,setOptions2]= useState([])
    const [selectedOption, setSelectedOption] = useState(null);
    const [selectedOption2, setSelectedOption2] = useState(null);
      
    const handleChange = (event) => {
      setSelectedOption(event.target.value);
      setLoanPolicyID(event.target.value)
          
    };

    const handleChange2 = (event) => {
        setSelectedOption2(event.target.value);
        setLoanAmount(event.target.value)
            
      };

    const [loanpolicyID,setLoanPolicyID] = useState();
    const [loanPurpose,setLoanPurpose] = useState();
    const [loanAmount,setLoanAmount] = useState();
    const [loan,setLoan] = useState({});
    const [flag,setFlag] = useState(0);

        const fetchLoans = async () => {
        const httpHeader = { 
            method:'POST',
            body: JSON.stringify({
                'loanpolicyID':loanpolicyID,
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


            fetch("https://localhost:7075/api/CustomerLoan/ApplyForALoan",httpHeader)
            .then(r=>r.json())
            .then(r=>setLoan(r))
    }

    useEffect(() => {
        var func =async()=>{
            const response2 = await fetch('https://localhost:7075/api/BankEmpLoan/GetAllLoanPolicies', {
            method: 'GET',
            headers: {
                'Authorization': 'Bearer '+token,
                body: JSON.stringify(customerID), 
                'Content-Type': 'application/json'
            }
        })
        .then(response => response.json())
        .then(data => {
        setOptions(data);
        setOptions2(data);
        });
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
                                                {console.log(options2)+"kk"}
                                                {options2.map((options) => (
                                                <option key={options2.loanPoilcyID} value={options2.loanPoilcyID}>
                                                    {options2.loanPoilcyID}
                                                </option>
                                                ))}
                                            </select>
                                        </div>

                <label for="input3">Loan Purpose</label>
                <input type="text" class="form-control" id="input2" placeholder="Enter Loan Purpose"
                value={loanPurpose} onChange={(e)=>setLoanPurpose(e.target.value)}/>
                
                <div>
                                            <label htmlFor="input1">Amount</label>
                                            <br/>
                                            <select value={selectedOption2} onChange={handleChange2} class="browser-default custom-select">
                                                {options.map((options) => (
                                                <option key={options.loanAmount} value={options.loanAmount}>
                                                    {options.loanAmount}
                                                </option>
                                                ))}
                                            </select>
                                        </div>
                <button type="button" class="btn btn-success" data-toggle="button" 
                aria-pressed="false" autocomplete="off" onClick={flagmethod}>
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
                        </tr>
                    </thead>
                    <tbody>
                        {console.log(loan)}
                        
                        <tr key={loan.loanPolicyID}>
                            <td>{loan.loanPolicyID}</td>
                            <td>{customerID}</td>
                            <td>{loan.loanPurpose}</td>
                            <td>{loan.loanAmount}</td>
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