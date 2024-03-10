import ViewProfile from './ViewProfile';
import UpdateProfile from './UpdateProfile';
import BENavbar from '../Home/BENavbar';

export default function BEProfile(){
    return(
        <div>
        <BENavbar/>
        <div style={{ display: 'flex' }}>

            <ViewProfile/>
            <UpdateProfile/>
        </div>
        </div>
    )
}