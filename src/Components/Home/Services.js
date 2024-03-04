import React from 'react';
import './style.css';
import pic1 from './Images/m3.jpg';
import pic2 from './Images/m5.jpg';
import pic3 from './Images/m4.jpg';

export default function Services() {
    return (
        <div>
            <section id="personal-banking" className="section">
                <div className="container">
                    <h2 className="section-heading">Our Services</h2>
                    <br /><br />
                    <div className="row">
                        <div className="col-md-4">
                            <div className="text-center">
                                <i className="fas fa-user service-icon"></i>
                                <h3>Personal Banking</h3>
                                <p>We are India's leading bank for personal savings. The Interest rate we provide is market desirable</p>
                            </div>
                        </div>
                        <div className="col-md-4">
                            <div className="text-center">
                                <i className="fas fa-building service-icon"></i>
                                <h3>Secure Payments</h3>
                                <p>Every transaction you do is private. Not even our employees can see. It goes without saying that they're easy too.</p>
                            </div>
                        </div>
                        <div className="col-md-4">
                            <div className="text-center">
                                <i className="fas fa-money-bill service-icon"></i>
                                <h3>Loans</h3>
                                <p>We understand how much dreams are meant for you. Let us achieve them together with zero to low interest loans.</p>
                            </div>
                        </div>
                    </div>
                </div>
            </section>

            <section id="images" className="section custom-bg-color1">
                <div className="container">
                    <div className="row">
                        <div className="col-md-4">
                            <div className="text-center">
                                <i className="fas fa-user service-icon"></i>
                                <img src={pic1} className="rounded" alt="Service 1" width="100%" />
                            </div>
                        </div>
                        <div className="col-md-4">
                            <div className="text-center">
                                <i className="fas fa-building service-icon"></i>
                                <img src={pic2} className="rounded" alt="Service 2" width="100%" />
                            </div>
                        </div>
                        <div className="col-md-4">
                            <div className="text-center">
                                <i className="fas fa-money-bill service-icon"></i>
                                <img src={pic3} className="rounded" alt="Service 3" width="100%" />
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    );
}
 