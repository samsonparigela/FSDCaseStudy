import React, { useState } from 'react';
import { Container, Row, Col, Nav } from 'react-bootstrap';
import BEProfile from '../BEProfile/Profile';
import BELoans from '../BELoan/BELoans';
import Accounts from '../BEAccount/Account';
import Navbar from '../Home/Navbar';

export default function BEDashboard() {
  const [selectedSection, setSelectedSection] = useState('profile');

  const handleSelect = (section) => {
    if (section === 'logout') {
      if (!window.confirm("Sure to logout?")) {
        return null;
      }
      alert("Logged out successfully");
      localStorage.clear();
      sessionStorage.clear();
      window.location.href = '/Home';
    } else {
      setSelectedSection(section);
    }
  };

  return (
    <Container fluid>
      <Row>
        <Col lg={2} md={4} sm={12} style={{ backgroundColor: '#54629F', color: 'white', paddingTop: '56px' }}>
          <Nav className="flex-column" onSelect={handleSelect} defaultActiveKey={selectedSection}>
            <Nav.Link eventKey="profile" style={{ padding: '10px', color: '#FFFFFF', fontSize: '18px' }}>Profile</Nav.Link>
            <Nav.Link eventKey="accounts" style={{ padding: '10px', color: '#FFFFFF', fontSize: '18px' }}>Accounts</Nav.Link>
            <Nav.Link eventKey="loans" style={{ padding: '10px', color: '#FFFFFF', fontSize: '18px' }}>Loans</Nav.Link>
            <Nav.Link eventKey="logout" style={{ padding: '10px', color: '#FFFFFF', fontSize: '18px' }}>Logout</Nav.Link>
          </Nav>
        </Col>

        <Col lg={10} md={8} sm={12} style={{ backgroundColor: '#f8f9fa', minHeight: '100vh', padding: '20px' }}>
          {/* Content changes based on selection */}
          {selectedSection === 'profile' && <BEProfile />}
          {selectedSection === 'loans' && <BELoans />}
          {selectedSection === 'accounts' && <Accounts />}
        </Col>
      </Row>
    </Container>
  );
}
