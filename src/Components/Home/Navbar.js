import React from 'react';

export default function Navbar() {
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
            <nav className="navbar navbar-expand-lg navbar-light custom-bg-color2">
                <div className="container">
                    <a className="navbar-brand" href="google.com">Maverick Bank</a>
                    <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="collapse navbar-collapse" id="navbarNav">
                        <ul className="navbar-nav ml-auto">
                            <li className="nav-item active">
                                <a className="nav-link" href="http://localhost:3000/Dashboard/">Dashboard</a>
                            </li>
                            <li className="nav-item active">
                                <a className="nav-link" onClick={logout}>Logout</a>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
        </div>
    );
}
