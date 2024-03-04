import './App.css';
import Navbar from './Components/Home/Navbar';
import Defaultbar from './Components/Home/Defaultbar';
import { Routes, Route, BrowserRouter } from 'react-router-dom';
import HomePage from './Components/Home/HomePage';
import Error from './Components/Home/Error';
import Login from './Components/Home/Login'
import Dashboard from './Components/Home/Dashboard';
import Accounts from './Components/CustomerAccount/Accounts';
import Transactions from './Components/CustomerTransaction/Transactions';
import Loans from './Components/CustomerLoan/Loans';
import Beneficiaries from './Components/CustomerBeneficiary/Beneficiaries'
import Profile from './Components/CustomerProfile/Profile';
import BEAccount from './Components/BEAccount/Account';
import BELogin from './Components/Home/BELogin'
import BELoans from './Components/BELoan/BELoans';
import BEProfile from './Components/BEProfile/Profile';
import BEDashboard from './Components/BEDashboard/BEDashboard';
import ForgotPassword from './Components/Home/ForgotPassword';
import BERegister from './Components/Home/BERegister';
import CustomerRegister from './Components/Home/CustomerRegister';
import AdminLogin from './Components/Home/AdminLogin';
import AdminDashboard from './Components/AdminDashboard/AdminDashboard';


export default function App() {
  const IsLoggedIn = sessionStorage.getItem("IsLoggedIn");
  return (
    <div>
      <BrowserRouter>
      <Routes>
        <Route path='/Home' element={<HomePage />}/>
        <Route path='/Login' element={<Login/>}/>
        <Route path='/BELogin' element={<BELogin/>}/>
        <Route path='/AdminLogin' element={<AdminLogin/>}/>
        <Route path='/Dashboard' element={<Dashboard/>}/>
        <Route path='/BERegister' element={<BERegister/>}/>
        <Route path='/CustomerRegister' element={<CustomerRegister/>}/>
        <Route path='/BEDashboard' element={<BEDashboard/>}/>
        <Route path='/AdminDashboard' element={<AdminDashboard/>}/>
        <Route path='/Dashboard/Accounts' element={<Accounts/>}/>
        <Route path='/Dashboard/Loans' element={<Loans/>}/>
        <Route path='/Dashboard/Transactions' element={<Transactions/>}/>
        <Route path='/Dashboard/Beneficiaries' element={<Beneficiaries/>}/>
        <Route path='/BEProfile' element={<BEProfile/>}/>
        <Route path='/Accounts' element={<Accounts/>}/>
        <Route path='/Dashboard/Profile' element={<Profile/>}/>
        <Route path='/BEAccounts' element={<BEAccount/>}/>
        <Route path='/BELoans' element={<BELoans/>}/>
        <Route path='/ForgotPassword' element={<ForgotPassword/>}/>
        <Route path='*' element={<Error />} />
      </Routes>
      </BrowserRouter>
    </div>
  );
}

