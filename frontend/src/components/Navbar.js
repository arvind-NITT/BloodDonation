import React, { useContext,useEffect,useState } from 'react';
import { Link } from 'react-router-dom';
import BloodDonationContext from '../context/Contexts';
import { useNavigate } from 'react-router-dom';
const Navbar = () => {

  const { setusertype,Role,setRole } = useContext(BloodDonationContext);
  const navigate = useNavigate();
   const logout=()=>{
    localStorage.clear();
    setRole("Null");
   }
   useEffect(() => {
    if(Role=="Null"){
      navigate('/');
    }
}, []);
  return (
    <nav className="navbar navbar-expand-lg navbar-light bg-light">
      <div className="container-fluid">
        <a className="navbar-brand" href="#">Navbar</a>
        <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
          <span className="navbar-toggler-icon"></span>
        </button>
        <div className="collapse navbar-collapse" id="navbarSupportedContent">
       { Role=="Null" &&  <ul className="navbar-nav me-auto mb-2 mb-lg-0">
        <li className="nav-item">
              <Link className="nav-link active" to="/login" onClick={() => setusertype("Recipient")}>Looking for blood</Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link active" to="/login" onClick={() => setusertype("Donor")}>Want to donate blood</Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link" to="/login" onClick={() => setusertype("BloodBank")}>Blood Bank Login</Link>
            </li>
          
           
          
          </ul>}
       { Role=="Recipient" &&  <ul className="navbar-nav me-auto mb-2 mb-lg-0">
        <li className="nav-item">
              <Link className="nav-link active" to="/login" onClick={() => logout()}>Logout</Link>
            </li>
          </ul>}
       { Role=="Donor" &&  <ul className="navbar-nav me-auto mb-2 mb-lg-0">
        <li className="nav-item">
              <Link className="nav-link active" to="/login" onClick={() => logout()}>Logout</Link>
            </li>
          </ul>}
          <form className="d-flex">
            <input className="form-control me-2" type="search" placeholder="Search" aria-label="Search"/>
            <Link className="nav-link active" to="/login" onClick={() => logout()}>Logout</Link>
          </form>
        </div>
      </div>
    </nav>
  );
};

export default Navbar;
