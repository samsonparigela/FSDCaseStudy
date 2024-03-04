import React, { useEffect, useState } from 'react';
import PieChart from './PieChart';

const Example = () => {
    const customerID = sessionStorage.getItem("CID");
    const token = sessionStorage.getItem("Token");
    const [allInBoundTransacs, setAllInBoundTransacs] = useState(0);
    const [allOutBoundTransacs, setAllOutBoundTransacs] = useState(0);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await fetch('https://localhost:7075/api/CustomerAccount/ViewAllYourTransactions?CID=' + customerID, {
                    method: 'GET',
                    headers: {
                        'Authorization': 'Bearer ' + token,
                        'Content-Type': 'application/json'
                    }
                });
                if (!response.ok) {
                    throw new Error('Failed to fetch data');
                }
                const data = await response.json();
                let filteredData1 = data.filter(obj => obj.description === "Self Deposit" || obj.description === "Self Withdraw");
                setAllInBoundTransacs(filteredData1.length);
                let filteredData2 = data.filter(obj => obj.description !== "Self Deposit" && obj.description !== "Self Withdraw");
                setAllOutBoundTransacs(filteredData2.length);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };
        fetchData();
    }, [customerID, token]);

    const data = {
        labels: ['Inbound Transacs', 'Outbound Transacs'],
        datasets: [
            {
                label: 'Transactions:',
                data: [allInBoundTransacs, allOutBoundTransacs],
                backgroundColor: [
                    'rgba(255, 99, 132, 0.5)',
                    'rgba(54, 162, 235, 0.5)',
                ],
                borderColor: [
                    'rgba(255, 99, 132, 1)',
                    'rgba(54, 162, 235, 1)',
                ],
                borderWidth: 1,
            },
        ],
    };

    return <PieChart data={data} />;
};

export default Example;
