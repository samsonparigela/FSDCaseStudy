import React, { useState, useEffect } from 'react';
import './style.css'
export default function ApproveLoan(){

    const token = sessionStorage.getItem("Token");
  
    const [options,setOptions]= useState([]);
    const [selectedOption, setSelectedOption] = useState("");
      
    const handleChange = (event) => {
      setSelectedOption(event.target.value);
      setLoanID(String(event.target.value))
          
    };
  
    useEffect(() => {
      const func =async()=>{
          await fetch('https://localhost:7075/api/BankEmpLoan/GetAllLoans', {
          method: 'GET',
          headers: {
              'Authorization': 'Bearer '+token,
              'Content-Type': 'application/json'
          }
      })
      .then(response => response.json())
      .then(data => {
        let filteredList = data.filter(obj => obj.status === "Pending" || obj.status === "Extend Request");
      setOptions(filteredList);
      });
  
      }
      func()
    },[])

    const [loanID, setLoanID] = useState();
    const [loan,setLoan] = useState({})
    const [flag,setFlag] = useState(0);

    const validateInput = ({ loanID }) => {
        if (loanID===null) {
          return false;
        }
        return true;
      };

    const fetchLoans = async () => {

        const validInput = validateInput({loanID})
      if(!validInput){
        alert("Loan ID cannot be null");
        return null;
      }

      try {

            const token = sessionStorage.getItem('Token');
            const response = await fetch('https://localhost:7075/api/BankEmpLoan/ApproveOrDisapproveALoan?LID='+loanID, {
                method: 'PUT',
                headers: {
                    'Authorization': 'Bearer '+token,
                    body: JSON.stringify(loanID), // Include your authorization token
                    'Content-Type': 'application/json'
                }
                });

            if (response.ok) {
            const loansData = await response.json();
            setLoan(loansData);
            } 
            else {
            console.error('Failed to fetch');
            }
        }catch (error) {
            console.error('Error fetching:', error);
        }
    }
  
  const flagmethod = (e) =>{
    if(flag==0){
        fetchLoans();
        setFlag(1);
    }
        
    else
        setFlag(0)
    }


  return (
    <div style={{ width: '50%' }}>
            <div className="container mt-5">
                <div className="row">
                    <div className="col-md-12 mb-4">
                        <div className="card custom-bg-color">
                            <div className="card-body">
                            <h1>Approve Loan</h1>     
                                <div>
                                    <label htmlFor="input1">Loan ID</label>
                                    <br/>
                                    <select value={selectedOption} onChange={handleChange} className="browser-default custom-select">
                                    <option value="">Select an option</option>
                                        {options.map((option) => (
                                        <option key={option.loanID} value={option.loanID}>
                                            {option.loanID}
                                            
                                        </option>
                                        ))}
                                    </select>
                                </div>
                                <br/>
                                <button type="button" className="btn btn-success" data-toggle="button" 
                                aria-pressed="false" onClick={flagmethod}>
                                Approve
                                </button>
                                {flag==1 ? 
                                <table className="table">
                                    <thead>
                                    <tr>
                                        <th>Loan Status</th>
                                    </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                        <td>{loan.status}</td>
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


