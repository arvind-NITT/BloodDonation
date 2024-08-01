// src/context/BloodDonationProvider.js

import React, { useState } from 'react';
import BloodDonationContext from './Contexts';

const BloodDonationStates = ({ children }) => {

  const [usertype, setusertype] = useState('');
  const [bloodType, setBloodType] = useState('');
  const [donors, setDonors] = useState([]);
  const [DonationCenter, setDonationCenter] = useState([]);
  const [Appointments, setAppointments] = useState([]);
  const [AllAppointments, setAllAppointments] = useState([]);
  const [Request, setRequests] = useState([]);
  const [RequestInmyDistrict, setRequestInmyDistrict] = useState([]);
  const [AllRequests, setAllRequests] = useState([]);
  const [BloodSearch, setBloodSearch] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const BACKENDLINK = process.env.REACT_APP_BACKEND_LINK;
  const addDonor = (newDonor) => {
    setDonors([...donors, newDonor]);
  };

  // const [setup_data,set_setup_data]= useState([]);
  // const [setup_customer,set_setup_customer]= useState([]);
  // const [setup_veges,set_setup_veges]= useState([]);
  // const [setup_Price,set_setup_Price]= useState([]);
  // const [total,settotal]= useState(0);
  // const [customertotal,setcustomertotal]= useState(0);
  // const [vegtotaltotal,setvegtotal]= useState(0);
 
   const fetchdonors= async(item)=>{
       const url= `${BACKENDLINK}/api/User/DonorSearch`;
      const response= await fetch(url,{
       method: 'POST', // *GET, POST, PUT, DELETE, etc.
       headers:{
           'Content-Type': 'application/json',
           "Access-Control-Allow-Origin": "*",
        //    'auth-token':localStorage.getItem('token'),
       },
       body: JSON.stringify({
        "bloodType": item.bloodType,
        "state": item.state,
        "district": item.district
       })
      })
      const startup_= await response.json();
      setDonors(startup_);
    console.log(startup_);
    
     
   }
   const token = localStorage.getItem('token');
   const SearchDonationCenter= async(item)=>{

       const url= `${BACKENDLINK}/api/Donor/SearchDonationCenter`;
      const response= await fetch(url,{
       method: 'POST', // *GET, POST, PUT, DELETE, etc.
       headers:{
           'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Access-Control-Allow-Origin': "*",
        'Authorization': `Bearer ${token}`
       },
       body: JSON.stringify({
        "state": item.state,
        "district": item.district
       })
      })
      const result= await response.json();
      setDonationCenter(result);  
      console.log(result);
   }
   const ScheduleAppointmentforme= async(item)=>{
       const url= `${BACKENDLINK}/api/Donor/ScheduleAppointment`;
      const response= await fetch(url,{
       method: 'POST', // *GET, POST, PUT, DELETE, etc.
       headers:{
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Access-Control-Allow-Origin': "*",
        'Authorization': `Bearer ${token}`
       },
       body: JSON.stringify({
        "centerId": item.centerid,
        "date": item.date
       })
      })
      const result= await response.json();
      setAppointments(result);  
      console.log(result);
   }
   const RequestBlood= async(item)=>{
       const url= `${BACKENDLINK}/api/Recipient/RequestBlood`;
      const response= await fetch(url,{
       method: 'POST', // *GET, POST, PUT, DELETE, etc.
       headers:{
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Access-Control-Allow-Origin': "*",
        'Authorization': `Bearer ${token}`
       },
       body: JSON.stringify({
        "bloodType": item.bloodType,
        "quantity": item.Quantity,
        "state": item.state,
        "district": item.district,
        "requestDate": "2024-07-31T19:00:50.897Z",
        "isUrgent": item.IsUrgent
       })
      })
      const result= await response.json();
      setRequests(result);  
      console.log(result);
   }
   const SearchForBlood= async(item)=>{
       const url= `${BACKENDLINK}/api/Recipient/SearchForBlood`;
      const response= await fetch(url,{
       method: 'POST', // *GET, POST, PUT, DELETE, etc.
       headers:{
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Access-Control-Allow-Origin': "*",
        'Authorization': `Bearer ${token}`
       },
       body: JSON.stringify({
        "bloodType": "A+",
        "state": "Madhya Pradesh",
        "district": "Mandsaur"
       })
      })
      const result= await response.json();
      setBloodSearch(result);  
      console.log(result);
   }
   const addDonationCenter= async(item)=>{
       const url= `${BACKENDLINK}/api/Admin/addDonationCenter`;
      const response= await fetch(url,{
       method: 'POST', // *GET, POST, PUT, DELETE, etc.
       headers:{
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Access-Control-Allow-Origin': "*",
        'Authorization': `Bearer ${token}`
       },
       body: JSON.stringify({
        "name": "Mandsaur",
        "state": "Madhya Pradesh",
        "district": "Mandsaur",
        "pincode": "458883",
        "address": "Mandsaur",
        "contactInfo": "9109705986",
        "operatingHours": "9-7"
       })
      })
      const result= await response.json();
      // setBloodSearch(result);  
   }
   const donorViewRequest= async()=>{
       const url= `${BACKENDLINK}/api/Donor/RequestInMyDistrict`;
      const response= await fetch(url,{
       method: 'GET', // *GET, POST, PUT, DELETE, etc.
       headers:{
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Access-Control-Allow-Origin': "*",
        'Authorization': `Bearer ${token}`
       },
      })
      const result= await response.json();
      setRequestInmyDistrict(result);  
      console.log(result);
   }
   const donorViewAppointment= async()=>{
       const url= `${BACKENDLINK}/api/Donor/ViewAppointment`;
      const response= await fetch(url,{
       method: 'GET', // *GET, POST, PUT, DELETE, etc.
       headers:{
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Access-Control-Allow-Origin': "*",
        'Authorization': `Bearer ${token}`
       },
      })
      const result= await response.json();
      setAllAppointments(result);  
      console.log(result);
   }
   const ViewRequest= async()=>{
       const url= `${BACKENDLINK}/api/Recipient/ViewRequest`;
      const response= await fetch(url,{
       method: 'GET', // *GET, POST, PUT, DELETE, etc.
       headers:{
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Access-Control-Allow-Origin': "*",
        'Authorization': `Bearer ${token}`
       },
      })
      const result= await response.json();
      setAllRequests(result);  
      console.log(result);
   }
   const UpdateInfoForDonor= async(item)=>{
       const url= `${BACKENDLINK}/api/Donor/UpdateInfo`;
      const response= await fetch(url,{
       method: 'PUT', // *GET, POST, PUT, DELETE, etc.
       headers:{
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Access-Control-Allow-Origin': "*",
        'Authorization': `Bearer ${token}`
       },
       body: JSON.stringify({
        "name": "Arvind",
        "dob": "2001-01-05",
        "bloodType": "A+",
        "gender": "Male",
        "father_Name": "Gopal Mali",
        "state": "Madhya Pradesh",
        "district": "Mandsaur",
        "pincode": "4588883",
        "address": "Mandsaur",
        "available": false
       })
      })
      const result= await response.json();
      setBloodSearch(result);  
   }
   const UpdateInfoForRecipient= async(item)=>{
       const url= `${BACKENDLINK}/api/Recipient/UpdateMedicalInfo`;
      const response= await fetch(url,{
       method: 'PUT', // *GET, POST, PUT, DELETE, etc.
       headers:{
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Access-Control-Allow-Origin': "*",
        'Authorization': `Bearer ${token}`
       },
       body: JSON.stringify({
        MedicalCondition: "not fine"
       })
      })
      const result= await response.json();
      setBloodSearch(result);  
      console.log(result);
   }


  return (
    <BloodDonationContext.Provider value={{setusertype,usertype,DonationCenter, UpdateInfoForRecipient,bloodType,
    fetchdonors, setBloodType,SearchDonationCenter,ScheduleAppointmentforme,RequestBlood,
    SearchForBlood, donors, addDonor, isLoading, setIsLoading , AllRequests, ViewRequest,addDonationCenter,donorViewRequest
    ,donorViewAppointment,UpdateInfoForDonor,AllAppointments,RequestInmyDistrict}}>
      {children}
    </BloodDonationContext.Provider>
  );
};

export default BloodDonationStates;
