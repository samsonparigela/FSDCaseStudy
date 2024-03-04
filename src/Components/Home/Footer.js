import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faFacebook, faTwitter, faInstagram, faLinkedin } from '@fortawesome/free-brands-svg-icons';

const Footer = () => {
    return (
        <footer className="bg-dark text-light py-5">
            <div className="container">
                <div className="row">
                    <div className="col-md-6">
                        <h5>About Us</h5>
                        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus sit amet eleifend lectus, ac posuere quam.</p>
                    </div>
                    <div className="col-md-6 text-right"> {/* Push to right */}
                        <h5>Connect with Us</h5>
                        <ul className="list-inline">
                            <li className="list-inline-item">
                                <a href="https://www.facebook.com/">
                                    <FontAwesomeIcon icon={faFacebook} size="lg" />
                                </a>
                            </li>
                            <li className="list-inline-item">
                                <a href="https://twitter.com/">
                                    <FontAwesomeIcon icon={faTwitter} size="lg" />
                                </a>
                            </li>
                            <li className="list-inline-item">
                                <a href="https://www.instagram.com/">
                                    <FontAwesomeIcon icon={faInstagram} size="lg" />
                                </a>
                            </li>
                            <li className="list-inline-item">
                                <a href="https://www.linkedin.com/">
                                    <FontAwesomeIcon icon={faLinkedin} size="lg" />
                                </a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
            <div className="text-center mt-3">
                <p>&copy; 2024 Maverick Bank. All rights reserved.</p>
            </div>
        </footer>
    );
};

export default Footer;
