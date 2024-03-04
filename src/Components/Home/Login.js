import { useState } from "react";
import pic from './Images/m10.jpg';
import { useNavigate } from "react-router-dom";
import Defaultbar from "./Defaultbar";

export default function Login() {
    const [userName, setUserName] = useState("");
    const [password, setPassword] = useState("");
    const [errorMessage, setErrorMessage] = useState('');

    const navigate = useNavigate();

    const Logins = (e) => {
        e.preventDefault();

        const user = {
            userName,
            password,
            userType: "",
            token: "",
            userID: 0
        };

        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(user)
        };

        fetch("https://localhost:7075/api/Customer/Login", requestOptions)
            .then(response => {
                if (!response.ok) {
                    throw new Error('Invalid Username or Password');
                }
                return response.json();
            })
            .then(data => {
                sessionStorage.setItem("UserName", data.userName);
                sessionStorage.setItem("Token", data.token);
                sessionStorage.setItem("CID", data.userID);
                sessionStorage.setItem("IsLoggedIn", true);
                alert("Logged In Successfully " + data.userName);
                navigate('/Dashboard');
            })
            .catch(error => {
                setErrorMessage(error.message);
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
                                <p>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;Customer</p>
                                <img src={pic} alt="Login Image" />
                                <form>
                                    <div className="mb-3">
                                        <label htmlFor="username" className="form-label">Username</label>
                                        <input type="text" className="form-control" value={userName} onChange={(e) => setUserName(e.target.value)}
                                            id="username" placeholder="Enter your username" />
                                    </div>
                                    <div className="mb-3">
                                        <label htmlFor="password" className="form-label">Password</label>
                                        <input type="password" className="form-control" id="password" placeholder="Enter your password"
                                            value={password} onChange={(e) => setPassword(e.target.value)} />
                                    </div>
                                    <div className="flex-container">
                                        <div className="flex-child magenta">
                                            <button type="submit" className="btn btn-primary" onClick={Logins}>Login</button>
                                        </div>
                                        <div className="flex-child green">
                                            <p className="form__hint"><a className="form__link" href="/ForgotPassword">Forgot password?</a></p>
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
