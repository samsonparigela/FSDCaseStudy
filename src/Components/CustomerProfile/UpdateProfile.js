import { useEffect, useState } from "react"

export default function UpdateProfile(){

    const [name,setName] = useState("");
    const [phone,setPhone] = useState("");
    const [address,setAddress] = useState("");
    const [profile1,setProfile1] = useState({});
    const validateInput = ({ name,phone,address }) => {
        if (!name.trim()) {
          return false;
        }
        if (!phone.trim()) {
            return false;
        }
        if (!address.trim()) {
            return false;
        }
        return true;
      };
    const [flag,setFlag] = useState(0);

    const customerID = sessionStorage.getItem("CID");

        const fetchProfile= async () => {
            
            const validInput = validateInput({ name,phone,address })
            if(!validInput){
                alert("One or more values are null");
                return null;
            }
            

        const token = sessionStorage.getItem('Token');
        const httpHeader1 = { 
            method:'PUT',
            body: JSON.stringify({
                'id':customerID,
                'name':name,
                'phoneNumber':phone,
                'address':address
            }),
            headers: {
                'Content-Type':'application/json',
                'accept' : 'text/plain',
                'Authorization': 'Bearer ' + token
            }
        };

            var response1 = await fetch("https://localhost:7075/api/Customer/UpdateCustomer",httpHeader1)
            .then(r=>r.json())
            .then(r=>setProfile1(r))

    }


    function flagmethod(){
        if(flag==0){
            fetchProfile();
            setFlag(1);
        }
        else
        setFlag(0);
    }

    return(
        <div style={{ width: '100%', backgroundColor: 'lightblue' }}>
            <div class="container mt-5">
                <div class="row">
                    <div class="col-md-12 mb-4">
                        <div class="card p-4 custom-bg-color">
                            <div class="card-body"></div>
            <h1>Update Profile</h1>
            <div class="form-group">

                <label htmlFor="input3">Name</label>
                <input type="text" class="form-control" id="input2" placeholder="Enter new Name"
                value={name} onChange={(e)=>setName(e.target.value)} required/>
                
                <label htmlFor="input3">Phone Number</label>
                <input type="text" class="form-control" id="input1" placeholder="Enter Phone"
                value={phone} onChange={(e)=>setPhone(e.target.value)} required/>

                <label htmlFor="input3">Address</label>
                <input type="text" class="form-control" id="input3" placeholder="Enter Address"
                value={address} onChange={(e)=>setAddress(e.target.value)} required/>
                <br/>
                <button type="button" class="btn btn-success" data-toggle="button" 
                aria-pressed="false" onClick={flagmethod}>
                Update Profile
                </button>
            </div>
            {flag==1?
                <table className="table">
                    <thead>
                        <tr>
                          <th>Name</th>
                          <th>Phone Number</th>
                          <th>Address</th>
                        </tr>
                    </thead>
                    <tbody>
                        
                        <tr>
                            <td>{profile1.name}</td>
                            <td>{profile1.phoneNumber}</td>
                            <td>{profile1.address}</td>
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