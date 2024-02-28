import { useEffect, useState } from "react"

export default function GetLoanAmountToAccount(){

    const [accountNumber,setAccountNumber] = useState();
    const [loanID,setLoanID] = useState();    
    const [transac,setTransac] = useState();
    const [flag,setFlag] = useState(0);

    const customerID = sessionStorage.getItem("CID");

    useEffect(()=>{
        const fetchLoans = async () => {
        const token = sessionStorage.getItem('Token');
        const httpHeader = { 
            method:'PUT',
            body: JSON.stringify({
                'loanID':loanID,
                'AccID':accountNumber
            }),
            headers: {
                'Content-Type':'application/json',
                'accept' : 'text/plain',
                'Authorization': 'Bearer ' + token
            }
        };

            fetch("https://localhost:7075/api/CustomerLoan/GetLoanAmountToAccount?LoanID="+loanID+"&AccID="+accountNumber,
            httpHeader)
            .then(r=>r.json())
            .then(r=>setTransac(r))
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
            <h1>Get Loan Amount Deposited</h1>
            <div class="form-group">
                <label for="input3">Loan ID</label>
                <input type="text" class="form-control" id="input1" placeholder="Enter Loan ID"
                value={loanID} onChange={(e)=>setLoanID(e.target.value)}/>


                <label for="input3">Account Number</label>
                <input type="text" class="form-control" id="input2" placeholder="Enter Account Nummber"
                value={accountNumber} onChange={(e)=>setAccountNumber(e.target.value)}/>

                <button type="button" class="btn btn-success" data-toggle="button" 
                aria-pressed="false" autocomplete="off" onClick={flagmethod}>
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
                    {console.log(transac)}
                    <tr key={transac.balance}>
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