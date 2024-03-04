import React from 'react';
import { Navbar, Nav, Dropdown } from 'react-bootstrap';

export default function Defaultbar() {
  return (
    <Navbar bg="#17a2b8" expand="lg" style={{ backgroundColor: '#17a2b8' }}>
      <div className="container">
        <Navbar.Brand href="#" className="mr-auto">Maverick Bank</Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="ml-auto">
            <Nav.Link href="http://localhost:3000/Home">Home</Nav.Link>
            <Nav.Link href="http://localhost:3000/Home">Services</Nav.Link>
            <Nav.Link href="http://localhost:3000/Home">About</Nav.Link>
            <Nav.Link href="http://localhost:3000/Home">Contact</Nav.Link>
            <Dropdown>
              <Dropdown.Toggle variant="#17a2b8" id="dropdown-basic">
                Login
              </Dropdown.Toggle>
              <Dropdown.Menu>
                <Dropdown.Item href="/Login">Customer</Dropdown.Item>
                <Dropdown.Item href="/BELogin">Bank Employee</Dropdown.Item>
                <Dropdown.Item href="/AdminLogin">Admin</Dropdown.Item>
              </Dropdown.Menu>
            </Dropdown>
            <Dropdown>
              <Dropdown.Toggle variant="#17a2b8" id="dropdown-basic">
                Sign Up
              </Dropdown.Toggle>
              <Dropdown.Menu>
                <Dropdown.Item href="/CustomerRegister">Customer</Dropdown.Item>
                <Dropdown.Item href="/BERegister">Bank Employee</Dropdown.Item>
              </Dropdown.Menu>
            </Dropdown>
          </Nav>
        </Navbar.Collapse>
      </div>
    </Navbar>
  );
}
