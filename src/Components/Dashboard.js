import './Styles/dashboard.css';
import pic1 from './Images/n3.jpg';
import pic2 from './Images/n5.jpg';
import pic3 from './Images/n7.jpg';
import pic4 from './Images/n9.jpg';
import { Link } from 'react-router-dom';
import Navbar from './Navbar';
import Defaultbar from './Defaultbar';
export default function Dashboard(){
  var logout = ()=> {
    alert("Logged out successfully");
    localStorage.clear();
    sessionStorage.clear();
    
    window.location.href = '/Home';

}
// var isLoggedIn = true;
    return(
        <div class="j">
              {/* {isLoggedIn?<Navbar />:<Defaultbar />} */}
            <div class="navbar-toggle-icon">
  <div class="row">
    <div class="col-lg-6 offset-lg-3">
      <div class="welcome-container text-center">
        <h2 class="text-dark">Welcome Back, {sessionStorage.getItem("UserName")}!</h2>
        <p>You have successfully logged in to your account.</p>
        <div class="mt-4">
          <button href="#" class="btn btn-secondary btn-lg" onClick={logout}>Log Out</button>
        </div>
      </div>
    </div>
  </div>
</div>


<div class="container">
  <div class="row">
    <div class="col-md-6">
      <div class="card transparent-card">
        <div class="row g-0">
          <div class="col-md-5">
            <img src={pic1} class="" width="240%" alt="Card Image"/>
          </div>
          <div class="col-md-8">
            <div class="card-body">
              <h5 class="card-title">Accounts</h5>
              <p class="card-text">Manage your Accounts. Open and close them. View all your Transactions based on various filters.</p>
              <Link to="./Accounts" class="btn btn-primary">Accounts</Link>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="col-md-6">
      <div class="card transparent-card">
        <div class="row g-0">
          <div class="col-md-5">
            <img src={pic2} class="" width="240%" alt="Card Image"/>
          </div>
          <div class="col-md-8">
            <div class="card-body">
              <h5 class="card-title">Transactions</h5>
              <p class="card-text">Make all your transactions here. Deposit, Withdraw and Transfer money. View all the transactions done.</p>
              <Link to="./Transactions" class="btn btn-primary">Transactions</Link>
            </div>
          </div>
        </div>
      </div>
    </div>

  </div>
</div>
<br/><br/><br/><br/>
<div class="container">
  <div class="row">
    <div class="col-md-6">
      <div class="card transparent-card">
        <div class="row g-0">
          <div class="col-md-5">
            <img src={pic4} class="" width="240%" alt="Card Image"/>
          </div>
          <div class="col-md-8">
            <div class="card-body">
              <h5 class="card-title">My Profile</h5>
              <p class="card-text">View your customer profile. Update the necessary details.</p>
              <Link to="/Dashboard/Profile" class="btn btn-primary">My Profile</Link>
            </div>
          </div>
        </div>
      </div>
    </div>

    <div class="col-md-6">
      <div class="card transparent-card">
        <div class="row g-0">
          <div class="col-md-5">
            <img src={pic3} class="" width="240%" alt="Card Image"/>
          </div>
          <div class="col-md-8">
            <div class="card-body">
              <h5 class="card-title">Loans</h5>
              <p class="card-text">Apply Loan. Repay Loan. View all the loans you applied and availed. Disburse the money of approved loans to account. View your credit.</p>
              <Link to="./Loans" class="btn btn-primary">Loans</Link>
            </div>
          </div>
        </div>
      </div>
    </div>

  </div>
</div>
        </div>
    )
}