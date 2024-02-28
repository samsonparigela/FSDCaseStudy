import pic1 from './Images/m3.jpg';
import pic2 from './Images/m5.jpg';
import pic3 from './Images/m4.jpg';
import './style.css';

export default function Services(){
    return(
        <div>
            <section id="services" class="section">
                <div class="container">
                    <h2 class="section-heading">Our Services</h2>
                    <br/><br/>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="text-center">
                                <i class="fas fa-user service-icon"></i>
                                <h3>Personal Banking</h3>
                                <p>We are India's leading bank for personal savings. The Interest rate we provide is market desirable</p>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="text-center">
                                <i class="fas fa-building service-icon"></i>
                                <h3>Secure Payments</h3>
                                <p>Every transaction you do is private. Not even our employees can see. It goes without saying that they're easy too.</p>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="text-center">
                                <i class="fas fa-money-bill service-icon"></i>
                                <h3>Loans</h3>
                                <p>We understand how much dreams are meant for you. Let us achieve them together with zero to low interest loans.</p>
                            </div>
                        </div>
                    </div>
                </div>
            </section>

            <section id="services" class="section custom-bg-color1">
                <div class="container">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="text-center">
                                <i class="fas fa-user service-icon"></i>
                                <img src={pic1} className = "rounded" alt="jk" width="100%"/>          
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="text-center">
                                <i class="fas fa-building service-icon"></i>
                                <img src={pic2} alt="jk" className = "rounded" width="100%"/>
                            </div>
                        </div>
                        <div class="col-md-4">
                        <div class="text-center">
                            <i class="fas fa-money-bill service-icon"></i>
                            <img src={pic3} alt="jk" className = "rounded" width="100%"/>
                        </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    )
}