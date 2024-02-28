import './App.css';
import Navbar from './Components/Navbar';
import Defaultbar from './Components/Defaultbar';
import { Routes, Route, BrowserRouter } from 'react-router-dom';
import HomePage from './Components/HomePage';
import Error from './Components/Error';
import Login from './Components/Login'
import Dashboard from './Components/Dashboard';
import Accounts from './Components/CustomerAccount/Accounts';
import Transactions from './Components/CustomerTransaction/Transactions';
import Loans from './Components/CustomerLoan/Loans';
import Beneficiaries from './Components/CustomerBeneficiary/Beneficiaries'
import Profile from './Components/CustomerProfile/Profile';
import BEAccount from './Components/BEAccount/Account';
import BELogin from './Components/BELogin'
import BELoans from './Components/BELoan/BELoans';
import BEProfile from './Components/BEProfile/Profile';
import BEDashboard from './Components/BEDashboard/BEDashboard';
import ForgotPassword from './Components/ForgotPassword';
import BERegister from './Components/Home/BERegister';
import CustomerRegister from './Components/Home/CustomerRegister';


export default function App() {
  var isLoggedIn = sessionStorage.getItem("Token")
  return (
    <div>
    {isLoggedIn?<Navbar />:<Defaultbar />}
      <BrowserRouter>
      <Routes>
        <Route path='/Home' element={<HomePage />}/>
        <Route path='/Login' element={<Login/>}/>
        <Route path='/BELogin' element={<BELogin/>}/>
        <Route path='/Dashboard' element={<Dashboard/>}/>
        <Route path='/BERegister' element={<BERegister/>}/>
        <Route path='/CustomerRegister' element={<CustomerRegister/>}/>
        <Route path='/BEDashboard' element={<BEDashboard/>}/>
        <Route path='/Dashboard/Accounts' element={<Accounts/>}/>
        <Route path='/Dashboard/Loans' element={<Loans/>}/>
        <Route path='/Dashboard/Transactions' element={<Transactions/>}/>
        <Route path='/Dashboard/Beneficiaries' element={<Beneficiaries/>}/>
        <Route path='/BEProfile' element={<BEProfile/>}/>
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

