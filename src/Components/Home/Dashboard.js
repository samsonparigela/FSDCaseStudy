import React from 'react';
import { Link } from 'react-router-dom';
import Navbar from './Navbar';
import Defaultbar from './Defaultbar';
import pic1 from './Images/n3.jpg';
import pic2 from './Images/n5.jpg';
import pic3 from './Images/n7.jpg';
import pic4 from './Images/n9.jpg';
import './style.css';

export default function Dashboard() {
  var logout = () => {
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
              <p>You have successfully logged in to your account.</p>
              <div className="mt-4">
                <button className="btn btn-secondary btn-lg" onClick={logout}>Log Out</button>
              </div>
            </div>
          </div>
        </div>
        <div className="row">
          <div className="col-md-6">
            <div className="card transparent-card">
              <img src={pic1} className="card-img-top" alt="Card Image" />
              <div className="card-body">
                <h5 className="card-title">Accounts</h5>
                <p className="card-text">Manage your Accounts. Open and close them. View all your Transactions based on various filters.</p>
                <Link to="./Accounts" className="btn btn-primary">Accounts</Link>
              </div>
            </div>
          </div>

          <div className="col-md-6">
            <div className="card transparent-card">
              <img src={pic2} className="card-img-top" alt="Card Image" />
              <div className="card-body">
                <h5 className="card-title">Transactions</h5>
                <p className="card-text">Make all your transactions here. Deposit, Withdraw and Transfer money. View all the transactions done.</p>
                <Link to="./Transactions" className="btn btn-primary">Transactions</Link>
              </div>
            </div>
          </div>
          
          <div className="col-md-6">
          <br/><br/>
            <div className="card transparent-card">
              <img src={pic4} className="card-img-top" alt="Card Image" />
              <div className="card-body">
                <h5 className="card-title">My Profile</h5>
                <p className="card-text">View your customer profile. Update the necessary details.</p>
                <Link to="/Dashboard/Profile" className="btn btn-primary">My Profile</Link>
              </div>
            </div>
          </div>
          <div className="col-md-6">
          <br/><br/>
            <div className="card transparent-card">
              <img src={pic3} className="card-img-top" alt="Card Image" />
              <div className="card-body">
                <h5 className="card-title">Loans</h5>
                <p className="card-text">Apply Loan. Repay Loan. View all the loans you applied and availed. Disburse the money of approved loans to account. View your credit.</p>
                <Link to="./Loans" className="btn btn-primary">Loans</Link>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
