import React, { useState, useEffect } from 'react';
export default function ViewAllBranches(){

    
        const [branches, setBranches] = useState([]);
        const [branchID, setBranchID] = useState(0);
        var [flag,setFlag] = useState(0);

        const validateInput = ({ branchID }) => {
          if (!branchID.trim()) {
            return false;
          }
          return true;
        };

        const fetchBranches = async () => {
          const validInput = validateInput({branchID})
      if(!validInput){
        alert("Branch ID cannot be null");
        return null;
      }
          try {
            const response = await fetch('https://localhost:7075/api/CustomerAccount/View All Branches?BID='+branchID, {
              method: 'GET',
              headers: {
                body: JSON.stringify(branchID), // Include your authorization token
                'Content-Type': 'application/json'
              }
            });
            if (response.ok) {
              const ordersData = await response.json();
              setBranches(ordersData);
            } else {
              console.error('Failed to fetch orders');
            }
          } catch (error) {
            console.error('Error fetching orders:', error);
          }
        }
        const flagmethod = (e) =>{
          e.preventDefault()
          if(flag==0){
            fetchBranches()
            setFlag(1);
          }
          
        else
        setFlag(0)
        }

      return (
        <div style={{ width: '50%', backgroundColor: 'lightblue' }}>
        <div class="container mt-5">
            <div class="row">
                <div class="col-md-12 mb-4">
                    <div class="card custom-bg-color">
                                <div class="card-body">
    
    <div>
          <h1>All Branches Available</h1>
          <div class="form-group">
                            <label htmlFor="input1">Bank ID</label>
                            <input type="text" class="form-control" id="input1" placeholder="Enter Bank ID"
                             value={branchID} onChange={(e)=>setBranchID(e.target.value)} autoComplete="on"/>
          <button type="button" class="btn btn-success" data-toggle="button" 
          aria-pressed="false" onClick={flagmethod}>
          Get all Branches
          </button>
          </div>
          {flag==1? 
          <table className="table">
            <thead>
              <tr>
                <th>IFSC Code</th>
                <th>Branch Name</th>
                <th>City</th>
                <th>Bank ID</th>
              </tr>
            </thead>
            <tbody>
            {branches.map(b => (
                <tr key={b.ifscCode}>
                  <td>{b.ifscCode}</td>
                  <td>{b.branchName}</td>
                  <td>{b.city}</td>
                  <td>{b.bankID}</td>
                </tr>
              ))}
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