import React from 'react';
import ViewAllTransacsByAcc from "./ViewAllTransacsByAcc.js";
import ViewLastNTransacs from "./ViewLastNTransacs.js";
import ThisMonthTransacs from "./ThisMonthTransacs.js";
import LastMonthTransacs from "./LastMonthTransacs.js";
import ViewAllYourTransacs from "./ViewAllYourTransacs.js";
import TransacsToAcc from "./TransacsToAcc.js";
import GetAllTransacsSent from "./GetAllTransacsSent.js";
import GetAllTransacsReceived from "./GetAllTransacsReceived.js";
import './style.css'

export default function Transacs() {
    return (
        <div style={{ width: '80%'}}>
            <div className="container">

                    <ViewAllTransacsByAcc />
                    <ViewAllYourTransacs />
                    <ViewLastNTransacs />
                    <ThisMonthTransacs />
                    <LastMonthTransacs />
                    <TransacsToAcc />
                    <GetAllTransacsSent />
                    <GetAllTransacsReceived />

            </div>
        </div>
    );
}