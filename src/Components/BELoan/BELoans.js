import ApproveLoan from './ApproveLoan'
import CheckCreditWorthiness from './CheckCreditWorthiness'
import GetAllLoans from './GetAllLoans'
import GetAllLoansbyACustomer from './GetAllLoansByACustomer'
import GetAllLoanPolicies from './GetLoanPolicies'
import GetLoansThatNeedApproval from './GetLoansThatNeedApproval'
import React, { useState } from 'react';
import BENavbar from '../Home/BENavbar'
import { Container, Row, Col, Nav } from 'react-bootstrap';
import './style.css'

export default function BELoans(){
    const [selectedSection, setSelectedSection] = useState('loans');
  
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
              <Nav.Link eventKey='loans' style={{ color: '#FFFFFF' }}>All Loans</Nav.Link>
              <Nav.Link eventKey='approvalloans' style={{ color: '#FFFFFF' }}>Approval Request Loans</Nav.Link>
              <Nav.Link eventKey='credit' style={{ color: '#FFFFFF' }}>Credit Worthiness</Nav.Link>
              <Nav.Link eventKey='approveloan' style={{ color: '#FFFFFF' }}>Approve Loan</Nav.Link>
              <Nav.Link eventKey='loansbycustomer' style={{ color: '#FFFFFF' }}>Loans By Customer</Nav.Link>
              <Nav.Link eventKey='loanPolicies' style={{ color: '#FFFFFF' }}>Loan Policies</Nav.Link>
            </Nav>
          </Col>
  
          <Col md={10} style={{ backgroundColor: '#f8f6fa', minHeight: '100vh', padding: '20px' }}>
            {/* Content changes based on selection */}
            {selectedSection === 'loans' && <GetAllLoans />}
            {selectedSection === 'approvalloans' && <GetLoansThatNeedApproval />}
            {selectedSection === 'credit' && <CheckCreditWorthiness />}
            {selectedSection === 'approveloan' && <ApproveLoan />}
            {selectedSection === 'loansbycustomer' && <GetAllLoansbyACustomer />}
            {selectedSection === 'loanPolicies' && <GetAllLoanPolicies />}

          </Col>
        </Row>
      </Container>
      </div>
    );
  }
  