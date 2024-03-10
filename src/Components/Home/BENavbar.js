import React from 'react';
import logo from './Images/logo.jpeg'; // Import your logo image

export default function BENavbar() {
    const logout = () => {
        if (!window.confirm("Sure to logout?")) {
          return null;
        }
        alert("Logged out successfully");
        localStorage.clear();
        sessionStorage.clear();
        window.location.href = '/Home';
    };

    return (
        <nav className="navbar navbar-expand-lg navbar-light custom-bg-color2">
            <div className="container-fluid d-flex align-items-center">
                <a className="navbar-brand d-flex align-items-center" href="www.sbi.com">
                    <img src={logo} alt="Maverick Bank Logo" width="70" height="70" className="mr-2" />
                    <span className="text-center">Maverick Bank</span> {/* Center the title */}
                </a>
                <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="collapse navbar-collapse" id="navbarNav">
                    <ul className="navbar-nav ml-auto">
                        <li className="nav-item active">
                            <a className="nav-link" href="http://localhost:3000/BEDashboard/">Dashboard</a>
                        </li>
                        <li className="nav-item active">
                            <button className="nav-link btn" onClick={logout}>Logout</button>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
    );
}
