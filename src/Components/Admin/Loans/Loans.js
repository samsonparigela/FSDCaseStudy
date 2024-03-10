import ApproveLoan from './ApproveLoan'
import CheckCreditWorthiness from '../../BELoan/CheckCreditWorthiness'
import GetAllLoans from './GetAllLoans'
import GetAllLoansbyACustomer from './GetAllLoansByACustomer'
import GetAllLoanPolicies from './GetLoanPolicies'
import GetLoansThatNeedApproval from './GetLoansThatNeedApproval'
import DeleteLoanPolicy from './DeleteLoanPolicy'
import AddLoanPolicy from './AddLoanPolicy'
export default function Loans(){
    return(
        <div>
        <div style={{ display: 'flex' }}>
            <AddLoanPolicy />
            <DeleteLoanPolicy />
        </div>             
        <div style={{ display: 'flex' }}>
            <GetAllLoans />
            <GetLoansThatNeedApproval />
            
        </div>  
        <div style={{ display: 'flex' }}>
            <CheckCreditWorthiness/>
            <ApproveLoan />
        </div>  
        <div style={{ display: 'flex' }}>
            <GetAllLoansbyACustomer />
            <GetAllLoanPolicies />
        </div>   
        </div>
    )
}