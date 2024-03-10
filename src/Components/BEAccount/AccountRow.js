import React from 'react';

function AccountRow({ account }) {
    return (
        <tr key={account.accountNumber}>
            <td>{account.accountNumber}</td>
            <td>{account.customerID}</td>
            <td>{account.ifscCode}</td>
            <td>{account.status}</td>
            <td>{account.accountType}</td>
            <td>{account.balance}</td>
        </tr>
    );
}

export default AccountRow;