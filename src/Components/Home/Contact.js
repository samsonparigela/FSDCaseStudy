import './style.css';

export default function Contact(){
    return(
        <div>
            <section id="contact" class="py-5 custom-bg-color">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-8 mx-auto text-center">
                            <h2 class="display-4">Contact Us</h2>
                            <p class="lead">Have questions? We're here to help. Reach out to us using the form below or contact us directly.</p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6 mx-auto">
                            <form>
                                <div class="mb-3">
                                    <label for="name" class="form-label">Name</label>
                                    <input type="text" class="form-control" id="name" placeholder="Enter your name"/>
                                </div>
                                <div class="mb-3">
                                    <label for="email" class="form-label">Email</label>
                                    <input type="email" class="form-control" id="email" placeholder="Enter your email"/>
                                </div>
                                <div class="mb-3">
                                    <label for="message" class="form-label">Message</label>
                                    <textarea class="form-control" id="message" rows="3" placeholder="Enter your message"></textarea>
                                </div>
                                <button type="submit" class="btn btn-primary">Submit</button>
                            </form>
                        </div>
                        <div class="col-lg-4 mx-auto">
                            <h4>Contact Information</h4>
                            <ul class="list-unstyled">
                                <li><strong>Address:</strong> 123 Madhapur, Hyderabad, India</li>
                                <li><strong>Phone:</strong> +91 (123) 456-7890</li>
                                <li><strong>Email:</strong> mavericbank@gmail.com</li>
                            </ul>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    )
}