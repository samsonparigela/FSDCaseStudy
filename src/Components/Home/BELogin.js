import { useState } from "react";
import pic from './Images/m10.jpg';
import './style.css';
import { Navigate, useNavigate } from "react-router-dom";
import Gaps from "./Gaps";
import ForgotPassword from "./ForgotPassword";

export default function BELogin()
{

    var [userName,setUserName] = useState("");
    var [password,setPassword] = useState("");
    var [isLoggedIn,setIsLoggedIn] = useState("false");
    const [errorMessage, setErrorMessage] = useState('');


    var navigate=useNavigate();

    var user={};
    var Logins = (e) =>{
        e.preventDefault();
        user.userName=userName;
        user.password=password;
        user.userType="";
        user.token="";
        user.userID=0;

        var requestOptions = {
            method:'POST',
            headers: {'Content-Type':'application/json'},
            body: JSON.stringify(user)
        };
        fetch("https://localhost:7075/api/BankEmployee/Login",requestOptions)
        .then(r=>r.json())
        .then(r=>{

            sessionStorage.setItem("UserName",r.userName);
            sessionStorage.setItem("Token",r.token);
            sessionStorage.setItem("BID",r.userID);
            setIsLoggedIn(true);
            sessionStorage.setItem("IsLoggedIn",true);
            alert("Logged In Successfully "+r.userName);
            navigate('/BEDashboard');
        })
        .catch(e=>{
            setErrorMessage("Invalid Username or Password "+e.message);
            setIsLoggedIn(false);
        })
    };
    return(
        <div class='custom-bg-color'>
        <div class="container">
            <div class="row justify-content-center">
                <div class="col-lg-6">
                    <div class="login-container">
                        <div class="login-form">
                        <p>&emsp;&emsp;&emsp;&emsp;
                            &emsp;&emsp;&emsp;&emsp;
                             Bank Employee</p>
                            <img src={pic} alt="Login Image"/>
                                <form>
                                    <div class="mb-3">
                                        <label for="username" class="form-label">Username</label>
                                        <input type="text" class="form-control" value={userName} onChange={(e)=>setUserName(e.target.value)}
                                        id="username" placeholder="Enter your username"/>
                                    </div>
                                    <div class="mb-3">
                                        <label for="password" class="form-label">Password</label>
                                        <input type="password" class="form-control" id="password" placeholder="Enter your password"
                                        value={password} onChange={(e)=>setPassword(e.target.value)}/>
                                    </div>
                                    <div class="flex-container">
                                    <div class="flex-child magenta">
                                    <button type="submit" class="btn btn-primary" onClick={Logins}>Login</button>
                                    </div>
                                    
<div class="flex-child green">
        <p className="form__hint"><a className="form__link" href="/ForgotPassword">Forgot password?</a></p>
      </div>
      </div>
      <br/>
      {errorMessage && (
  <p className="error"> {errorMessage} </p>
)}
                                </form>
                        </div>
                    </div>
                </div>
            </div>
            <div class='custom-bg-color1'>
            &nbsp;
        </div>
        <Gaps />
        </div>
        </div>
    )
}





