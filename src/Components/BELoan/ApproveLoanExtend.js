// import React, { useState, useEffect } from 'react';
// import './style.css'
// export default function ApproveLoanExtend(){

//     const [loanID, setLoanID] = useState();
//     const [approval, setApproval] = useState();
//     const [loan,setLoan] = useState({})
//     var bankEmpID = sessionStorage.getItem("BID");
//     var [flag,setFlag] = useState(0);
//     const token = sessionStorage.getItem('Token');


//       try {


//         const response = await fetch('https://localhost:7075/api/BankEmpLoan/ApproveOrDisapproveLoanExtend?LID='+loanID+'&approval='+approval, {
//                                 method: 'PUT',
//                                 headers: {
//                                                 'Authorization': 'Bearer '+token,
//                                                 body: JSON.stringify(loanID), // Include your authorization token
//                                                 'Content-Type': 'application/json'
//                                          }
//                                         });

//             if (response.ok) {
//             const loansData = await response.json();
//             setLoan(loansData);
//             console.log(loansData);
//             } 
//             else {
//             console.error('Failed to fetch');
//             }
//         }catch (error) {
//             console.error('Error fetching:', error);
//         }
//     }
  
//   var flagmethod = (e) =>{
//     if(flag==0){
//         fetchLoans();
//         setFlag(1);
//     }
        
//     else
//         setFlag(0)
//     }


//   return (
//     <div style={{ width: '50%', backgroundColor: 'lightblue' }}>
//             <div class="container mt-5">
//                 <div class="row">
//                     <div class="col-md-12 mb-4">
//                         <div class="card custom-bg-color">
//                             <div class="card-body">
                                                    
//                                 <h1>Approve Loan</h1>
//                                 <div class="form-group">
//                                     <label htmlFor="accountName">Loan ID</label>
//                                     <input type="text" class="form-control"  id="accountNumber" placeholder="Enter LoanID"
//                                     value={loanID} onChange={(e)=>setLoanID(e.target.value)}/>
//                                 </div>
//                                 <button type="button" class="btn btn-success" data-toggle="button" 
//                                 aria-pressed="false" onClick={flagmethod}>
//                                 Approve
//                                 </button>
//                                 {flag==1 ? 
//                                 <table className="table">
//                                     <thead>
//                                     <tr>
//                                         <th>Loan Status</th>
//                                     </tr>
//                                     </thead>
//                                     <tbody>
//                                         <tr>
//                                         <td>{loan.status}</td>
//                                         </tr>
//                                     </tbody>
//                                 </table>
//                                 :<p></p>}
//                             </div>
//                         </div>
//                     </div>
//                 </div>
//             </div>
//         </div>
//   );
// }

