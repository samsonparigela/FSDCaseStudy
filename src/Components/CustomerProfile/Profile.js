import ViewProfile from './ViewProfile';
import UpdateProfile from './UpdateProfile';
import Beneficiaries from '../CustomerBeneficiary/Beneficiaries';
import Navbar from '../Home/Navbar';
import TransactionsPie from './TransactionsPie';

export default function Profile(){
    return(
        <div>
            <Navbar/>
            <div style={{ display: 'flex' }}>
                <ViewProfile/>
            </div>
            <div style={{ display: 'flex' }}>
                <UpdateProfile/>
            </div>
            <div>
                <Beneficiaries/>
            </div>
            <div style={{ width: '100%', backgroundColor: 'lightblue' }}>
                <TransactionsPie/>

            </div>
        </div>
    )
}