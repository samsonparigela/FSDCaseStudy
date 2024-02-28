import ApplyLoan from './ApplyLoan';
import AskForExtension from './AskForExtension';
import GetAllAppliedLoans from './GetAllAppliedLoans';
import GetAllAvailedLoans from './GetAllAvailedLoans';
import GetLoanAmountToAccount from './GetLoanAmountToAccount';
import GetLoanPolicies from './GetLoanPolicies';
import RepayLoan from './RepayLoan';


export default function Loans(){
    return(
        <div style={{ width: '100%', backgroundColor: 'lightblue' }}>
            <br/>
            <ApplyLoan />
            <AskForExtension />
            <GetAllAppliedLoans />
            <GetAllAvailedLoans />
            <GetLoanPolicies />
            <GetLoanAmountToAccount />
            <RepayLoan />
        </div>
    )
}