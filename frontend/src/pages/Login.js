import React, { useState, useEffect } from 'react';
import Navbar from '../components/Navbar';
import { redirect } from 'react-router-dom';
import { useNavigate } from 'react-router-dom';

const Login = () => {
    const navigate = useNavigate();
  const [formData, setFormData] = useState({
  
    password: '',
    contact: '',
    
  });

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit =async (e) => {

    try{
    e.preventDefault();

    const response= await  fetch('https://localhost:7020/api/User/Login', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Access-Control-Allow-Origin': "*"

      },
      body: JSON.stringify({
  
      
        "phoneNumber": formData.contact,
        
        "password": formData.password,
      
      })
    })
    const data= await response.json();
   if(!response.ok){
    alert(data.message)

    console.log('Response:', data);
    return ;
   }
      localStorage.setItem('token', data.token);
      localStorage.setItem('UserID', data.UserID);
       alert("Login done");
       navigate('/');
      }catch(error)
    {  console.error('Error:', error);
      alert(error);}
    ;
  };



  return (
    <div className="container w-75 d-flex p-5">
      <div className="LeftContainer" style={{ fontFamily: 'Playfair Display, serif', backgroundColor: 'rgb(250, 210, 157)' }}>
        <h2>Now</h2>
        <h1 style={{ color: 'rgb(68, 59, 51)', fontWeight: 800, fontSize: '55px' }}>Find Your Life Partner</h1>
        <h2>Easy and fast</h2>
        <div className="login_couple_photo">
          <img src="./Images/login-couple.png" alt="" style={{ width: '230px' }} />
        </div>
        <div className="logo-bg"></div>
      </div>
      <form className="row w-75 container container-md bg-white g-3 p-5 rounded" id="RegisterForm" onSubmit={handleSubmit}>
        <div className="heading_login">
          <h7>START FOR FREE</h7>
          <h2>SIGN UP TO MATRIMONY</h2>
          <h7>Already a member? <a href="./Login.html">Login</a></h7>
        </div>
        <div className="col-md-6">
          <label htmlFor="contact" className="form-label">Contact Number</label>
          <input type="text" className="form-control" id="contact" name="contact" onChange={handleChange} />
        </div>
        <div className="col-md-6">
          <label htmlFor="password" className="form-label">Password</label>
          <input type="password" className="form-control" id="password" name="password" onChange={handleChange} />
        </div>
       
       
        <div className="col-12">
          <button type="submit" className="btn btn-primary">Login</button>
        </div>
      </form>
    </div>
  );
}

export default Login;