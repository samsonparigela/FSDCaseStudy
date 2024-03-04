import { useState } from "react";
import pic from './Images/m7.jpg';
import './style.css';
import { Navigate, useNavigate } from "react-router-dom";
import Navbar from "./Navbar";
import Defaultbar from "./Defaultbar";

export default function BERegister() {

    const [userName, setUserName] = useState("");
    const [password, setPassword] = useState("");
    const [name, setName] = useState("");
    const [position, setPosition] = useState("");

    const [errorMessage, setErrorMessage] = useState('');

    const navigate = useNavigate();

    const user = {};

    const Register = (e) => {

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

        if (position.length < 5) {
            setErrorMessage("The length of Position is too short");
            return;
        }

        e.preventDefault();

        user.userName = userName;
        user.password = password;
        user.name = name;
        user.position = position;

        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(user)
        };

        fetch("https://localhost:7075/api/BankEmployee/Register", requestOptions)
            .then(response => {
                if (!response.ok) {
                    throw new Error('Invalid Username or Password');
                }
                return response.json();
            })
            .then(data => {
                sessionStorage.setItem("UserName", data.userName);
                alert(`${data.userName} Registered In Successfully`);
                navigate('/BELogin');
            })
            .catch(error => {
                setErrorMessage(error.message);
                console.error('There was an error!', error);
            });
    };

    return (
        <div className='custom-bg-color'>
            <Defaultbar />
            <div className="container">
                <div className="row justify-content-center">
                    <div className="col-lg-6">
                        <div className="login-container">
                            <div className="login-form">
                                <p>&emsp;&emsp;&emsp;&emsp;
                                    &emsp;&emsp;&emsp;&emsp;
                                    Bank Employee</p>
                                <img src={pic} alt="Sign Up" />
                                <form>
                                    <div className="mb-3">
                                        <label htmlFor="username" className="form-label">Username</label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            value={userName}
                                            onChange={(e) => setUserName(e.target.value)}
                                            id="username"
                                            placeholder="Enter your username"
                                            minLength="10"
                                            required
                                        />
                                    </div>
                                    <div className="mb-3">
                                        <label htmlFor="password" className="form-label">Password</label>
                                        <input
                                            type="password"
                                            className="form-control"
                                            value={password}
                                            onChange={(e) => setPassword(e.target.value)}
                                            id="password"
                                            placeholder="Enter your password"
                                            minLength="10"
                                            required
                                        />
                                    </div>
                                    <div className="mb-3">
                                        <label htmlFor="name" className="form-label">Name</label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            value={name}
                                            onChange={(e) => setName(e.target.value)}
                                            id="name"
                                            placeholder="Enter your Name"
                                            minLength="4"
                                            required
                                        />
                                    </div>
                                    <div className="mb-3">
                                        <label htmlFor="position" className="form-label">Position</label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            value={position}
                                            onChange={(e) => setPosition(e.target.value)}
                                            id="position"
                                            placeholder="Enter your Position"
                                            minLength="5"
                                            required
                                        />
                                    </div>
                                    <div className="flex-container">
                                        <div className="flex-child magenta">
                                            <button type="submit" className="btn btn-primary" onClick={Register}>Register</button>
                                        </div>
                                    </div>
                                    <br />
                                    {errorMessage && (
                                        <p className="error">{errorMessage}</p>
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
