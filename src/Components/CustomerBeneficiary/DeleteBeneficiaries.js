import { useEffect, useState } from "react"
import './style.css'
import axios from "axios";
export default function DeleteBeneficiaries(){

    const [options,setOptions]= useState([])
    const [selectedOption, setSelectedOption] = useState("");


    var customerID = sessionStorage.getItem("CID");
    const token = sessionStorage.getItem("Token");
    useEffect(()=>{
    const func = async() =>{
        const response2 = await axios.get('https://localhost:7075/api/CustomerBeneficiary/GetAllBeneficiary?CID='+customerID, {
                headers: {
                    'Authorization': 'Bearer '+token,
                    'Content-Type': 'application/json'
                }
            })
            
            let filteredData = response2.data.filter(obj => obj.beneficiaryName !== "Self");
            setOptions(filteredData);
    } 
    func()
},[selectedOption]);
    const handleChange = (event) => {
        event.preventDefault()
        let val = (event.target.value);
        setSelectedOption(val);
        setBeneficiaryNumber(val);
      };


    const [beneficiaryNumber,setBeneficiaryNumber] = useState(11);
    const [beneficiary,setBeneficiary] = useState({});
    const [flag,setFlag] = useState(0);


        const fetchBeneficiaries= async () => {
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

            await fetch("https://localhost:7075/api/CustomerBeneficiary/DeleteBeneficiary?ID="+beneficiaryNumber,httpHeader)
            .then(r=>r.json())
            .then(r=>setBeneficiary(r))
    }

    function flagmethod(){
        if(flag==0){
            fetchBeneficiaries()
            setFlag(1);
        }
        

        else
        setFlag(0);
    }

    return(
        <div style={{ width: '100%', backgroundColor: 'lightblue' }}>
        <div>
        <div className="container mt-5">
                        <div className="row">
                        <div className="col-md-12 mb-4">
                                <div className="card custom-bg-color">
                                    <div className="card-body">
            <h1>Delete Beneficiary</h1>
            <div className="form-group">
            <div>
                                            <label htmlFor="input1">Beneficiary Number</label>
                                            <br/>
                                            <select value={selectedOption} onChange={handleChange} className="browser-default custom-select">
                                            <option value="">Select an option</option>
                                                {options.map((options) => (
                                                <option key={options.beneficiaryAccountNumber} value={options.beneficiaryAccountNumber}>
                                                    {options.beneficiaryAccountNumber}&ensp;&ensp;{options.beneficiaryName}
                                                </option>
                                                ))}
                                            </select>
                                        </div>
                                        <br/>

                <button type="button" className="btn btn-success" data-toggle="button" 
                aria-pressed="false" onClick={flagmethod}>
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