import React from 'react';
import './style.css';

export default function Contact() {
    return (
        <div>
            <section id="contact" className="py-5 custom-bg-color">
                <div className="container">
                    <div className="row">
                        <div className="col-lg-8 mx-auto text-center">
                            <h2 className="display-4">Contact Us</h2>
                            <p className="lead">Have questions? We're here to help. Reach out to us using the form below or contact us directly.</p>
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-lg-6 mx-auto">
                            <form>
                                <div className="mb-3">
                                    <label htmlFor="name" className="form-label">Name</label>
                                    <input type="text" className="form-control" id="name" placeholder="Enter your name" />
                                </div>
                                <div className="mb-3">
                                    <label htmlFor="email" className="form-label">Email</label>
                                    <input type="email" className="form-control" id="email" placeholder="Enter your email" />
                                </div>
                                <div className="mb-3">
                                    <label htmlFor="message" className="form-label">Message</label>
                                    <textarea className="form-control" id="message" rows="3" placeholder="Enter your message"></textarea>
                                </div>
                                <button type="submit" className="btn btn-primary">Submit</button>
                            </form>
                        </div>
                        <div className="col-lg-4 mx-auto">
                            <h4>Contact Information</h4>
                            <ul className="list-unstyled">
                                <li><strong>Address:</strong> 123 Madhapur, Hyderabad, India</li>
                                <li><strong>Phone:</strong> +91 (123) 456-7890</li>
                                <li><strong>Email:</strong> mavericbank@gmail.com</li>
                            </ul>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    );
}
