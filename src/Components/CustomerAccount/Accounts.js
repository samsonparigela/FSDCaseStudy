import OpenAccount from "./OpenAccount.js"
import CloseAccount from "./CloseAccount.js"
import GetAllAccounts from "./GetAllAccounts.js"
import ViewAllTransacsByAcc from "./ViewAllTransacsByAcc.js"
import ViewLastNTransacs from "./ViewLastNTransacs.js"
import ThisMonthTransacs from "./ThisMonthTransacs.js"
import LastMonthTransacs from "./LastMonthTransacs.js"
import ViewAllYourTransacs from "./ViewAllYourTransacs.js"
import TransacsToAcc from "./TransacsToAcc.js"
import ViewAllBanks from "./ViewAllBanks.js"
import ViewAllBranches from "./ViewAllBranches.js"
import GetAllTransacsSent from "./GetAllTransacsSent.js"
import GetAllTransacsRecieved from "./GetAllTransacsRecieved.js"
import Navbar from "../Home/Navbar.js"

export default function Accounts(){
    return(
        <div>
            <Navbar/>
        <div style={{ display: 'flex' }}>
            <GetAllAccounts />
        </div>
        <div style={{ display: 'flex' }}>
            <ViewAllTransacsByAcc />
        </div>
        <div style={{ display: 'flex' }}>
            <ViewAllYourTransacs />
        </div>
        <div style={{ display: 'flex' }}>
            <ViewLastNTransacs />
        </div>
        <div style={{ display: 'flex' }}>
            <ThisMonthTransacs />
        </div>
        <div style={{ display: 'flex' }}>
            <LastMonthTransacs />
        </div>
        <div style={{ display: 'flex' }}>
            <TransacsToAcc />
        </div>
        <div style={{ display: 'flex' }}>
            <GetAllTransacsSent />
        </div>
        <div style={{ display: 'flex' }}>
            <GetAllTransacsRecieved />
        </div>
        <div style={{ display: 'flex' }}>
            <ViewAllBanks />
            <ViewAllBranches />
        </div>
        <div style={{ display: 'flex' }}>
            
        </div>
        <div style={{ display: 'flex' }}>
            <OpenAccount />
            <CloseAccount />
        </div>
        </div>
    )
}