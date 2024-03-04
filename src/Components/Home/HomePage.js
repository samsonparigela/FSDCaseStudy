import WelcomeCard from './WelcomeCard'
import Services from './Services'
import About from './About'
import Contact from './Contact'
import Navbar from './Navbar'
import Defaultbar from './Defaultbar'
import footer from './Footer';
import Footer from './Footer'

export default function HomePage(){
    // var isLoggedIn = sessionStorage.getItem("Token");
    return(
        <div className="jj">
            <Defaultbar/>
            {/* {isLoggedIn?<Navbar />:<Defaultbar />} */}
            <WelcomeCard />
            <Services />
            <About />
            <Contact />
            <Footer/>
        </div>
    )
}