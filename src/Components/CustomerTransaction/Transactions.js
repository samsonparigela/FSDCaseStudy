import ViewAllYourTransacs from './ViewAllYourTransacs';
import TransferMoney from './TransferMoney';
import WithdrawMoney from './WithdrawMoney';
import DepositMoney from './DepositMoney';
import Navbar from '../Home/Navbar';


export default function Transactions(){
    return(
        <div style={{ width: '100%', backgroundColor: 'lightblue' }}>
            <Navbar/>
            <br/>
            <DepositMoney />
            <WithdrawMoney />
            <TransferMoney />
            <ViewAllYourTransacs />
        </div>
    )
}