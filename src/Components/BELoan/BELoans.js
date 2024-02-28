import ApproveLoan from './ApproveLoan'
import CheckCreditWorthiness from './CheckCreditWorthiness'
import GetAllLoans from './GetAllLoans'
import GetAllLoansbyACustomer from './GetAllLoansByACustomer'
import GetAllLoanPolicies from './GetLoanPolicies'
import GetLoansThatNeedApproval from './GetLoansThatNeedApproval'

export default function BELoans(){
    return(
        <div>
        <div style={{ display: 'flex' }}>
            <GetAllLoans />
            <GetLoansThatNeedApproval />
            
        </div>  
        <div style={{ display: 'flex' }}>
            <CheckCreditWorthiness />
            <ApproveLoan />
        </div>  
        <div style={{ display: 'flex' }}>
            <GetAllLoansbyACustomer />
            <GetAllLoanPolicies />
        </div>      
        </div>
    )
}