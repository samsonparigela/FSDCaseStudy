import Navbar from '../Home/Navbar';
import ApplyLoan from './ApplyLoan';
import AskForExtension from './AskForExtension';
import GetAllAppliedLoans from './GetAllAppliedLoans';
import GetAllAvailedLoans from './GetAllAvailedLoans';
import GetLoanAmountToAccount from './GetLoanAmountToAccount';
import GetLoanPolicies from './GetLoanPolicies';
import RepayLoan from './RepayLoan';
import React from 'react';
import { useState } from 'react';
import { Container, Row, Col, Nav } from 'react-bootstrap';
import './style.css'


export default function Loans(){
    const [selectedSection, setSelectedSection] = useState('applyloan');
  
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
              <Nav.Link eventKey="applyloan" style={{ color: '#FFFFFF' }}>Apply Loan</Nav.Link>
              <Nav.Link eventKey="askextension" style={{ color: '#FFFFFF' }}>Ask for extension</Nav.Link>
              <Nav.Link eventKey="appliedloans" style={{ color: '#FFFFFF' }}>Applied Loans</Nav.Link>
              <Nav.Link eventKey="approvedloans" style={{ color: '#FFFFFF' }}>Approved Loans</Nav.Link>
              <Nav.Link eventKey="loanpolicies" style={{ color: '#FFFFFF' }}>Loan Policies</Nav.Link>
              <Nav.Link eventKey="loandeposit" style={{ color: '#FFFFFF' }}>Deposit Loan Amount</Nav.Link>
              <Nav.Link eventKey="repayloan" style={{ color: '#FFFFFF' }}>Repay Loan</Nav.Link>
            </Nav>
          </Col>
  
          <Col md={10} style={{ backgroundColor: '#f8f6fa', minHeight: '100vh', padding: '20px' }}>
            {/* Content changes based on selection */}
            {selectedSection === 'applyloan' && <ApplyLoan />}
            {selectedSection === 'askextension' && <AskForExtension />}
            {selectedSection === 'appliedloans' && <GetAllAppliedLoans />}
            {selectedSection === 'approvedloans' && <GetAllAvailedLoans />}
            {selectedSection === 'loanpolicies' && <GetLoanPolicies />}
            {selectedSection === 'loandeposit' && <GetLoanAmountToAccount />}
            {selectedSection === 'repayloan' && <RepayLoan />}
          </Col>
        </Row>
      </Container>
      </div>
    );
  }
  