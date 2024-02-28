import { useState } from "react";
import pic from './Images/m7.jpg';
import './style.css';
import { Navigate, useNavigate } from "react-router-dom";

export default function CustomerRegister()
{

    var [userName,setUserName] = useState("");
    var [password,setPassword] = useState("");
    var [name,setName] = useState("");
    var [dob,setDOB] = useState("");
    var [age,setAge] = useState("");
    var [phone,setPhone] = useState("");
    var [address,setAddress] = useState("");
    var [gender,setGender] = useState("");
    var [aadhaar,setAadhaar] = useState("");
    var [panNumber,setPanNumber] = useState("");

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

        if(phone.toString().length<4){
            setErrorMessage("The length of Phone Number is too short");
            return;
        }

        if(panNumber.length<10){
            setErrorMessage("The length of Pan Number is too short");
            return;
        }

        if(aadhaar.length<4){
            setErrorMessage("The length of Aadhaar number is too short");
            return;
        }


        e.preventDefault();
        user.userName=userName;
        user.password=password;
        user.name=name;
        user.dob=dob;
        user.age=age;
        user.phone=phone;
        user.address=address;
        user.gender=gender;
        user.aadhaar=aadhaar;
        user.panNumber=panNumber;

        var requestOptions = {
            method:'POST',
            headers: {'Content-Type':'application/json'},
            body: JSON.stringify(user)
        };
        console.log(user);
        fetch("https://localhost:7075/api/Customer/Register",requestOptions)
        .then(r=>r.json())
        .then(r=>{
            sessionStorage.setItem("UserName",r.userName);
            alert(r.userName+" Registered In Successfully");
            navigate('/Login');
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
                             Customer</p>
                            <img src={pic} alt="Sign Up Image"/>
                                <form>
                                    <div class="mb-3">
                                        <label for="username" class="form-label">Username</label>
                                        <input type="text" class="form-control" value={userName} onChange={(e)=>setUserName(e.target.value)}
                                        id="username" placeholder="Enter your username" minlength="8" required/>
                                    </div>
                                    <div class="mb-3">
                                        <label for="username" class="form-label">Password</label>
                                        <input type="password" class="form-control" id="password" placeholder="Enter your password"
                                        value={password} minlength="8" required onChange={(e)=>setPassword(e.target.value)} />
                                    </div>
                                    <div class="mb-3">
                                        <label for="name" class="form-label">Name</label>
                                        <input type="text" class="form-control" value={name} onChange={(e)=>setName(e.target.value)}
                                        id="name" placeholder="Enter your Name" minlength="4" required/>
                                    </div>
                                    <div class="mb-3">
                                        <label for="dob" class="form-label">Date of birth</label>
                                        <input type="date" class="form-control" value={dob} onChange={(e)=>setDOB(e.target.value)}
                                        id="dob" placeholder="Enter your DOB" required/>
                                    </div>
                                    <div class="mb-3">
                                        <label for="age" class="form-label">Age</label>
                                        <input type="number" class="form-control" value={age} onChange={(e)=>setAge(e.target.value)}
                                        id="age" placeholder="Enter your Age" required/>
                                    </div>
                                    <div class="mb-3">
                                        <label for="phone" class="form-label">Phone Number</label>
                                        <input type="text" class="form-control" value={phone} onChange={(e)=>setPhone(e.target.value)}
                                        id="phone" placeholder="Enter your Phone" minlength="10" required/>
                                    </div>
                                    <div class="mb-3">
                                        <label for="address" class="form-label">Address</label>
                                        <input type="text" class="form-control" value={address} onChange={(e)=>setAddress(e.target.value)}
                                        id="address" placeholder="Enter your Address" required/>
                                    </div>
                                    <div class="mb-3">
                                        <label for="gender" class="form-label">Gender</label>
                                        <input type="text" class="form-control" value={gender} onChange={(e)=>setGender(e.target.value)}
                                        id="gender" placeholder="Enter your Gender" required/>
                                    </div>
                                    <div class="mb-3">
                                        <label for="aadhaarNumber" class="form-label">Aadhaar Number</label>
                                        <input type="text" class="form-control" value={aadhaar} onChange={(e)=>setAadhaar(e.target.value)}
                                        id="aadhaarNumber" placeholder="Enter your Aadhaar Number" minlength="10" required/>
                                    </div>
                                    <div class="mb-3">
                                        <label for="panNumber" class="form-label">Pan Number</label>
                                        <input type="text" class="form-control" value={panNumber} onChange={(e)=>setPanNumber(e.target.value)}
                                        id="panNumber" placeholder="Enter your PAN Number" required minlength="10"/>
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





