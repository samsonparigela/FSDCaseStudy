import React from 'react';
import './style.css';
import pic1 from './Images/jj66.jpg';

export default function WelcomeCard() {
    return (
        <div className="jumbotron jumbotron-fluid">
            <div className="container">
                <img src={pic1} alt="Image" width={300} className="img-fluid float-right rounded" />
                <h1 className="display-4">Welcome to Maverick Bank</h1>
                <p className="lead">"Your Trust, Our Promise."</p>
                <a className="btn btn-info btn-lg" href="/CustomerRegister" role="button">Sign Up</a>
            </div>
        </div>
    );
}
