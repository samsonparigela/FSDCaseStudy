import ViewProfile from './ViewProfile';
import UpdateProfile from './UpdateProfile';

export default function Profile(){
    return(
        <div>
            <div style={{ display: 'flex' }}>
                <ViewProfile/>
            </div>
            <div style={{ display: 'flex' }}>
                <UpdateProfile/>
            </div>
        </div>
    )
}