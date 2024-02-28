import './style.css';
import pic from './Images/M3.jpeg'
export default function About(){
    return(
        <div>
              <section id="about" class="py-5 custom-bg-color2">
    <div class="container">
      <div class="row">
        <div class="col-lg-6">
          <h2 class="display-4">About Our Bank</h2>
          <p class="lead">At Maverick Bank, we're committed to empowering our customers to achieve their financial goals. With a history spanning over 50 years, we've built a reputation for trust, reliability, and innovation in banking services.</p>
          <p>We offer a comprehensive range of financial products and services tailored to meet the diverse needs of individuals, families, and businesses. Whether you're looking to save for the future, invest wisely, or secure a loan, our team of dedicated professionals is here to help you every step of the way.</p>
          <p>As a community-focused bank, we believe in giving back. Through our philanthropic efforts and community outreach programs, we strive to make a positive impact in the areas we serve.</p>
          <a href="#" class="btn btn-info btn-lg">Learn More</a>
        </div>
        <div class="col-lg-6">
        <br/><br/><br/><br/>
          <img src={pic} class="img-fluid square rounded" alt="About Image" width="1000px" />
        </div>
      </div>
    </div>
  </section>


<br/>
        </div>
    )
}