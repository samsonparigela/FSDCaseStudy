import React, { useState } from 'react';
import { Container, Row, Col, Nav } from 'react-bootstrap';
import AdminProfile from '../AdminProfile/Profile';
import Customers from '../Admin/Customers/Customers';
import Accounts from '../Admin/Accounts/Accounts';
import Loans from '../Admin/Loans/Loans';
import Employee from '../Admin/Employee/Employee';
import Banks from '../Admin/Banks/Banks';
import Branches from '../Admin/Branches/Branches';
import './style.css';

export default function AdminDashboard() {
  const [selectedSection, setSelectedSection] = useState('profile');

  const handleSelect = (section) => {
    setSelectedSection(section);
  };

  const logout = () => {
    if (window.confirm("Sure to logout?")) {
      alert("Logged out successfully");
      localStorage.clear();
      sessionStorage.clear();
      window.location.href = '/Home';
    }
  };

  return (
    <Container fluid>
      <Row>
        <Col md={2} style={{ backgroundColor: '#54629F', color: 'white', paddingTop: '56px' }}>
          <Nav className="flex-column" onSelect={handleSelect} defaultActiveKey={selectedSection}>
            <Nav.Link eventKey="profile" style={{ color: '#FFFFFF' }}>Profile</Nav.Link>
            <Nav.Link eventKey="customers" style={{ color: '#FFFFFF' }}>Customers</Nav.Link>
            <Nav.Link eventKey="accounts" style={{ color: '#FFFFFF' }}>Accounts</Nav.Link>
            <Nav.Link eventKey="loans" style={{ color: '#FFFFFF' }}>Loans</Nav.Link>
            <Nav.Link eventKey="Banks" style={{ color: '#FFFFFF' }}>Banks</Nav.Link>
            <Nav.Link eventKey="Branches" style={{ color: '#FFFFFF' }}>Branches</Nav.Link>
            <Nav.Link eventKey="Bank Employees" style={{ color: '#FFFFFF' }}>Employees</Nav.Link>
            <Nav.Link eventKey="logout" style={{ color: '#FFFFFF' }} onClick={() => logout()}>Logout</Nav.Link>
          </Nav>
        </Col>

        <Col md={10} style={{ backgroundColor: '#f8f6fa', minHeight: '100vh', padding: '20px' }}>
          {/* Content changes based on selection */}
          {selectedSection === 'profile' && <AdminProfile />}
          {selectedSection === 'customers' && <Customers />}
          {selectedSection === 'accounts' && <Accounts />}
          {selectedSection === 'loans' && <Loans />}
          {selectedSection === 'Bank Employees' && <Employee />}
          {selectedSection === 'Banks' && <Banks />}
          {selectedSection === 'Branches' && <Branches />}
        </Col>
      </Row>
    </Container>
  );
}
