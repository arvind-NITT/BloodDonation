import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import BloodDonationStates from './context/BloodDonationStates';
import Navbar from './components/Navbar';
import Home from './pages/Home';
import Register from './pages/Register';
import Login from './pages/Login';
import Recipient from './pages/Recipient';

function App() {
  return (
    <BloodDonationStates>
      <Router>
        <div className="App">
         
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/register" element={<Register />} />
            <Route path="/login" element={<Login />} />
            <Route path="/Recipient" element={<Recipient />} />
            {/* Add more routes as needed */}
          </Routes>
        </div>
      </Router>
    </BloodDonationStates>
  );
}

export default App;