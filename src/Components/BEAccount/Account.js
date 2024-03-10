import ApproveAccountOpen from "./ApproveAccountOpen";
import ApproveAccountClose from "./ApproveAccountClose";
import GetAllAccounts from "./GetAllAccounts";
import GetAllAccountsForOpenRequest from './GetAllAccountsForOpenRequest';
import GetAllAccountsForCloseRequest from './GetAllAccountsForCloseRequest';
import GetAllTransacs from './GetAllTransacs';
import GetAllTransacsByAccount from './GetAllTransacsByAccount';
import GetAllTransacsSent from './GetAllTransacsSent';
import GetAllTransacsRecieved from './GetAllTransacsRecieved';
import GetTopTransacs from './GetTopTransacs';
import GetCustomerForAccount from "./GetCustomerForAccount";
import React from 'react';
import { useState } from 'react';
import BENavbar from '../Home/BENavbar'
import { Container, Row, Col, Nav } from 'react-bootstrap';
import './style.css'

export default function Accounts() {
    const [selectedSection, setSelectedSection] = useState('openAccount');
  
    const handleSelect = (section) => {
      setSelectedSection(section);
    };
  
    return (
      <div>
      <BENavbar/>
      <Container fluid>
  
        <Row>
          <Col md={2} style={{ backgroundColor: '#54629F', color: 'white', paddingTop: '56px' }}>
            <Nav className="flex-column" onSelect={handleSelect} defaultActiveKey={selectedSection}>
              <Nav.Link eventKey="accounts" style={{ color: '#FFFFFF' }}>Accounts</Nav.Link>
              <Nav.Link eventKey="accountsforopen" style={{ color: '#FFFFFF' }}>Accounts for Open request</Nav.Link>
              <Nav.Link eventKey="accountsforclose" style={{ color: '#FFFFFF' }}>Accounts for Close request</Nav.Link>
              <Nav.Link eventKey="openAccount" style={{ color: '#FFFFFF' }}>Approve Account Opening</Nav.Link>
              <Nav.Link eventKey="closeAccount" style={{ color: '#FFFFFF' }}>Approve Account Closing</Nav.Link>
              <Nav.Link eventKey="customerforaccount" style={{ color: '#FFFFFF' }}>Customer for Account</Nav.Link>
              <Nav.Link eventKey="senttransacs" style={{ color: '#FFFFFF' }}>Sent Transactions</Nav.Link>
              <Nav.Link eventKey="receivedtransacs" style={{ color: '#FFFFFF' }}>Received Transactions</Nav.Link>
              <Nav.Link eventKey="alltransacs" style={{ color: '#FFFFFF' }}>All Transactions</Nav.Link>
              <Nav.Link eventKey="transacsbyaccount" style={{ color: '#FFFFFF' }}>Transactions By Account</Nav.Link>
              <Nav.Link eventKey="top5transacs" style={{ color: '#FFFFFF' }}>Top 5 Transactions</Nav.Link>
            </Nav>
          </Col>
  
          <Col md={10} style={{ backgroundColor: '#f8f6fa', minHeight: '100vh', padding: '20px' }}>
            {/* Content changes based on selection */}
            {selectedSection === 'accounts' && <GetAllAccounts />}
            {selectedSection === 'openAccount' && <ApproveAccountOpen />}
            {selectedSection === 'closeAccount' && <ApproveAccountClose />}
            {selectedSection === 'customerforaccount' && <GetCustomerForAccount />}
            {selectedSection === 'senttransacs' && <GetAllTransacsSent />}
            {selectedSection === 'receivedtransacs' && <GetAllTransacsRecieved />}
            {selectedSection === 'accountsforopen' && <GetAllAccountsForOpenRequest/>}
            {selectedSection === 'accountsforclose' && <GetAllAccountsForCloseRequest />}
            {selectedSection === 'alltransacs' && <GetAllTransacs />}
            {selectedSection === 'transacsbyaccount' && <GetAllTransacsByAccount />}
            {selectedSection === 'top5transacs' && <GetTopTransacs/>}
          </Col>
        </Row>
      </Container>
      </div>
    );
  }
  