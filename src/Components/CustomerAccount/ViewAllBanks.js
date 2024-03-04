import React, { useState, useEffect } from 'react';
import pic1 from './j4.jpeg'
export default function ViewAllBanks(){

    
        const [banks, setBanks] = useState([]);
        var customerID = sessionStorage.getItem("CID");

        const fetchBanks = async () => {
          try {
            const token = sessionStorage.getItem('Token');
            const response = await fetch('https://localhost:7075/api/CustomerAccount/View All Banks', {
              method: 'GET',
              headers: {
                'Authorization': 'Bearer '+token,
                body: JSON.stringify(customerID), // Include your authorization token
                'Content-Type': 'application/json'
              }
            });
    
            if (response.ok) {
              const ordersData = await response.json();
              setBanks(ordersData);
            } else {
              console.error('Failed to fetch orders');
            }
          } catch (error) {
            console.error('Error fetching orders:', error);
          }
        }
      var [flag,setFlag] = useState(0);
      var flagmethod = (e) =>{
        if(flag==0){
          fetchBanks()
          setFlag(1)
        }
        
      else
      setFlag(0)
      }
      return (
        <div style={{ width: '50%', backgroundColor: 'lightblue' }}>
        <div className="container mt-5">
            <div className="row">
                <div className="col-md-12 mb-4">
                    <div className="card custom-bg-color">
                                <div className="card-body">
    
    <div>
          <h1>All Banks Available</h1>
          <button type="button" className="btn btn-success" data-toggle="button" 
          aria-pressed="false" onClick={flagmethod}>
          Get all banks
          </button>
          {flag==1? 
          <table className="table">
            <thead>
              <tr>
                <th>Bank ID</th>
                <th>Bank Name</th>
              </tr>
            </thead>
            <tbody>
            {banks.map(b => (
                <tr key={b.bankID}>
                  <td>{b.bankID}</td>
                  <td>{b.bankName}</td>
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