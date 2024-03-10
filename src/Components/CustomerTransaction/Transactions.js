import ViewAllYourTransacs from './ViewAllYourTransacs';
import TransferMoney from './TransferMoney';
import WithdrawMoney from './WithdrawMoney';
import DepositMoney from './DepositMoney';
import Navbar from '../Home/Navbar';
import React, { useState } from 'react';
import { Container, Row, Col, Nav } from 'react-bootstrap';
import './style.css';


export default function Transactions() {
  const [selectedSection, setSelectedSection] = useState('Deposit');

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
            <Nav.Link eventKey="Deposit" style={{ color: '#FFFFFF' }}>Deposit</Nav.Link>
            <Nav.Link eventKey="Withdraw" style={{ color: '#FFFFFF' }}>Withdraw</Nav.Link>
            <Nav.Link eventKey="Transfer" style={{ color: '#FFFFFF' }}>Transfer</Nav.Link>
            <Nav.Link eventKey="Transacs" style={{ color: '#FFFFFF' }}>Transactions</Nav.Link>
          </Nav>
        </Col>

        <Col md={10} style={{ backgroundColor: '#f8f6fa', minHeight: '100vh', padding: '20px' }}>
          {/* Content changes based on selection */}
          {selectedSection === 'Deposit' && <DepositMoney />}
          {selectedSection === 'Withdraw' && <WithdrawMoney />}
          {selectedSection === 'Transfer' && <TransferMoney />}
          {selectedSection === 'Transacs' && <ViewAllYourTransacs />}
        </Col>
      </Row>
    </Container>
    </div>
  );
}
