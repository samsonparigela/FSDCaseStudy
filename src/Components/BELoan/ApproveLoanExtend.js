import React, { useState, useEffect } from 'react';
import './style.css'
export default function ApproveLoanExtend(){

    const token = sessionStorage.getItem("Token");
  
    const [options,setOptions]= useState([]);
    const [approve,setApprove]= useState([]);
    const [selectedOption, setSelectedOption] = useState(null);
      
    const handleChange = (event) => {
      setSelectedOption(event.target.value);
      setLoanID(String(event.target.value))
          
    };
  
    var [flag,setFlag] = useState(0);
  
    useEffect(() => {
      var func =async()=>{
          const response2 = await fetch('https://localhost:7075/api/BankEmpLoan/GetAllLoans', {
          method: 'GET',
          headers: {
              'Authorization': 'Bearer '+token,
              'Content-Type': 'application/json'
          }
      })
      .then(response => response.json())
      .then(data => {
        let filteredList = data.filter(obj => obj.status === "Extend Request");
      setOptions(filteredList);
      });
  
      }
      func()
    },[])

    const [loanID, setLoanID] = useState();
    const [loan,setLoan] = useState({})
    var bankEmpID = sessionStorage.getItem("BID");
    var [flag,setFlag] = useState(0);

    const fetchLoans = async () => {

      try {

            const token = sessionStorage.getItem('Token');
            const response = await fetch('https://localhost:7075/api/BankEmpLoan/ApproveOrDisapproveLoanExtend?LID='+loanID+'&approval='+approve, {
                method: 'PUT',
                headers: {
                    'Authorization': 'Bearer '+token,
                    body: JSON.stringify(loanID), // Include your authorization token
                    'Content-Type': 'application/json'
                }
                });

            if (response.ok) {
            const loansData = response.json();
            setLoan(loansData);
            console.log(loansData);
            } 
            else {
            console.error('Failed to fetch');
            }
        }catch (error) {
            console.error('Error fetching:', error);
        }
    }
  
  var flagmethod = (e) =>{
    if(flag==0){
        if(e==="yes")
        setApprove("Approve Extension");
    else
    setApprove("Not Approved")
        fetchLoans();
        setFlag(1);
    }
        
    else
        setFlag(0)
    }


  return (
    <div style={{ width: '50%', backgroundColor: 'lightblue' }}>
            <div class="container mt-5">
                <div class="row">
                    <div class="col-md-12 mb-4">
                        <div class="card custom-bg-color">
                            <div class="card-body">
                            <h1>Approve Loan</h1>     
                                <div>
                                    <label htmlFor="input1">Loan ID</label>
                                    <br/>
                                    <select value={selectedOption} onChange={handleChange} class="browser-default custom-select">
                                    <option value="">Select an option</option>
                                        {options.map((option) => (
                                        <option key={option.loanID} value={option.loanID}>
                                            {option.loanID}
                                            
                                        </option>
                                        ))}
                                    </select>
                                </div>
                                <br/>
                                <button type="button" class="btn btn-success" data-toggle="button" 
                                aria-pressed="false" onClick={flagmethod("yes")}>
                                Accept
                                </button>
                                <button type="button" class="btn btn-danger" data-toggle="button" 
                                aria-pressed="false" onClick={flagmethod("no")}>
                                Reject
                                </button>
                                {flag==1 ? 
                                <table className="table">
                                    <thead>
                                    <tr>
                                        <th>Loan Extend approve Status</th>
                                    </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                        <td>{approve}</td>
                                        </tr>
                                    </tbody>
                                </table>
                                :<p></p>}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
  );
}


