import { useEffect, useState } from "react"
import './style.css'
import axios from "axios";
export default function AddBeneficiaries(){

    const [options,setOptions]= useState([])
    const [selectedOption, setSelectedOption] = useState("");
    const [ifscCode,setIfscCode] = useState(0);

    var customerID = sessionStorage.getItem("CID");
    const token = sessionStorage.getItem("Token");
    useEffect(()=>{
    const func = async() =>{
        const response2 = await axios.get('https://localhost:7075/api/BankAndBranch/GetAllBranches', {
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
        setIfscCode(val);
      };

    const [beneficiaryNumber,setBeneficiaryNumber] = useState(0);
    const [beneficiaryName,setBeneficiaryName] = useState("");

    const [beneficiary,setBeneficiary] = useState({});
    const [flag,setFlag] = useState(0);


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
    useEffect(() => {
        if (flag) {
            fetchBeneficiaries();
        }
    }, [flag]);
    function flagmethod(){
        if(!flag)
        setFlag(true);
      else{
        window.location.reload(false);
        setFlag(false);
      }

      }

    return(

        <div style={{ width: '100%', backgroundColor: 'lightblue' }}>
        <br/>

        <div>
        <div className="container mt-5">
                        <div className="row">
                        <div className="col-md-12 mb-4">
                                <div className="card custom-bg-color">
                                    <div className="card-body">
            <h1>Add Beneficiary</h1>
            <div className="form-group">
                <label htmlFor="input3">Beneficiary Number</label>
                <input type="text" className="form-control" id="input1" placeholder="Enter Beneficiary ACC No"
                value={beneficiaryNumber} onChange={(e)=>setBeneficiaryNumber(e.target.value)}/>


                <label htmlFor="input3">beneficiary Name</label>
                <input type="text" className="form-control" id="input2" placeholder="Enter Beneficiary Name"
                value={beneficiaryName} onChange={(e)=>setBeneficiaryName(e.target.value)}/>
                
                <div>
                                                <label htmlFor="subOptions">IFSC Code</label>
                                                <select value={selectedOption} onChange={handleChange} className="browser-default custom-select">
                                                <option value="">Select an option</option>
                                                    {options.map(option => (
                                                    <option key={option.ifscCode} value={option.ifscCode}>
                                                        {option.ifscCode}&ensp;&ensp;{option.branchName}
                                                        </option>
                                                    ))}
                                                </select>
                                                </div>

                <button type="button" className="btn btn-success" data-toggle="button" 
                aria-pressed="false" onClick={flagmethod}>
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