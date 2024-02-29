import { useEffect, useState } from "react"
import axios from "axios";
export default function AskForExtension(){

    const [loanID,setLoanID] = useState();    
    const [flag,setFlag] = useState(0);

    const token = sessionStorage.getItem("Token");
    var customerID = sessionStorage.getItem("CID");
    
    const url1 = 'https://localhost:7075/api/CustomerLoan/GetAllAppliedLoans?ID='+customerID;
  
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
        setTenure((event.target.value))
            
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

    const [tenure,setTenure] = useState();  
    const [loan,setLoan] = useState({});
    
        const fetchLoans = async () => {

        const Body = {
                'loanID': loanID,
                'tenureInMonths': tenure,
                'status':'Extend Request'
        }
        
        const httpHeader = { 
            method:'PUT',
            body:JSON.stringify(Body),
            headers: {
                'Content-Type':'application/json',
                'accept' : 'text/plain',
                'Authorization': 'Bearer ' + token
            }
        };

            fetch("https://localhost:7075/api/CustomerLoan/AskForExtension",httpHeader)
            .then(r=>r.json())
            .then(r=>setLoan(r))
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
            <h1>Ask for loan extension</h1>
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
                                            <label htmlFor="input1">Tenure in Months</label>
                                            <br/>
                                            <select value={selectedOption2} onChange={handleChange2} class="browser-default custom-select">
                                            <option value="">Select an option</option>
                                                <option value="1">1 Month</option>
                                                <option value="2">2 Months</option>
                                                <option value="3">3 Months</option>
                                                <option value="4">4 Months</option>
                                                <option value="5">5 Months</option>
                                                <option value="6">6 Months</option>

                                            </select>
                                        </div>
                <button type="button" class="btn btn-success" data-toggle="button" 
                aria-pressed="false" onClick={flagmethod}>
                Apply
                </button>
            </div>
            {flag==1?
                <table className="table">
                    <thead>
                        <tr>
                          <th>Loan ID</th>
                          <th>status</th>
                        </tr>
                    </thead>
                    <tbody>
                        
                        <tr key={loan.loanID}>
                            <td>{loan.loanID}</td>
                            <td>{loan.status}</td>
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