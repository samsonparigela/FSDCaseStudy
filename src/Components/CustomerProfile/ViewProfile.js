import React, { useState, useEffect } from 'react';

export default function ViewProfile(){

    const [profile, setProfile] = useState([]);
    var customerID = sessionStorage.getItem("CID");

    const fetchProfile = async () => {
      try {
        const token = sessionStorage.getItem('Token');
        const response = await fetch('https://localhost:7075/api/Customer/GetByID?ID='+customerID, {
          method: 'GET',
          headers: {
            'Authorization': 'Bearer '+token,
            body: JSON.stringify(customerID), // Include your authorization token
            'Content-Type': 'application/json'
          }
        });

        if (response.ok) {
          const profileData = await response.json();
          setProfile(profileData);
        } else {
          console.error('Failed to fetch');
        }
      } catch (error) {
        console.error('Error fetching:', error);
      } 
    }
  var [flag,setFlag] = useState(0);
  var flagmethod = (e) =>{
    if(flag==0){
      fetchProfile()
      setFlag(1)
    }
   
  else
  setFlag(0)
  }
  return (
    <div style={{ width: '100%', backgroundColor: 'lightblue' }}>
    <div className="container mt-5">
        <div className="row">
            <div className="col-md-12 mb-4">
                <div className="card p-4 custom-bg-color">
                    <div className="card-body">
      <h1>Your Profile</h1>
      <button type="button" className="btn btn-success" data-toggle="button" 
      aria-pressed="false" onClick={flagmethod}>
      View
      </button>
      {flag==1? 
      <table className="table">
        <thead>
          <tr>
            <th>Customer ID</th>
            <th>Name</th>
            <th>DOB</th>
            <th>Age</th>
            <th>Phone</th>
            <th>Address</th>
            <th>Gender</th>
            <th>Aadhaar Number</th>
            <th>PAN Number</th>
          </tr>
        </thead>
        <tbody>
            <tr key={profile.customerID}>
              <td>{profile.customerID}</td>
              <td>{profile.name}</td>
              <td>{profile.dob}</td>
              <td>{profile.age}</td>
              <td>{profile.phone}</td>
              <td>{profile.address}</td>
              <td>{profile.gender}</td>
              <td>{profile.aadhaar}</td>
              <td>{profile.panNumber}</td>
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


