import React from 'react';
import { Link } from 'react-router-dom';
import Navbar from './Navbar';
import pic1 from './Images/n3.jpg';
import pic3 from './Images/n7.jpg';
import pic4 from './Images/n9.jpg';
import './style.css';

export default function BEDashboard() {
  const logout = () => {
    if (!window.confirm("Sure to logout?")) {
      return null;
    }
    alert("Logged out successfully");
    localStorage.clear();
    sessionStorage.clear();
    window.location.href = '/Home';
  }

  return (
    <div>
      <Navbar />
      <div className="container">
        <div className="row">
          <div className="col-lg-6 offset-lg-3">
            <div className="welcome-container text-center">
              <h2 className="text-dark">Welcome Back, {sessionStorage.getItem("UserName")}!</h2>
              <p>You have successfully logged in to your Employee account.</p>
              <div className="mt-4">
                <button className="btn btn-secondary btn-lg" onClick={logout}>Log Out</button>
              </div>
            </div>
          </div>
        </div>
        <div className="row">
          <div className="col-md-6">
            <div className="card transparent-card">
              <img src={pic1} className="card-img-top"/>
              <div className="card-body">
                <h5 className="card-title">Accounts</h5>
                <p className="card-text">Manage Customer Accounts</p>
                <Link to="/BEAccounts" className="btn btn-primary">Accounts</Link>
              </div>
            </div>
          </div>
          <div className="col-md-6">
            <div className="card transparent-card">
              <img src={pic3} className="card-img-top"/>
              <div className="card-body">
              <h5 className="card-title">Loans</h5>
                <p className="card-text">Manage all the Customer Loans</p>
                <Link to="/BELoans" className="btn btn-primary">Loans</Link>
              </div>
            </div>
          </div>
        </div>
        <div className="row">
          <div className="col-md-6">
            <div className="card transparent-card">
              <img src={pic4} className="card-img-top"/>
              <div className="card-body">
              <h5 className="card-title">My Profile</h5>
                <p className="card-text">View your Employee profile. Update the necessary details.</p>
                <Link to="/BEProfile" className="btn btn-primary">My Profile</Link>

              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
