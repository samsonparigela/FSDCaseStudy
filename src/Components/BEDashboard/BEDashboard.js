import React, { useState } from 'react';
import { Container, Row, Col, Nav } from 'react-bootstrap';
import BEProfile from '../BEProfile/Profile';
import BELoans from '../BELoan/BELoans';
import Accounts from '../BEAccount/Account';
export default function BEDashboard() {
  const [selectedSection, setSelectedSection] = useState('profile');
  var logout = ()=> {
    alert("Logged out successfully");
    localStorage.clear();
    sessionStorage.clear();
    
    window.location.href = '/Home';

}
  const handleSelect = (section) => {
    setSelectedSection(section);
  };

  return (
    <Container fluid>
      <Row>
        {/* Sidebar */}
        <Col md={2} style={{ backgroundColor: '#54626F', color: 'white', paddingTop: '56px' }}>
          <Nav className="flex-column" onSelect={handleSelect} defaultActiveKey={selectedSection}>
            <Nav.Link eventKey="profile" style={{ padding: '10px', color:'#F5F5DC' ,fontSize: 40}}>Profile</Nav.Link>
            <Nav.Link eventKey="accounts" style={{ padding: '10px',color:'#F5F5DC' ,fontSize: 40}}>Accounts</Nav.Link>
            <Nav.Link eventKey="loans" style={{ padding: '10px' ,color:'#F5F5DC' ,fontSize: 40}}>Loans</Nav.Link>
            <Nav.Link eventKey="logout" style={{ padding: '10px' ,color:'#F5F5DC' ,fontSize: 40}}>Logout</Nav.Link>
          </Nav>
        </Col>

        {/* Page content */}
        <Col md={10} style={{ backgroundColor: '#f8f9fa', minHeight: '100vh', padding: '20px' }}>
          {/* Content changes based on selection */}
          {selectedSection === 'profile' && (
            <BEProfile />
          )}
          {selectedSection === 'loans' && (
            <div>
             <BELoans />
            </div>
          )}
          {selectedSection === 'accounts' && (
            <div>
              <Accounts />
            </div>
          )}
          {selectedSection === 'logout' && (
            logout()
          )}
        </Col>
      </Row>
    </Container>
  );
}


