import React from 'react';
import './style.css'
const Alert = ({ message, onClose }) => {
  return (
    <div className="overlay">
      <div className="alert">
        <p>{message}</p>
        <button onClick={onClose}>Close</button>
      </div>
    </div>
  );
};

export default Alert;
