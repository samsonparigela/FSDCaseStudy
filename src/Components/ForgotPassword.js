import React, { useState } from 'react';
import Gaps from './Gaps';
const ForgotPassword = () => {
  const [userName, setUserName] = useState('');
  const [newPassword, setNewPassword] = useState('');
  const [oldPassword, setOldPassword] = useState('');
  const [error, setError] = useState('');

  const reset = async (e) => {
    e.preventDefault();

    // Add validation if needed
    if (!userName) {
      setError('Please enter your email address.');
      return;
    }

    try {
      const response = await fetch('/api/forgot-password', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ userName }),
      });

      const data = await response.json();

      if (response.ok) {
        setUserName(data.message);
      } else {
        setError(data.error || 'Something went wrong.');
      }
    } catch (error) {
      console.error('Error:', error);
      setError('Something went wrong. Please try again later.');
    }
  };

  return (
    <div class='custom-bg-color'>
        <div class="container">
            <div class="row justify-content-center">
                <div class="col-lg-6">
                    <div class="login-container">    
                        <div class="login-form">
                             <h2>Forgot Password</h2>
                                <form>
                                    <div class="mb-3">
                                        <label for="username" class="form-label">Username</label>
                                        <input type="text" class="form-control" value={userName} onChange={(e)=>setUserName(e.target.value)}
                                        id="username" placeholder="Enter your username"/>
                                    </div>
                                    <div class="mb-3">
                                        <label for="password" class="form-label">Old Password</label>
                                        <input type="password" class="form-control" id="password" placeholder="Enter your password"
                                        value={newPassword} onChange={(e)=>setNewPassword(e.target.value)}/>
                                    </div>
                                    <div class="mb-3">
                                        <label for="password" class="form-label">New Password</label>
                                        <input type="password" class="form-control" id="password" placeholder="Enter your password"
                                        value={oldPassword} onChange={(e)=>setOldPassword(e.target.value)}/>
                                    </div>
                                    <div class="flex-container">
                                    <div class="flex-child magenta">
                                    <button type="submit" class="btn btn-primary" onClick={reset}>Reset Password</button>
                                    </div>
      </div>
      <br/>
      {error && (
  <p className="error"> {error} </p>)}
                                </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <Gaps />
        </div>
  );
};

export default ForgotPassword;
