import React, { useState } from 'react';
import { Container, Row, Col, Nav } from 'react-bootstrap';
import AdminProfile from '../AdminProfile/Profile';
import Customers from '../Admin/Customers/Customers';
import Accounts from '../Admin/Accounts/Accounts';
import Loans from '../Admin/Loans/Loans';
import Employee from '../Admin/Employee/Employee';
import Banks from '../Admin/Banks/Banks';
import Branches from '../Admin/Branches/Branches';
export default function AdminDashboard() {
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
            <Nav.Link eventKey="customers" style={{ padding: '10px',color:'#F5F5DC' ,fontSize: 40}}>Customers</Nav.Link>
            <Nav.Link eventKey="accounts" style={{ padding: '10px',color:'#F5F5DC' ,fontSize: 40}}>Accounts</Nav.Link>
            <Nav.Link eventKey="loans" style={{ padding: '10px' ,color:'#F5F5DC' ,fontSize: 40}}>Loans</Nav.Link>
            <Nav.Link eventKey="Banks" style={{ padding: '10px' ,color:'#F5F5DC' ,fontSize: 40}}>Banks</Nav.Link>
            <Nav.Link eventKey="Branches" style={{ padding: '10px' ,color:'#F5F5DC' ,fontSize: 40}}>Branches</Nav.Link>
            <Nav.Link eventKey="Bank Employees" style={{ padding: '10px' ,color:'#F5F5DC' ,fontSize: 40}}>Employees</Nav.Link>
            <Nav.Link eventKey="logout" style={{ padding: '10px' ,color:'#F5F5DC' ,fontSize: 40}}>Logout</Nav.Link>
          </Nav>
        </Col>

        {/* Page content */}
        <Col md={10} style={{ backgroundColor: '#f8f9fa', minHeight: '100vh', padding: '20px' }}>
          {/* Content changes based on selection */}
          {selectedSection === 'profile' && (
            <AdminProfile />
          )}
          {selectedSection === 'customers' && (
            <div>
             <Customers />
            </div>
          )}
          {selectedSection === 'accounts' && (
            <div>
             <Accounts />
            </div>
          )}
          {selectedSection === 'loans' && (
            <div>
              <Loans />
            </div>
          )}
          {selectedSection === 'Bank Employees' && (
            <div>
              <Employee />
            </div>
          )}
          {selectedSection === 'Banks' && (
            <div>
              <Banks />
            </div>
          )}
          {selectedSection === 'Branches' && (
            <div>
              <Branches />
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


