import React from 'react';
import OpenAccount from "./OpenAccount.js";
import CloseAccount from "./CloseAccount.js";
import GetAllAccounts from "./GetAllAccounts.js";
import ViewAllBanks from "./ViewAllBanks.js";
import ViewAllBranches from "./ViewAllBranches.js";
import Navbar from "../Home/Navbar.js";
import Transacs from './Transacs.js';
import { useState } from 'react';
import { Container, Row, Col, Nav } from 'react-bootstrap';
import './style.css'


export default function Accounts() {
    const [selectedSection, setSelectedSection] = useState('accounts');
  
    const handleSelect = (section) => {
      setSelectedSection(section);
    };
  
    return (
      <div>
      <Navbar/>
      <Container fluid>
  
        <Row>
          <Col md={2} style={{ backgroundColor: '#54629F', color: 'white', paddingTop: '56px' }}>
            <Nav className="flex-column" onSelect={handleSelect} defaultActiveKey={selectedSection}>
              <Nav.Link eventKey="accounts" style={{ color: '#FFFFFF' }}>Accounts</Nav.Link>
              <Nav.Link eventKey="openAccount" style={{ color: '#FFFFFF' }}>Open Account</Nav.Link>
              <Nav.Link eventKey="closeAccount" style={{ color: '#FFFFFF' }}>Close Account</Nav.Link>
              <Nav.Link eventKey="banks" style={{ color: '#FFFFFF' }}>Banks</Nav.Link>
              <Nav.Link eventKey="branches" style={{ color: '#FFFFFF' }}>Branches</Nav.Link>
              <Nav.Link eventKey="transacs" style={{ color: '#FFFFFF' }}>Transactions</Nav.Link>
            </Nav>
          </Col>
  
          <Col md={10} style={{ backgroundColor: '#f8f6fa', minHeight: '100vh', padding: '20px' }}>
            {/* Content changes based on selection */}
            {selectedSection === 'accounts' && <GetAllAccounts />}
            {selectedSection === 'openAccount' && <OpenAccount />}
            {selectedSection === 'closeAccount' && <CloseAccount />}
            {selectedSection === 'banks' && <ViewAllBanks />}
            {selectedSection === 'branches' && <ViewAllBranches />}
            {selectedSection === 'transacs' && <Transacs />}
          </Col>
        </Row>
      </Container>
      </div>
    );
  }
  