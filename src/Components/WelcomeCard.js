import './style.css';
import pic1 from './Images/jj66.jpg'
export default function WelcomeCard(){
    return(
        <div class="jumbotron jumbotron-fluid">
            <div class="container">
            <img src={pic1} alt="Image" width={300} class="img-fluid float-right rounded"></img>
                <h1 class="display-4">Welcome to Maverick Bank</h1>
                <p class="lead">"Your Trust, Our Promise."</p>
                <a class="btn btn-info btn-lg" href="/CustomerRegister" role="button">Sign Up</a>
            </div>
            
        </div>
    )
}