import React, { useContext,useEffect,useState } from 'react';
import { Link } from 'react-router-dom';
import BloodDonationContext from '../context/Contexts';
import { useNavigate } from 'react-router-dom';
const Navbar = () => {

  const { setusertype,Role,setRole,setBloodBankwantstoseeAppointments,setDonorwantstoseeAppointments } = useContext(BloodDonationContext);
  const navigate = useNavigate();
  const token  = localStorage.getItem('token');
   const logout=()=>{
    localStorage.clear();
    setRole("Null");
    navigate('/');
   }
   const setBloodBankwantstoseeAppointment=()=>{
    console.log("switching")
    setBloodBankwantstoseeAppointments(true);
   }
   const setDonorwantstoseeAppointment=()=>{
    console.log("switching")
    setDonorwantstoseeAppointments(true);
   }

   useEffect(() => {
    if(!localStorage.getItem('token')){
      navigate('/');
    }
}, []);
  return (
    <nav className="navbar navbar-expand-lg navbar-light bg-light">
      <div className="container-fluid">
        <a className="navbar-brand" href="#">Blood Buddy</a>
        <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
          <span className="navbar-toggler-icon"></span>
        </button>
        <div className="collapse navbar-collapse" id="navbarSupportedContent">
       { !token &&  <ul className="navbar-nav me-auto mb-2 mb-lg-0">
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
       {/* { token &&  <ul className="navbar-nav me-auto mb-2 mb-lg-0">
        <li className="nav-item">
              <Link className="nav-link active" to="/login" onClick={() => logout()}>Logout</Link>
            </li>
          </ul>} */}

        

          {
            localStorage.getItem('Role') && localStorage.getItem('Role') ==="Bloodbank" && <Link className="nav-link active" to=""  onClick={setBloodBankwantstoseeAppointment}>All Appointments</Link>
          }
          {
            localStorage.getItem('Role') && localStorage.getItem('Role') ==="Donor" && <Link className="nav-link active" to=""  onClick={setDonorwantstoseeAppointment}>All Appointments</Link>
          }
 { token && <form className="d-flex">
            {/* <input className="form-control me-2" type="search" placeholder="Search" aria-label="Search"/> */}
            <Link className="nav-link active" to="" >{localStorage.getItem('Role')}</Link>
            <Link className="nav-link active" to="/login" onClick={() => logout()}>Logout</Link>

          </form>}
        </div>
      </div>
    </nav>
  );
};

export default Navbar;
