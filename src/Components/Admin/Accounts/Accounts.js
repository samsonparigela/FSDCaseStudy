import GetAllAccounts from "../../BEAccount/GetAllAccounts";
import GetAllAccountsForOpenRequest from '../../BEAccount/GetAllAccountsForOpenRequest';
import GetAllAccountsForCloseRequest from '../../BEAccount/GetAllAccountsForCloseRequest';
import GetAllTransacs from '../../BEAccount/GetAllTransacs';
import GetAllTransacsByAccount from '../../BEAccount/GetAllTransacsByAccount';
import GetAllTransacsSent from '../../BEAccount/GetAllTransacsSent';
import GetAllTransacsRecieved from '../../BEAccount/GetAllTransacsRecieved';
import GetTopTransacs from '../../BEAccount/GetTopTransacs';
import GetCustomerForAccount from "../../BEAccount/GetCustomerForAccount";

export default function Accounts(){
    return(
        <div>
            <div style={{ display: 'flex' }}>
            <GetAllAccounts />
            </div>
            <div style={{ display: 'flex' }}>
            <GetTopTransacs />
            </div>
            <div style={{ display: 'flex' }}>
            <GetAllAccountsForOpenRequest />
            </div>
            <div style={{ display: 'flex' }}>
            <GetAllAccountsForCloseRequest />
            </div>
            <div style={{ display: 'flex' }}>
            <GetAllTransacs />
            </div>
            <div style={{ display: 'flex' }}>
            <GetAllTransacsByAccount />
            </div>
            <div style={{ display: 'flex' }}>
            <GetAllTransacsSent />
            </div>
            <div style={{ display: 'flex' }}>
            <GetAllTransacsRecieved />
            </div>
            <div style={{ display: 'flex' }}>
            <GetCustomerForAccount />
            </div>
        </div>
    )
}