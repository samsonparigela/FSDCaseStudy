import { useEffect, useState } from "react"
import './style.css'
export default function DeleteBeneficiaries(){

    const [beneficiaryNumber,setBeneficiaryNumber] = useState(11);
    const [beneficiary,setBeneficiary] = useState({});
    const [flag,setFlag] = useState(0);


    useEffect(()=>{
        const fetchBeneficiaries= async () => {
        const token = sessionStorage.getItem('Token');
        const httpHeader = { 
            method:'DELETE',
            body: JSON.stringify({
                'beneficiaryAccountNumber':beneficiaryNumber
            }),
            headers: {
                'Content-Type':'application/json',
                'accept' : 'text/plain',
                'Authorization': 'Bearer ' + token
            }
        };
        console.log(beneficiaryNumber);

            await fetch("https://localhost:7075/api/CustomerBeneficiary/DeleteBeneficiary?ID="+beneficiaryNumber,httpHeader)
            .then(r=>r.json())
            .then(r=>setBeneficiary(r))
    }
        fetchBeneficiaries();

    },[flag]);

    function flagmethod(){
        if(flag==0)
        setFlag(1);

        else
        setFlag(0);
    }

    return(
        <div style={{ width: '100%', backgroundColor: 'lightblue' }}>
        <div>
        <div class="container mt-5">
                        <div class="row">
                        <div className="col-md-12 mb-4">
                                <div class="card custom-bg-color">
                                    <div class="card-body">
            <h1>Delete Beneficiary</h1>
            <div class="form-group">
                <label for="input3">Beneficiary Number</label>
                <input type="text" class="form-control" id="input1" placeholder="Enter Beneficiary ACC No"
                value={beneficiaryNumber} onChange={(e)=>setBeneficiaryNumber(e.target.value)}/>

                <button type="button" class="btn btn-success" data-toggle="button" 
                aria-pressed="false" autocomplete="off" onClick={flagmethod}>
                Delete Beneficiary
                </button>
            </div>
            {flag==1?
                <table className="table">
                    <thead>
                        <tr>
                          <th>Beneficiary Number</th>
                          <th>beneficiary Name</th>
                          <th>IFSC Code</th>
                        </tr>
                    </thead>
                    <tbody>
                        {console.log(beneficiary)}
                        
                        <tr>
                            <td>{beneficiary.beneficiaryAccountNumber}</td>
                            <td>{beneficiary.beneficiaryName}</td>
                            <td>{beneficiary.ifscCode}</td>
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
    </div>
    </div>
    )
}