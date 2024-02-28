import { useState } from "react";
import pic from './Images/m7.jpg';
import './style.css';
import { Navigate, useNavigate } from "react-router-dom";

export default function BERegister()
{

    var [userName,setUserName] = useState("");
    var [password,setPassword] = useState("");
    var [name,setName] = useState("");
    var [position,setPosition] = useState("");

    var [isLoggedIn,setIsLoggedIn] = useState("false");
    const [errorMessage, setErrorMessage] = useState('');

    var navigate=useNavigate();

    var user={};
    var Register = (e) =>{

        if(userName.length<8){
            setErrorMessage("The length of username is too short");
            return;
        }
        
        if(password.length<8){
            setErrorMessage("The length of password is too short");
            return;
        }

        if(name.length<4){
            setErrorMessage("The length of Name is too short");
            return;
        }

        if(position.length<5){
            setErrorMessage("The length of Phone Number is too short");
            return;
        }


        e.preventDefault();
        user.userName=userName;
        user.password=password;
        user.name=name;
        user.position=position;;

        var requestOptions = {
            method:'POST',
            headers: {'Content-Type':'application/json'},
            body: JSON.stringify(user)
        };
        console.log(user);
        fetch("https://localhost:7075/api/BankEmployee/Register",requestOptions)
        .then(r=>r.json())
        .then(r=>{
            sessionStorage.setItem("UserName",r.userName);
            alert(r.userName+" Registered In Successfully");
            navigate('/BELogin');
        })
        .catch(e=>{
            setErrorMessage("Invalid Username or Password");
            console.log(e);
            console.log("Ayya");
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
                            <img src={pic} alt="Sign Up Image"/>
                                <form>
                                    <div class="mb-3">
                                        <label for="username" class="form-label">Username</label>
                                        <input type="text" class="form-control" value={userName} onChange={(e)=>setUserName(e.target.value)}
                                        id="username" placeholder="Enter your username" minlength="10" required/>
                                    </div>
                                    <div class="mb-3">
                                        <label for="password" class="form-label">Password</label>
                                        <input type="password" class="form-control" id="password" placeholder="Enter your password"
                                        value={password} onChange={(e)=>setPassword(e.target.value)} minlength="10" required/>
                                    </div>
                                    <div class="mb-3">
                                        <label for="username" class="form-label">Name</label>
                                        <input type="text" class="form-control" value={name} onChange={(e)=>setName(e.target.value)}
                                        id="name" placeholder="Enter your Name" minlength="4" required/>
                                    </div>
                                    <div class="mb-3">
                                        <label for="username" class="form-label">Position</label>
                                        <input type="text" class="form-control" value={position} onChange={(e)=>setPosition(e.target.value)}
                                        id="position" placeholder="Enter your Position" minlength="5" required/>
                                    </div>
                                    <div class="flex-container">
                                    <div class="flex-child magenta">
                                    <button type="submit" class="btn btn-primary" onClick={Register}>Register</button>
                                    </div>
      </div>
      <br/>
      {errorMessage && (
  <p className="error"> {errorMessage} </p>)}
                                </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </div>

    )
}





