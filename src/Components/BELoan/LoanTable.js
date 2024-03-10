// LoanTable.js
import React from 'react';

function LoanTable({ loans }) {
  return (
    <div className="table-responsive">
      <table className="table">
        <thead>
          <tr>
            <th>Loan ID</th>
            <th>Customer ID</th>
            <th>Loan Amount</th>
            <th>Status</th>
            <th>Loan Purpose</th>
          </tr>
        </thead>
        <tbody>
          {loans.map(loan => (
            <tr key={loan.loanID}>
              <td>{loan.loanID}</td>
              <td>{loan.customerID}</td>
              <td>{loan.loanAmount}</td>
              <td>{loan.status}</td>
              <td>{loan.loanPurpose}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default LoanTable;
