import ViewProfile from './ViewProfile';
import UpdateProfile from './UpdateProfile';

export default function BEProfile(){
    return(
        <div style={{ display: 'flex' }}>
            <ViewProfile/>
            <UpdateProfile/>
        </div>
    )
}