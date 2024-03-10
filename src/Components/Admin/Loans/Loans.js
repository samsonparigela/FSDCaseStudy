import ApproveLoan from '../../BELoan/ApproveLoan'
import CheckCreditWorthiness from '../../BELoan/CheckCreditWorthiness'
import GetAllLoans from '../../BELoan/GetAllLoans'
import GetAllLoansbyACustomer from '../../BELoan/GetAllLoansByACustomer'
import GetAllLoanPolicies from '../../BELoan/GetLoanPolicies'
import GetLoansThatNeedApproval from '../../BELoan/GetLoansThatNeedApproval'
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