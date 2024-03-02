import React, { useState, useEffect } from 'react';
import './style.css'
export default function ViewProfile(){

    const [profile, setProfile] = useState({});
    var AdminID = sessionStorage.getItem("AdminID");
    var [flag,setFlag] = useState(0);
    
    const fetchProfile = async () => {
      try {
        if(flag==0)
          setFlag(1)
        else
          setFlag(0)
        const token = sessionStorage.getItem('Token');
        const response = await fetch('https://localhost:7075/api/Admin/GetAdminByID?key='+AdminID, {
          method: 'GET',
          headers: {
            'Authorization': 'Bearer '+token,
            body: JSON.stringify(AdminID), // Include your authorization token
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

  return (
    <div style={{ width: '50%', backgroundColor: 'lightblue' }}>


<div>
<div class="container mt-5">
                <div class="row">
                <div className="col-md-12 mb-4">
                        <div class="card custom-bg-color">
                            <div class="card-body">
      <h1>Your Profile</h1>
      <br/>
      <button type="button" class="btn btn-success" data-toggle="button" 
      aria-pressed="false" onClick={fetchProfile}>
      View
      </button>
      <br/>
      {flag==1? 
      <table className="table">
        <thead>
          <tr>
            <th>Admin ID</th>
            <th>Name</th>
            <th>UserID</th>
          </tr>
        </thead>
        <tbody>
            <tr key={profile.adminID}>
              <td>{profile.adminID}</td>
              <td>{profile.name}</td>
              <td>{profile.userID}</td>
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
    </div>
  );
}


