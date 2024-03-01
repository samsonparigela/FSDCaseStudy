import { useEffect, useState } from "react"
import './style.css'
export default function UpdateProfile(){

    const [name,setName] = useState("");
    const [position,setPosition] = useState("");

    const [profile1,setProfile1] = useState({});

    const [flag,setFlag] = useState(0);

    const customerID = sessionStorage.getItem("BID");

        const fetchProfile= async () => {

            if(flag==0)
            setFlag(1);
    
            else
            setFlag(0);
        const token = sessionStorage.getItem('Token');
        const httpHeader1 = { 
            method:'PUT',
            body: JSON.stringify({
                'id':customerID,
                'name':name,
                'position':position
            }),
            headers: {
                'Content-Type':'application/json',
                'accept' : 'text/plain',
                'Authorization': 'Bearer ' + token
            }
        };

            var response1 = await fetch("https://localhost:7075/api/BankEmployee/Update",httpHeader1)
            .then(r=>r.json())
            .then(r=>setProfile1(r))

    }

    return(
        <div style={{ width: '50%', backgroundColor: 'lightblue' }}>
            <div class="container mt-5">
                <div class="row">
                    <div class="col-md-12 mb-4">
                        <div class="card p-4 custom-bg-color">
                            <div class="card-body"></div>
            <h1>Update Profile</h1>
            <div class="form-group">

                <label htmlFor="input3">Name</label>
                <input type="text" class="form-control" id="input2" placeholder="Enter new Name"
                value={name} onChange={(e)=>setName(e.target.value)}/>
                

                <label htmlFor="input3">Position</label>
                <input type="text" class="form-control" id="input3" placeholder="Enter Position"
                value={position} onChange={(e)=>setPosition(e.target.value)}/>
                <br/>
                <button type="button" class="btn btn-success" data-toggle="button" 
                aria-pressed="false" onClick={fetchProfile}>
                Update Profile
                </button>
            </div>
            {flag==1?
                <table className="table">
                    <thead>
                        <tr>
                          <th>Name</th>
                          <th>Position</th>
                        </tr>
                    </thead>
                    <tbody>
                        
                        <tr>
                            <td>{profile1.name}</td>
                            <td>{profile1.position}</td>
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