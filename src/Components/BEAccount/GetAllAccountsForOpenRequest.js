import React, { useState} from 'react';
import './style.css';

export default function GetAllAccountsForOpenRequest() {
    const [accounts, setAccounts] = useState([]);
    const [flag, setFlag] = useState(0);

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
    const fetchAccounts = async () => {
        try {
            const token = sessionStorage.getItem('Token');
            const response = await fetch('https://localhost:7075/api/BankEmpAccount/GetAllAccountsForOpenRequest', {
                method: 'GET',
                headers: {
                    'Authorization': 'Bearer ' + token,
                    'Content-Type': 'application/json'
                }
            });

            if (response.ok) {
                const accountsData = await response.json();
                setAccounts(accountsData);
            }
        } catch (error) {
            console.error('Error fetching:', error);
        }
    }

    const flagmethod = () => {
        if (flag === 0) {
            setFlag(1);
            fetchAccounts();
        } else {
            setFlag(0);
        }
    }

    return (
        <div className="container mt-5">
            <div className="row justify-content-center">
                <div className="col-md-10">
                    <div className="card custom-bg-color">
                        <div className="card-body">
                            <h1 className="text-center">All Accounts for Opening</h1>
                            <div className="text-center"> 
                            <button type="button" className="btn btn-success mb-3" onClick={flagmethod}>
                            Get all Accounts
                            </button>
                            </div>
                            {flag === 1 &&
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
                                            </tr>
                                        </thead>
                                        <tbody>
                                        {accounts.map(account => (
                                                    <AccountRow key={account.accountNumber} account={account} />
                                                ))}
                                        </tbody>
                                    </table>
                                </div>
                            }
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}
