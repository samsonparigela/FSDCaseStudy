import WelcomeCard from './WelcomeCard'
import Services from './Services'
import About from './About'
import Contact from './Contact'
import Defaultbar from './Defaultbar'
import Footer from './Footer'

export default function HomePage(){
    return(
        <div className="jj">
            <Defaultbar/>
            <WelcomeCard />
            <Services />
            <About />
            <Contact />
            <Footer/>
        </div>
    )
}