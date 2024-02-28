import ViewAllYourTransacs from './ViewAllYourTransacs';
import TransferMoney from './TransferMoney';
import WithdrawMoney from './WithdrawMoney';
import DepositMoney from './DepositMoney';


export default function Transactions(){
    return(
        <div style={{ width: '100%', backgroundColor: 'lightblue' }}>
            <br/>
            <DepositMoney />
            <WithdrawMoney />
            <TransferMoney />
            <ViewAllYourTransacs />
        </div>
    )
}