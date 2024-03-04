import Navbar from '../Home/Navbar';
import AddBeneficiaries from './AddBeneficiaries';
import DeleteBeneficiaries from './DeleteBeneficiaries';
import GetAllBeneficiaries from './GetAllBeneficiaries';
import UpdateBeneficiaries from './UpdateBeneficiaries'

export default function Beneficiaries(){
    return(
        <div style={{ width: '100%', backgroundColor: 'lightblue' }}>
            <AddBeneficiaries/>
            <DeleteBeneficiaries/>
            <GetAllBeneficiaries/>
            {/* <UpdateBeneficiaries /> */}
        </div>
    )
}