import GetAllAccounts from "./GetAllAccounts";
import GetAllAccountsForOpenRequest from './GetAllAccountsForOpenRequest';
import GetAllAccountsForCloseRequest from './GetAllAccountsForCloseRequest';
import GetAllTransacs from './GetAllTransacs';
import GetAllTransacsByAccount from './GetAllTransacsByAccount';
import GetAllTransacsSent from './GetAllTransacsSent';
import GetAllTransacsRecieved from './GetAllTransacsRecieved';
import GetTopTransacs from './GetTopTransacs';
import GetCustomerForAccount from "./GetCustomerForAccount";

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