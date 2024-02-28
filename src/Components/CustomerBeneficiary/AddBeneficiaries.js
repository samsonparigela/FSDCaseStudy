import { useEffect, useState } from "react"
import './style.css'
export default function AddBeneficiaries(){

    const [beneficiaryNumber,setBeneficiaryNumber] = useState();
    const [beneficiaryName,setBeneficiaryName] = useState();
    const [ifscCode,setIfscCode] = useState();
    const [beneficiary,setBeneficiary] = useState({});
    const [flag,setFlag] = useState(0);

    const customerID = sessionStorage.getItem("CID");

    useEffect(()=>{
        const fetchBeneficiaries= async () => {
        const token = sessionStorage.getItem('Token');
        const httpHeader = { 
            method:'POST',
            body: JSON.stringify({
                'customerID':customerID,
                'beneficiaryNumber':beneficiaryNumber,
                'beneficiaryName':beneficiaryName,
                'ifscCode':ifscCode
            }),
            headers: {
                'Content-Type':'application/json',
                'accept' : 'text/plain',
                'Authorization': 'Bearer ' + token
            }
        };


            await fetch("https://localhost:7075/api/CustomerBeneficiary/AddBeneficiary",httpHeader)
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
        <br/>

        <div>
        <div class="container mt-5">
                        <div class="row">
                        <div className="col-md-12 mb-4">
                                <div class="card custom-bg-color">
                                    <div class="card-body">
            <h1>Add Beneficiary</h1>
            <div class="form-group">
                <label for="input3">Beneficiary Number</label>
                <input type="text" class="form-control" id="input1" placeholder="Enter Beneficiary ACC No"
                value={beneficiaryNumber} onChange={(e)=>setBeneficiaryNumber(e.target.value)}/>


                <label for="input3">beneficiary Name</label>
                <input type="text" class="form-control" id="input2" placeholder="Enter Beneficiary Name"
                value={beneficiaryName} onChange={(e)=>setBeneficiaryName(e.target.value)}/>
                
                <label for="input3">IFSC</label>
                <input type="text" class="form-control" id="input3" placeholder="Enter IFSC Code"
                value={ifscCode} onChange={(e)=>setIfscCode(e.target.value)}/>

                <button type="button" class="btn btn-success" data-toggle="button" 
                aria-pressed="false" autocomplete="off" onClick={flagmethod}>
                Add Beneficiary
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
                            <td>{beneficiary.beneFiciaryNumber}</td>
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