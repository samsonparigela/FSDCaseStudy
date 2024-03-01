import WelcomeCard from './WelcomeCard'
import Services from './Services'
import About from './About'
import Contact from './Contact'
import Navbar from './Navbar'
import Defaultbar from './Defaultbar'

export default function HomePage(){
    // var isLoggedIn = sessionStorage.getItem("Token");
    return(
        <div class="jj">
            {/* {isLoggedIn?<Navbar />:<Defaultbar />} */}
            <WelcomeCard />
            <Services />
            <About />
            <Contact />
        </div>
    )
}