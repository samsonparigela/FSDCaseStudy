import { Dropdown } from 'react-bootstrap';

export default function Defaultbar(){
    return(
        <div>
            <nav class="navbar navbar-expand-lg navbar-light bg-info">
  <div class="container">
    <a class="navbar-brand" href="#">Maverick Bank</a>
    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarNav">
      <ul class="navbar-nav ml-auto">
        <li class="nav-item active">
          <a class="nav-link" href="http://localhost:3000/Home">Home</a>
        </li>
        <li class="nav-item">
          <a class="nav-link" href="http://localhost:3000/Home">Services</a>
        </li>
        <li class="nav-item">
          <a class="nav-link" href="http://localhost:3000/Home">About</a>
        </li>
        <li class="nav-item">
          <a class="nav-link" href="http://localhost:3000/Home">Contact</a>
        </li>
        <Dropdown>
      <Dropdown.Toggle variant="info" id="dropdown-basic">
        <h7>Login</h7>
      </Dropdown.Toggle>

      <Dropdown.Menu>
        <Dropdown.Item href="/Login">Customer</Dropdown.Item>
        <Dropdown.Item href="/BELogin">Bank Employee</Dropdown.Item>
      </Dropdown.Menu>
    </Dropdown>
    <Dropdown>
      <Dropdown.Toggle variant="info" id="dropdown-basic">
        Sign Up
      </Dropdown.Toggle>

      <Dropdown.Menu>
        <Dropdown.Item href="/CustomerRegister">Customer</Dropdown.Item>
        <Dropdown.Item href="/BERegister">Bank Employee</Dropdown.Item>
      </Dropdown.Menu>
    </Dropdown>
      </ul>
    </div>
  </div>
</nav>
        </div>
    )
}