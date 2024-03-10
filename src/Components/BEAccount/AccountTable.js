// AccountTable.js
import React from 'react';
import AccountRow from './AccountRow';

function AccountTable({ accounts }) {
  return (
    <div className="table-responsive">
      <table className="table">
        <thead>
          <tr>
            <th>AccountNumber</th>
            <th>CustomerID</th>
            <th>IFSC Code</th>
            <th>Status</th>
            <th>Account Type</th>
            <th>Balance</th>
            {/* Add more table headers as needed */}
          </tr>
        </thead>
        <tbody>
          {accounts.map(account => (
            <AccountRow key={account.accountNumber} account={account} />
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default AccountTable;
