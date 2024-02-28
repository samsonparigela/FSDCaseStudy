import { useEffect, useState } from "react"

export default function AskForExtension(){

    const [tenure,setTenure] = useState();
    const [loanID,setLoanID] = useState();    
    const [loan,setLoan] = useState({});
    const [flag,setFlag] = useState(0);

    const customerID = sessionStorage.getItem("CID");

    useEffect(()=>{
        const fetchLoans = async () => {
        const token = sessionStorage.getItem('Token');
        const httpHeader = { 
            method:'PUT',
            body: JSON.stringify({
                'loanID':loanID,
                'tenureInMonths':tenure
            }),
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
        fetchLoans();
    },[flag]);

    function flagmethod(){
        if(flag==0)
        setFlag(1);

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
                <label for="input3">Loan ID</label>
                <input type="text" class="form-control" id="input1" placeholder="Enter Loan ID"
                value={loanID} onChange={(e)=>setLoanID(e.target.value)}/>


                <label for="input3">Tenure In Months</label>
                <input type="text" class="form-control" id="input2" placeholder="Enter Tenure"
                value={tenure} onChange={(e)=>setTenure(e.target.value)}/>

                <button type="button" class="btn btn-success" data-toggle="button" 
                aria-pressed="false" autocomplete="off" onClick={flagmethod}>
                Apply
                </button>
            </div>
            {flag==1?
                <table className="table">
                    <thead>
                        <tr>
                          <th>Loan ID</th>
                          <th>Customer ID</th>
                          <th>Tenure In Months</th>
                        </tr>
                    </thead>
                    <tbody>
                        {console.log(loan)}
                        
                        <tr key={loan.loanID}>
                            <td>{loan.loanID}</td>
                            <td>{customerID}</td>
                            <td>{loan.tenureInMonths}</td>
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