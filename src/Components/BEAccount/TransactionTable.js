// TransactionTable.js
import React from 'react';

const TransactionTable = ({ transacs }) => {
  return (
    <div className="table-responsive">                                
      <table className="table">
        <thead>
          <tr>
            <th>TransactionID</th>
            <th>Status</th>
            <th>Source Account Number</th>
            <th>Destination Account Number</th>
            <th>Transaction Type</th>
            <th>Amount</th>
            <th>Description</th>
            <th>Transaction Date</th>
            {/* Add more table headers as needed */}
          </tr>
        </thead>
        <tbody>
          {transacs.map(tran => (
            <tr key={tran.transactionID}>
              <td>{tran.transactionID}</td>
              <td>{tran.status}</td>
              <td>{tran.sAccountID}</td>
              <td>{tran.beneficiaryAccountNumber}</td>
              <td>{tran.transactionType}</td>
              <td>{tran.amount}</td>
              <td>{tran.description}</td>
              <td>{tran.transactionDate}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default TransactionTable;
