// src/context/BloodDonationProvider.js

import React, { useState } from 'react';
import BloodDonationContext from './Contexts';

const BloodDonationStates = ({ children }) => {

  const [usertype, setusertype] = useState('');
  const [userProfile, setuserProfile] = useState([]);
  const [Role, setRole] = useState("Null");
  const [AvailableBlood, setAvailableBlood] = useState([]);
  const [bloodType, setBloodType] = useState('');
  const [donors, setDonors] = useState([]);
  const [DonationCenter, setDonationCenter] = useState([]);
  const [RecipientDonationCenter, setRecipientDonationCenter] = useState([]);
  const [Appointments, setAppointments] = useState([]);
  const [AllAppointments, setAllAppointments] = useState(null);
  const [AllBloodbankAppointments, setAllBloodbankAppointments] = useState([]);
  const [Request, setRequests] = useState([]);
  const [RequestInmyDistrict, setRequestInmyDistrict] = useState([]);
  const [AllRequests, setAllRequests] = useState([]);
  const [BloodSearch, setBloodSearch] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const [BloodBankwantstoseeAppointments, setBloodBankwantstoseeAppointments] = useState(false);
  const [DonorwantstoseeAppointments, setDonorwantstoseeAppointments] = useState(false);
  const BACKENDLINK = process.env.REACT_APP_BACKEND_LINK;
  const addDonor = (newDonor) => {
    setDonors([...donors, newDonor]);
  };

 
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
   const SearchDonationCenterNearMe= async(item)=>{

       const url= `${BACKENDLINK}/api/User/DonationCenterSearch`;
      const response= await fetch(url,{
       method: 'POST', // *GET, POST, PUT, DELETE, etc.
       headers:{
           'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Access-Control-Allow-Origin': "*",

       },
       body: JSON.stringify({
        "state": item.state,
        "district": item.district
       })
      })
      const result= await response.json();
      if(!response.ok){
        alert(result.message)
    
        console.log('Response:', result);
        return ;
       }
      setDonationCenter(result);  
      console.log(result);
   }
   const SearchDonationCenterNearMeForRecipient= async(item)=>{

       const url= `${BACKENDLINK}/api/Recipient/SearchForDonationCenters`;
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
      if(!response.ok){
        alert(result.message)
    
        console.log('Response:', result);
        return ;
       }
      setRecipientDonationCenter(result);  
      console.log(result);
   }
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
      if(!response.ok){
        alert(result.message)
    
        console.log('Response:', result);
        return ;
       }
      setDonationCenter(result);  
      console.log(result);
   }
   const ScheduleAppointmentforme= async(item)=>{

    const today = new Date();
  const maxDate = new Date();
  maxDate.setDate(today.getDate() + 180);

  const appointmentDate = new Date(item.date);

  if (appointmentDate <= today) {
    console.error('Appointment date must be greater than today.');
    alert('Appointment date must be greater than today.')
    return;
  }

  if (appointmentDate > maxDate) {
    console.error('Appointment date cannot be more than 180 days from today.');
    alert("Appointment date cannot be more than 180 days from today.");
    return;
  }
  const sixtyDaysInMilliseconds = 60 * 24 * 60 * 60 * 1000;
  for (let appointment of AllAppointments) {
    if(appointment.status ==="Cancelled") {
      continue;
    }
    const existingAppointmentDate = new Date(appointment.appointmentDate);
    console.log(appointmentDate,existingAppointmentDate);
    const differenceInTime = Math.abs(appointmentDate - existingAppointmentDate);
     
    if (differenceInTime < sixtyDaysInMilliseconds) {
      console.error('The new appointment date must be at least 60 days apart from any existing appointment.');
      alert('The new appointment date must be at least 60 days apart from any existing appointment.')
      return;
    }
  }

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
      const newAppointment= await response.json();
      if(!response.ok){
        alert(newAppointment.message)
    
        console.log('Response:', newAppointment);
        return ;
       }
       alert("Done");
      setAppointments(newAppointment);  
      setAllAppointments((prevAllAppointments) => [...prevAllAppointments, newAppointment]);
      console.log(newAppointment);
   }
   const getTodaysDate = () => {
    return new Date().toISOString();
};
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
        "requestDate": getTodaysDate(),
        "isUrgent": item.IsUrgent
       })
      })
      const result= await response.json();
      if(!response.ok){
        alert(result.message)
    
        console.log('Response:', result);
        return ;
       }
      setRequests(result);  
      ViewRequest();
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
        "bloodType": item.bloodType,
        "state": item.state,
        "district": item.district
       })
      })
      const result= await response.json();
      if(!response.ok){
        alert(result.message)
    
        console.log('Response:', result);
        return ;
       }
      setBloodSearch(result);  
      console.log(result);
   }
//------------------------------------------//

const CancelmyRequest= async(item)=>{
  const url= `${BACKENDLINK}/api/Recipient/CencelRequest`;
 const response= await fetch(url,{
  method: 'PUT', // *GET, POST, PUT, DELETE, etc.
  headers:{
   'Content-Type': 'application/json',
   'Accept': 'application/json',
   'Access-Control-Allow-Origin': "*",
   'Authorization': `Bearer ${token}`
  },
  body: JSON.stringify({
    requestid:item
  })
 })
 const result= await response.json();
 if(!response.ok){
  alert(result.message)

  console.log('Response:', result);
  return ;
 }
 // setBloodSearch(result);  
 ViewRequest();
}
const FullfilledmyRequest= async(item)=>{
  const url= `${BACKENDLINK}/api/Recipient/ApproveRequest`;
 const response= await fetch(url,{
  method: 'PUT', // *GET, POST, PUT, DELETE, etc.
  headers:{
   'Content-Type': 'application/json',
   'Accept': 'application/json',
   'Access-Control-Allow-Origin': "*",
   'Authorization': `Bearer ${token}`
  },
  body: JSON.stringify({
    requestid:item
  })
 })
 const result= await response.json();
 if(!response.ok){
  alert(result.message)

  console.log('Response:', result);
  return ;
 }
 // setBloodSearch(result);  
 ViewRequest();
}

//-------------------------------------------//


const GetInfoForDonor= async(item)=>{
  const url= `${BACKENDLINK}/api/Donor/GetInfo`;
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
 if(!response.ok){
   alert(result.message)

   console.log('Response:', result);
   return ;
  }
  console.log(result);
  setuserProfile(result);  
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
      if(!response.ok){
        alert(result.message)
    
        console.log('Response:', result);
        return ;
       }
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
      if(!response.ok){
        alert(result.message)
    
        console.log('Response:', result);
        return ;
       }
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
      if(!response.ok){
        alert(result.message)
    
        console.log('Response:', result);
        return ;
       }
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
      if(!response.ok){
        alert(result.message)
    
        console.log('Response:', result);
        return ;
       }
      setAllRequests(result);  
      console.log(result);
   }
   const CancelmyAppointment= async(item)=>{
       const url= `${BACKENDLINK}/api/Donor/CancelAppointment`;
      const response= await fetch(url,{
       method: 'PUT', // *GET, POST, PUT, DELETE, etc.
       headers:{
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Access-Control-Allow-Origin': "*",
        'Authorization': `Bearer ${token}`
       },
       body: JSON.stringify({
        Appointmentid:item
       })
      })
      const result= await response.json();
      if(!response.ok){
        alert(result.message)
    
        console.log('Response:', result);
        return ;
       }
      // setBloodSearch(result);  
      donorViewAppointment();
   }
   const ReschedulemyAppointment= async(item)=>{

    const today = new Date();
    const maxDate = new Date();
    maxDate.setDate(today.getDate() + 180);
  
    const appointmentDate = new Date(item.date);
  
    if (appointmentDate <= today) {
      console.error('Appointment date must be greater than today.');
      alert('Appointment date must be greater than today.')
      return;
    }
  
    if (appointmentDate > maxDate) {
      console.error('Appointment date cannot be more than 180 days from today.');
      alert("Appointment date cannot be more than 180 days from today.");
      return;
    }
    const sixtyDaysInMilliseconds = 60 * 24 * 60 * 60 * 1000;
    for (let appointment of AllAppointments) {
      console.log(appointment.appointmentId, item.centerid);
      if(appointment.appointmentId === item.centerid || appointment.status ==="Cancelled"){
        console.log(appointment.appointmentId, item.centerid);
        continue;
      }

      const existingAppointmentDate = new Date(appointment.appointmentDate);
      console.log(appointmentDate,existingAppointmentDate);
      const differenceInTime = Math.abs(appointmentDate - existingAppointmentDate);
       
      if (differenceInTime < sixtyDaysInMilliseconds) {
        console.error('The new appointment date must be at least 60 days apart from any existing appointment.');
        return;
      }
    }


       const url= `${BACKENDLINK}/api/Donor/ReScheduleAppointment`;
      const response= await fetch(url,{
       method: 'PUT', // *GET, POST, PUT, DELETE, etc.
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
      if(!response.ok){
        alert(result.message)
    
        console.log('Response:', result);
        return ;
       }
      // setBloodSearch(result);  
      donorViewAppointment();
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
        "name":item.name,
        "dob": item.dob,
        "bloodType": item.bloodType,
        "gender": item.gender,
        "father_Name": item.fatherName,
        "state": item.state,
        "district": item.district,
        "pincode": item.pincode,
        "address":item.address,
        "available": item.available
       })
      })
      const result= await response.json();
      if(!response.ok){
        alert(result.message)
    
        console.log('Response:', result);
        return ;
       }
       setuserProfile(result);  
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

  //----------------------------------------------------------------------//
  const BloodbankAllAppointment= async()=>{
    const url= `${BACKENDLINK}/api/BloodBank/ViewAppointment`;
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
   if(!response.ok){
    alert(result.message)

    console.log('Response:', result);
    return ;
   }
   setAllBloodbankAppointments(result);  
   
   console.log(result);
}
const AppointmentUpdateByBloodBank= async(item)=>{
  console.log("Updating");
  const url= `${BACKENDLINK}/api/BloodBank/UpdateAppointment`;
 const response= await fetch(url,{
  method: 'PUT', // *GET, POST, PUT, DELETE, etc.
  headers:{
   'Content-Type': 'application/json',
   'Accept': 'application/json',
   'Access-Control-Allow-Origin': "*",
   'Authorization': `Bearer ${token}`
  },
  body: JSON.stringify({
   Appointmentid:item
  })
 })
 const result= await response.json();
 if(!response.ok){
  alert(result.message)

  console.log('Response:', result);
  return ;
 }
 // setBloodSearch(result);  
 BloodbankAllAppointment();
 AvailabilityCheck();
}
const AppointmentCencelledByBloodBank= async(item)=>{
  const url= `${BACKENDLINK}/api/BloodBank/CancelAppointment`;
 const response= await fetch(url,{
  method: 'PUT', // *GET, POST, PUT, DELETE, etc.
  headers:{
   'Content-Type': 'application/json',
   'Accept': 'application/json',
   'Access-Control-Allow-Origin': "*",
   'Authorization': `Bearer ${token}`
  },
  body: JSON.stringify({
   Appointmentid:item
  })
 })
 const result= await response.json();
 if(!response.ok){
  alert(result.message)

  console.log('Response:', result);
  return ;
 }
 // setBloodSearch(result);  
 BloodbankAllAppointment();
}
const RescheduleAppointmentByBloodBank= async(item)=>{

  const today = new Date();
  const maxDate = new Date();
  maxDate.setDate(today.getDate() + 180);

  const appointmentDate = new Date(item.date);

  if (appointmentDate <= today) {
    console.error('Appointment date must be greater than today.');
    alert('Appointment date must be greater than today.')
    return;
  }

  if (appointmentDate > maxDate) {
    console.error('Appointment date cannot be more than 180 days from today.');
    alert("Appointment date cannot be more than 180 days from today.");
    return;
  }
  // const sixtyDaysInMilliseconds = 60 * 24 * 60 * 60 * 1000;
  // for (let appointment of AllBloodbankAppointments) {
  //   console.log(appointment.appointmentId, item.centerid);
  //   if(appointment.appointmentId === item.centerid){
  //     console.log(appointment.appointmentId, item.centerid);
  //     continue;
  //   }

  //   const existingAppointmentDate = new Date(appointment.appointmentDate);
  //   console.log(appointmentDate,existingAppointmentDate);
  //   const differenceInTime = Math.abs(appointmentDate - existingAppointmentDate);
     
  //   if (differenceInTime < sixtyDaysInMilliseconds) {
  //     console.error('The new appointment date must be at least 60 days apart from any existing appointment.');
  //     alert("The new appointment date must be at least 60 days apart from any existing appointment.");
  //     return;
  //   }
  // }


     const url= `${BACKENDLINK}/api/BloodBank/ReScheduleAppointment`;
    const response= await fetch(url,{
     method: 'PUT', // *GET, POST, PUT, DELETE, etc.
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
    // setBloodSearch(result);  
    if(!response.ok){
      alert(result.message)
  
      console.log('Response:', result);
      return ;
     }
    BloodbankAllAppointment();
 }

 const AvailabilityCheck = async()=>{
  const url= `${BACKENDLINK}/api/BloodBank/BloodAvailabilityInInventories`;
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
  if(!response.ok){
    alert(result.message)

    console.log('Response:', result);
    return ;
   }
  setAvailableBlood(result);  
  
  console.log(result);
 }
 const AddNewInventory= async(item)=>{
  const url= `${BACKENDLINK}/api/BloodBank/AddInventory`;
 const response= await fetch(url,{
  method: 'POST', // *GET, POST, PUT, DELETE, etc.
  headers:{
   'Content-Type': 'application/json',
   'Accept': 'application/json',
   'Access-Control-Allow-Origin': "*",
   'Authorization': `Bearer ${token}`
  },
  body: JSON.stringify({
    CenterId:2,
    BloodType:item.bloodType,
    Quantity:item.unit
  })
 })
 const result= await response.json();
//  setBloodSearch(result);  
  AvailabilityCheck();
 console.log(result);
}
 const UpdateInventory= async(item)=>{
  const url= `${BACKENDLINK}/api/BloodBank/UpdateInventory`;
 const response= await fetch(url,{
  method: 'PUT', // *GET, POST, PUT, DELETE, etc.
  headers:{
   'Content-Type': 'application/json',
   'Accept': 'application/json',
   'Access-Control-Allow-Origin': "*",
   'Authorization': `Bearer ${token}`
  },
  body: JSON.stringify({

   InventoryId:item.Inventoryid,
   Quantity:item.Unit
  })
 })
 const result= await response.json();
//  setBloodSearch(result);  
AvailabilityCheck();
 console.log(result);
}




  return (
    <BloodDonationContext.Provider value={{setusertype,usertype,DonationCenter, UpdateInfoForRecipient,bloodType,
    fetchdonors, setBloodType,SearchDonationCenter,ScheduleAppointmentforme,RequestBlood,
    SearchForBlood, donors, addDonor, isLoading, setIsLoading , AllRequests, ViewRequest,addDonationCenter,donorViewRequest
    ,donorViewAppointment,UpdateInfoForDonor,AllAppointments,RequestInmyDistrict,CancelmyAppointment,ReschedulemyAppointment,
    AllBloodbankAppointments,AppointmentCencelledByBloodBank,
    AppointmentUpdateByBloodBank,BloodbankAllAppointment,RescheduleAppointmentByBloodBank,AvailabilityCheck,AvailableBlood,
    SearchDonationCenterNearMe,SearchDonationCenterNearMeForRecipient,RecipientDonationCenter,
    setRole,Role,CancelmyRequest,FullfilledmyRequest,UpdateInventory,setBloodBankwantstoseeAppointments,BloodBankwantstoseeAppointments,
    DonorwantstoseeAppointments,setDonorwantstoseeAppointments,BloodSearch,AddNewInventory,userProfile,GetInfoForDonor}}>
      {children}
    </BloodDonationContext.Provider>
  );
};

export default BloodDonationStates;
