import { useState } from "react";
import pic from './Images/m7.jpg';
import './style.css';
import { useNavigate } from "react-router-dom";
import Navbar from "./Navbar";
import Defaultbar from "./Defaultbar";

export default function CustomerRegister() {
    const [userName, setUserName] = useState("");
    const [password, setPassword] = useState("");
    const [name, setName] = useState("");
    const [dob, setDOB] = useState("");
    const [age, setAge] = useState("");
    const [phone, setPhone] = useState("");
    const [address, setAddress] = useState("");
    const [gender, setGender] = useState("");
    const [aadhaar, setAadhaar] = useState("");
    const [panNumber, setPanNumber] = useState("");

    const [errorMessage, setErrorMessage] = useState('');
    const navigate = useNavigate();

    const Register = (e) => {
        e.preventDefault();

        // Validation checks
        if (userName.length < 8) {
            setErrorMessage("The length of username is too short");
            return;
        }
        if (password.length < 8) {
            setErrorMessage("The length of password is too short");
            return;
        }
        if (name.length < 4) {
            setErrorMessage("The length of Name is too short");
            return;
        }
        if (phone.toString().length < 10) {
            setErrorMessage("The length of Phone Number is too short");
            return;
        }
        if (panNumber.length < 10) {
            setErrorMessage("The length of Pan Number is too short");
            return;
        }
        if (aadhaar.length < 4) {
            setErrorMessage("The length of Aadhaar number is too short");
            return;
        }

        // Prepare user object
        const user = {
            userName,
            password,
            name,
            dob,
            age,
            phone,
            address,
            gender,
            aadhaar,
            panNumber
        };

        // Perform registration
        fetch("https://localhost:7075/api/Customer/Register", {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(user)
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Invalid Username or Password');
                }
                return response.json();
            })
            .then(data => {
                sessionStorage.setItem("UserName", data.userName);
                alert(`${data.userName} Registered In Successfully`);
                navigate('/Login');
            })
            .catch(error => {
                setErrorMessage(error.message);
                console.error('There was an error!', error);
            });
    };

    return (
        <div className='custom-bg-color'>
            <Defaultbar/>
            <div className="container">
                <div className="row justify-content-center">
                    <div className="col-lg-6">
                        <div className="login-container">
                            <div className="login-form">
                                <p>&emsp;&emsp;&emsp;&emsp;&emsp;
                                &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Customer</p>
                                <img src={pic} alt="Sign Up Image" />
                                <form onSubmit={Register}>
                                    <div className="mb-3">
                                        <label htmlFor="username" className="form-label">Username</label>
                                        <input type="text" className="form-control" value={userName} onChange={(e) => setUserName(e.target.value)}
                                            id="username" placeholder="Enter your username" minLength="8" required />
                                    </div>
                                    <div className="mb-3">
                                        <label htmlFor="password" className="form-label">Password</label>
                                        <input type="password" className="form-control" id="password" placeholder="Enter your password"
                                            value={password} minLength="8" required onChange={(e) => setPassword(e.target.value)} />
                                    </div>
                                    <div className="mb-3">
                                        <label htmlFor="name" className="form-label">Name</label>
                                        <input type="text" className="form-control" value={name} onChange={(e) => setName(e.target.value)}
                                            id="name" placeholder="Enter your Name" minLength="4" required />
                                    </div>
                                    <div className="mb-3">
                                        <label htmlFor="dob" className="form-label">Date of birth</label>
                                        <input type="date" className="form-control" value={dob} onChange={(e) => setDOB(e.target.value)}
                                            id="dob" placeholder="Enter your DOB" required />
                                    </div>
                                    <div className="mb-3">
                                        <label htmlFor="age" className="form-label">Age</label>
                                        <input type="number" className="form-control" value={age} onChange={(e) => setAge(e.target.value)}
                                            id="age" placeholder="Enter your Age" required />
                                    </div>
                                    <div className="mb-3">
                                        <label htmlFor="phone" className="form-label">Phone Number</label>
                                        <input type="tel" className="form-control" value={phone} onChange={(e) => setPhone(e.target.value)}
                                            id="phone" placeholder="Enter your Phone" minLength="10" required />
                                    </div>
                                    <div className="mb-3">
                                        <label htmlFor="address" className="form-label">Address</label>
                                        <input type="text" className="form-control" value={address} onChange={(e) => setAddress(e.target.value)}
                                            id="address" placeholder="Enter your Address" required />
                                    </div>
                                    <div className="mb-3">
                                        <label htmlFor="gender" className="form-label">Gender</label>
                                        <input type="text" className="form-control" value={gender} onChange={(e) => setGender(e.target.value)}
                                            id="gender" placeholder="Enter your Gender" required />
                                    </div>
                                    <div className="mb-3">
                                        <label htmlFor="aadhaarNumber" className="form-label">Aadhaar Number</label>
                                        <input type="text" className="form-control" value={aadhaar} onChange={(e) => setAadhaar(e.target.value)}
                                            id="aadhaarNumber" placeholder="Enter your Aadhaar Number" minLength="10" required />
                                    </div>
                                    <div className="mb-3">
                                        <label htmlFor="panNumber" className="form-label">Pan Number</label>
                                        <input type="text" className="form-control" value={panNumber} onChange={(e) => setPanNumber(e.target.value)}
                                            id="panNumber" placeholder="Enter your PAN Number" required minLength="10" />
                                    </div>
                                    <div className="flex-container">
                                        <div className="flex-child magenta">
                                            <button type="submit" className="btn btn-primary">Register</button>
                                        </div>
                                    </div>
                                    <br />
                                    {errorMessage && (
                                        <p className="error"> {errorMessage} </p>
                                    )}
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}
