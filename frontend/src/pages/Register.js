import React, { useContext, useEffect, useState } from 'react';
import Navbar from '../components/Navbar';
import BloodDonationContext from '../context/Contexts';

import { useNavigate } from 'react-router-dom';
import { Link } from 'react-router-dom';
const Register = () => {
  const BACKENDLINK = process.env.REACT_APP_BACKEND_LINK;
  const {usertype,setRole } = useContext(BloodDonationContext);

  useEffect(() => {
    console.log(usertype);
    
}, []);


    const navigate = useNavigate();
  const [formData, setFormData] = useState({
    name: '',
    email: '',
    password: '',
    contact: '',
    dateOfBirth: '',
    address: '',
    state: '',
    district: '',
    zip: '',
    bloodType: '',
    gender: '',
    fatherName: '',
    role: '',
    available: true,
    medicalCondition: ''
  });

  const [states, setStates] = useState([
    { name: 'Andhra Pradesh', districts: ['Anantapur', 'Chittoor', 'East Godavari', 'Guntur', 'Krishna', 'Kurnool', 'Prakasam', 'Srikakulam', 'Sri Potti Sriramulu Nellore', 'Visakhapatnam', 'Vizianagaram', 'West Godavari', 'YSR Kadapa'] },
    { name: 'Arunachal Pradesh', districts: ['Tawang', 'West Kameng', 'East Kameng', 'Papum Pare', 'Kurung Kumey', 'Kra Daadi', 'Lower Subansiri', 'Upper Subansiri', 'West Siang', 'East Siang', 'Siang', 'Upper Siang', 'Lower Siang', 'Lower Dibang Valley', 'Dibang Valley', 'Anjaw', 'Lohit', 'Namsai', 'Changlang', 'Tirap', 'Longding'] },
    { name: 'Assam', districts: ['Baksa', 'Barpeta', 'Biswanath', 'Bongaigaon', 'Cachar', 'Charaideo', 'Chirang', 'Darrang', 'Dhemaji', 'Dhubri', 'Dibrugarh', 'Goalpara', 'Golaghat', 'Hailakandi', 'Hojai', 'Jorhat', 'Kamrup Metropolitan', 'Kamrup', 'Karbi Anglong', 'Karimganj', 'Kokrajhar', 'Lakhimpur', 'Majuli', 'Morigaon', 'Nagaon', 'Nalbari', 'Dima Hasao', 'Sivasagar', 'Sonitpur', 'South Salmara-Mankachar', 'Tinsukia', 'Udalguri', 'West Karbi Anglong'] },
    { name: 'Bihar', districts: ['Araria', 'Arwal', 'Aurangabad', 'Banka', 'Begusarai', 'Bhagalpur', 'Bhojpur', 'Buxar', 'Darbhanga', 'East Champaran (Motihari)', 'Gaya', 'Gopalganj', 'Jamui', 'Jehanabad', 'Kaimur (Bhabua)', 'Katihar', 'Khagaria', 'Kishanganj', 'Lakhisarai', 'Madhepura', 'Madhubani', 'Munger (Monghyr)', 'Muzaffarpur', 'Nalanda', 'Nawada', 'Patna', 'Purnia (Purnea)', 'Rohtas', 'Saharsa', 'Samastipur', 'Saran', 'Sheikhpura', 'Sheohar', 'Sitamarhi', 'Siwan', 'Supaul', 'Vaishali', 'West Champaran'] },
    { name: 'Chhattisgarh', districts: ['Balod', 'Baloda Bazar', 'Balrampur', 'Bastar', 'Bemetara', 'Bijapur', 'Bilaspur', 'Dantewada (South Bastar)', 'Dhamtari', 'Durg', 'Gariyaband', 'Janjgir-Champa', 'Jashpur', 'Kabirdham (Kawardha)', 'Kanker (North Bastar)', 'Kondagaon', 'Korba', 'Koriya', 'Mahasamund', 'Mungeli', 'Narayanpur', 'Raigarh', 'Raipur', 'Rajnandgaon', 'Sukma', 'Surajpur', 'Surguja'] },
    { name: 'Goa', districts: ['North Goa', 'South Goa'] },
    { name: 'Gujarat', districts: ['Ahmedabad', 'Amreli', 'Anand', 'Aravalli', 'Banaskantha (Palanpur)', 'Bharuch', 'Bhavnagar', 'Botad', 'Chhota Udepur', 'Dahod', 'Dangs (Ahwa)', 'Devbhoomi Dwarka', 'Gandhinagar', 'Gir Somnath', 'Jamnagar', 'Junagadh', 'Kheda (Nadiad)', 'Kutch', 'Mahisagar', 'Mehsana', 'Morbi', 'Narmada (Rajpipla)', 'Navsari', 'Panchmahal (Godhra)', 'Patan', 'Porbandar', 'Rajkot', 'Sabarkantha (Himmatnagar)', 'Surat', 'Surendranagar', 'Tapi (Vyara)', 'Vadodara', 'Valsad'] },
    { name: 'Haryana', districts: ['Ambala', 'Bhiwani', 'Charkhi Dadri', 'Faridabad', 'Fatehabad', 'Gurugram', 'Hisar', 'Jind', 'Kaithal', 'Karnal', 'Mahendragarh', 'Panchkula', 'Panipat', 'Rewari', 'Sirsa', 'Sonipat', 'Yamunanagar'] },
    { name: 'Himachal Pradesh', districts: ['Bilaspur', 'Chamba', 'Hamirpur', 'Kangra', 'Kinnaur', 'Kullu', 'Lahaul and Spiti', 'Mandi', 'Shimla', 'Sirmaur', 'Solan', 'Una'] },
    { name: 'Jharkhand', districts: ['Bokaro', 'Chatra', 'Deoghar', 'Dhanbad', 'Dumka', 'East Singhbhum', 'Garhwa', 'Giridih', 'Godda', 'Gumla', 'Hazaribagh', 'Jamtara', 'Khunti', 'Koderma', 'Latehar', 'Lohardaga', 'Pakur', 'Palamu', 'Ramgarh', 'Ranchi', 'Sahebganj', 'Seraikela-Kharsawan', 'West Singhbhum'] },
    { name: 'Karnataka', districts: ['Bagalkote', 'Bangalore Rural', 'Bangalore Urban', 'Belagavi', 'Ballari', 'Bidar', 'Vijayapura', 'Chamarajanagar', 'Chikballapur', 'Chikkamagaluru', 'Chitradurga', 'Dakshina Kannada', 'Davangere', 'Dharwad', 'Gadag', 'Hassan', 'Haveri', 'Kodagu', 'Kolar', 'Koppal', 'Mandya', 'Mysuru', 'Raichur', 'Ramanagara', 'Shimoga', 'Tumkur', 'Udupi', 'Yadgir'] },
    { name: 'Kerala', districts: ['Alappuzha', 'Ernakulam', 'Idukki', 'Kannur', 'Kasaragod', 'Kollam', 'Kottayam', 'Kozhikode', 'Malappuram', 'Palakkad', 'Pathanamthitta', 'Thrissur', 'Wayanad'] },
    { name: 'Ladakh', districts: ['Leh', 'Kargil'] },
    { name: 'Lakshadweep', districts: ['Agatti', 'Amini', 'Andrott', 'Bangaram', 'Bithra', 'Kalapeni', 'Kavaratti', 'Kiltan', 'Minicoy', 'Suheli Atoll'] },
    { name: 'Madhya Pradesh', districts: ['Agar Malwa', 'Alirajpur', 'Anuppur', 'Ashok Nagar', 'Balaghat', 'Barwani', 'Betul', 'Bhind', 'Bhopal', 'Burhanpur', 'Chhindwara', 'Dewas', 'Dhar', 'Dindori', 'East Nimar (Khandwa)', 'Guna', 'Gwalior', 'Harda', 'Hoshangabad', 'Indore', 'Jabalpur', 'Jhabua', 'Katni', 'Khandwa', 'Khargone', 'Mandla', 'Mandsaur', 'Morena', 'Narsinghpur', 'Neemuch', 'Panna', 'Raisen', 'Rajgarh', 'Ratlam', 'Rewa', 'Sagar', 'Satna', 'Sehore', 'Senapati', 'Shahdol', 'Shajapur', 'Sheopur', 'Shivpuri', 'Sidhi', 'Singrauli', 'Tikamgarh', 'Ujjain', 'Umaria', 'Vidisha'] },
    { name: 'Maharashtra', districts: ['Ahmednagar', 'Akola', 'Amravati', 'Aurangabad', 'Beed', 'Bhandara', 'Buldhana', 'Chandrapur', 'Dhule', 'Gadchiroli', 'Gondia', 'Hingoli', 'Jalgaon', 'Jalna', 'Kolhapur', 'Latur', 'Mumbai City', 'Mumbai Suburban', 'Nagpur', 'Nanded', 'Nandurbar', 'Nashik', 'Osmanabad', 'Palghar', 'Pune', 'Raigad', 'Ratnagiri', 'Satara', 'Sindhudurg', 'Solapur', 'Thane', 'Wardha', 'Washim', 'Yavatmal'] },
    { name: 'Manipur', districts: ['Bishnupur', 'Churachandpur', 'Imphal East', 'Imphal West', 'Jiribam', 'Kakching', 'Kamjong', 'Kangpokpi', 'Noney', 'Pherzawl', 'Senapati', 'Tamenglong', 'Thoubal', 'Ukhrul'] },
    { name: 'Meghalaya', districts: ['East Garo Hills', 'East Khasi Hills', 'Jaintia Hills', 'Ribhoi', 'West Garo Hills', 'West Jaintia Hills', 'West Khasi Hills'] },
    { name: 'Mizoram', districts: ['Aizawl', 'Champhai', 'Hnahthial', 'Kolasib', 'Lawngtlai', 'Lunglei', 'Mamit', 'Saiha', 'Serchhip'] },
    { name: 'Nagaland', districts: ['Dimapur', 'Kiphire', 'Kohima', 'Longleng', 'Mokokchung', 'Mon', 'Peren', 'Phek', 'Wokha', 'Zunheboto'] },
    { name: 'Odisha', districts: ['Angul', 'Boudh', 'Bhadrak', 'Balangir', 'Balasore', 'Baragarh', 'Bargarh', 'Cuttack', 'Dhenkanal', 'Gajapati', 'Ganjam', 'Jagatsinghpur', 'Jajpur', 'Jharsuguda', 'Kalahandi', 'Kandhamal', 'Kendrapara', 'Kendujhar', 'Khurda', 'Malkangiri', 'Nabarangpur', 'Nayagarh', 'Nuapada', 'Rayagada', 'Sambalpur', 'Subarnapur', 'Sundergarh'] },
    { name: 'Puducherry', districts: ['Karaikal', 'Mahe', 'Puducherry', 'Yanam'] },
    { name: 'Punjab', districts: ['Amritsar', 'Barnala', 'Bathinda', 'Faridkot', 'Fatehgarh Sahib', 'Fazilka', 'Firozepur', 'Gurdaspur', 'Hoshiarpur', 'Jalandhar', 'Kapurthala', 'Ludhiana', 'Mansa', 'Moga', 'Muktsar', 'Patiala', 'Rupnagar', 'S.A.S. Nagar', 'Sangrur', 'Tarn Taran'] },
    { name: 'Rajasthan', districts: ['Ajmer', 'Alwar', 'Banswara', 'Baran', 'Barmer', 'Bhilwara', 'Bikaner', 'Bundi', 'Chittorgarh', 'Churu', 'Dausa', 'Dholpur', 'Dungarpur', 'Hanumangarh', 'Jaipur', 'Jaisalmer', 'Jalore', 'Jhalawar', 'Jhunjhunu', 'Jodhpur', 'Karauli', 'Nagaur', 'Pali', 'Rajsmand', 'Sawai Madhopur', 'Sikar', 'Sirohi', 'Tonk', 'Udaipur'] },
    { name: 'Sikkim', districts: ['East Sikkim', 'North Sikkim', 'South Sikkim', 'West Sikkim'] },
    { name: 'Tamil Nadu', districts: ['Ariyalur', 'Chengalpattu', 'Chennai', 'Coimbatore', 'Cuddalore', 'Dharmapuri', 'Dindigul', 'Erode', 'Kallakurichi', 'Kancheepuram', 'Kanyakumari', 'Karur', 'Krishnagiri', 'Madurai', 'Nagapattinam', 'Namakkal', 'Nilgiris', 'Perambalur', 'Pudukkottai', 'Ramanathapuram', 'Ranipet', 'Salem', 'Sivagangai', 'Tenkasi', 'Thanjavur', 'Theni', 'Tiruchirappalli', 'Tirunelveli', 'Tirupathur', 'Tirupur', 'Tiruvallur', 'Tiruvannamalai', 'Vellore', 'Viluppuram', 'Virudhunagar'] },
    { name: 'Telangana', districts: ['Adilabad', 'Bhadradri Kothagudem', 'Hyderabad', 'Jagtial', 'Jangoan', 'J Bhadradri Kothagudem', 'Jangaon', 'Jayashankar Bhupalpally', 'Jogulamba Gadwal', 'Kamareddy', 'Karimnagar', 'Khammam', 'Mahabubabad', 'Mahabubnagar', 'Mancherial', 'Medak', 'Medchal', 'Nalgonda', 'Nirmal', 'Nizamabad', 'Peddapalli', 'Rajanna Sircilla', 'Rangareddy', 'Warangal Rural', 'Warangal Urban', 'Yadadri Bhongir'] },
    { name: 'Tripura', districts: ['Dhalai', 'Khowai', 'North Tripura', 'Sepahijala', 'South Tripura', 'Unakoti', 'West Tripura'] },
    { name: 'Uttar Pradesh', districts: ['Agra', 'Aligarh', 'Ambedkar Nagar', 'Amethi', 'Amroha', 'Auraiya', 'Azamgarh', 'Baghpat', 'Bahraich', 'Ballia', 'Balrampur', 'Banda', 'Barabanki', 'Bareilly', 'Basti', 'Bhadohi', 'Bijnor', 'Budaun', 'Bulandshahr', 'Chandauli', 'Chitrakoot', 'Deoria', 'Etah', 'Etawah', 'Farrukhabad', 'Fatehpur', 'Firozabad', 'Gautam Buddha Nagar', 'Ghaziabad', 'Ghazipur', 'Gonda', 'Gorakhpur', 'Hamirpur', 'Hapur', 'Hardoi', 'Hathras', 'Jalaun', 'Jaunpur', 'Jhansi', 'Kannauj', 'Kanpur Dehat', 'Kanpur Nagar', 'Kanshiram Nagar', 'Kushinagar', 'Lakhimpur Kheri', 'Lalitpur', 'Lucknow', 'Maharajganj', 'Mahoba', 'Manpuri', 'Mathura', 'Meerut', 'Mirzapur', 'Moradabad', 'Muzaffarnagar', 'Pilibhit', 'Pratapgarh', 'Raebareli', 'Rae Bareli', 'Rampur', 'Saharanpur', 'Sambhal', 'Sant Kabir Nagar', 'Sant Ravidas Nagar', 'Shahjahanpur', 'Shamli', 'Shravasti', 'Siddharthnagar', 'Sitapur', 'Sonbhadra', 'Sultanpur', 'Unnao', 'Varanasi'] },
    { name: 'Uttarakhand', districts: ['Almora', 'Bageshwar', 'Chamoli', 'Champawat', 'Dehradun', 'Haridwar', 'Nainital', 'Pauri Garhwal', 'Pithoragarh', 'Rudraprayag', 'Tehri Garhwal', 'Udham Singh Nagar', 'Uttarkashi'] },
    { name: 'West Bengal', districts: ['Alipurduar', 'Bankura', 'Birbhum', 'Burdwan', 'Cooch Behar', 'Dakshin Dinajpur', 'Darjeeling', 'Hooghly', 'Howrah', 'Jalpaiguri', 'Jhargram', 'Kalimpong', 'Kolkata', 'Maldah', 'Murarai', 'Nadia', 'North 24 Parganas', 'Paschim Bardhaman', 'Paschim Medinipur', 'Purba Bardhaman', 'Purba Medinipur', 'Purulia', 'South 24 Parganas', 'Uttar Dinajpur'] },
]);

  const handleStateChange = async(e) => {
    const selectedState = e.target.value;
    setFormData({ ...formData, state: selectedState, district: '' });
  };

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };
  const isValidPhoneNumber = (phoneNumber) => {
    return /^\d{10}$/.test(phoneNumber);
  };
  const handleSubmit =async (e) => {

    try{
    e.preventDefault();
    if (!isValidAge(formData.dateOfBirth)) {
      alert("Age must be 18 or above. You are not allowed to proceed.");
      return;
    }
    if (!isValidPhoneNumber(formData.contact)) {
      alert("Phone number must be a 10-digit number.");
      return;
    }
    const response= await  fetch(`${BACKENDLINK}/api/User/Register`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Access-Control-Allow-Origin': "*"

      },
      body: JSON.stringify({
  
        "name": formData.name,
        "email": formData.email,
        "phoneNumber": formData.contact,
        "dob": formData.dateOfBirth,
        "bloodType": formData.bloodType,
        "gender": formData.gender,
        "father_Name": formData.fatherName,
        "state": formData.state,
        "district": formData.district,
        "pincode": formData.zip,
        "address":formData.address,
        "role": usertype,
        "password": formData.password,
        "available": formData.available,
        "medicalCondition": formData.medicalCondition
      })
    })
    const data= await response.json();
    if(!response.ok){
      alert(data.message)
  
      console.log('Response:', data);
      return ;
     }
      console.log('Response:', data);
      localStorage.setItem('token', data.token);
      localStorage.setItem('UserID', data.UserID);
      localStorage.setItem('Role', data.role);
      
      setRole(data.role);
       alert("Registration done");
       if(data.role === "Recipient")
        navigate('/Recipient');
        else if(data.role === "Donor")
        navigate('/Donor');
        else if(data.role === "Bloodbank")
        navigate('/BloodBank');
      }catch(error)
    {  console.error('Error:', error);
      alert(error);}
    ;
  };

  const isValidAge = (dateOfBirth) => {
    var dob = new Date(dateOfBirth);
    var today = new Date();
    var age = today.getFullYear() - dob.getFullYear();
    var monthDifference = today.getMonth() - dob.getMonth();
    if (monthDifference < 0 || (monthDifference === 0 && today.getDate() < dob.getDate())) {
      age--;
    }
    return age >= 18;
  };

  return (
    <div className="container w-75 d-flex p-5">
      <div className="LeftContainer" style={{ fontFamily: 'Playfair Display, serif', backgroundColor: 'rgb(250, 210, 157)' }}>
        <h2>Now</h2>
        <h1 style={{ color: 'rgb(68, 59, 51)', fontWeight: 800, fontSize: '55px' }}>Find Your Blood Buddy</h1>
        <h2>Easy and fast</h2>
       
       
      </div>
      <form className="row w-75 container container-md bg-white g-3 p-5 rounded" id="RegisterForm" onSubmit={handleSubmit}>
        <div className="heading_login">
          <h7>START FOR FREE</h7>
          <h2>SignUp to India's most used blood donation Website</h2>
          <h7>Already a member? <Link to="/login">Login</Link></h7>
        </div>
        <div className="col-12">
          <label htmlFor="name" className="form-label">Full Name</label>
          <input type="text" className="form-control" id="name" name="name" onChange={handleChange} />
        </div>
        <div className="col-md-6">
          <label htmlFor="email" className="form-label">Email</label>
          <input type="email" className="form-control" id="email" name="email" onChange={handleChange} />
        </div>
        <div className="col-md-6">
          <label htmlFor="password" className="form-label">Password</label>
          <input type="password" className="form-control" id="password" name="password" onChange={handleChange} />
        </div>
        <div className="col-md-6">
          <label htmlFor="contact" className="form-label">Contact Number</label>
          <input type="text" className="form-control" id="contact" name="contact" onChange={handleChange} />
        </div>
        <div className="col-md-6">
          <label htmlFor="dateOfBirth" className="form-label">Date Of Birth</label>
          <input type="date" className="form-control" id="dateOfBirth" name="dateOfBirth" onChange={handleChange} />
        </div>
        <div className="col-md-6">
    <label htmlFor="state" className="form-label">State:</label>
    <select id="state" name="state" className="form-select"  value={formData.state} onChange={handleStateChange}>
      {states.map((state) => (
        <option  key={state.name} value={state.name}>{state.name}</option>
      ))}
    </select>
  </div>
  
  <div className="col-md-6">
    <label htmlFor="district" className="form-label">District:</label>
    <select className="form-select" id="district" name="district" value={formData.district} onChange={handleChange} disabled={!formData.state}>
      {states.find((state) => state.name === formData.state)?.districts.map((district) => (
        <option key={district} value={district}>{district}</option>
      ))}
    </select>
  </div>
        <div className="col-12">
          <label htmlFor="address" className="form-label">Address</label>
          <input type="text" className="form-control" id="address" name="address" onChange={handleChange} placeholder="1234 Main St" />
        </div>
        <div className="col-md-6">
          <label htmlFor="zip" className="form-label">Zip</label>
          <input type="text" className="form-control" id="zip" name="zip" onChange={handleChange} />
        </div>
        {/* <div className="col-md-6">
          <label htmlFor="bloodType" className="form-label">Blood Type</label>
          <input type="text" className="form-control" id="bloodType" name="bloodType" onChange={handleChange} />
        </div> */}
         <div className="col-md-4">
                                    <label htmlFor="inputBloodType" className="form-label">Blood Type</label>
                                    <select
                                       id="bloodType" 
                                       name="bloodType"
                                        className="form-select"
                                       
                                        onChange={handleChange}
                                    >
                                        <option value="" disabled>Choose...</option>
                                        <option value="A+">A+</option>
                                        <option value="O+">O+</option>
                                        <option value="B+">B+</option>
                                        <option value="AB+">AB+</option>
                                        <option value="A-">A-</option>
                                        <option value="O-">O-</option>
                                        <option value="B-">B-</option>
                                        <option value="AB-">AB-</option>
                                    </select>
                                </div>
        <div className="col-md-6">
          <label htmlFor="gender" className="form-label">Gender</label>
          <input type="text" className="form-control" id="gender" name="gender" onChange={handleChange} />
        </div>
        <div className="col-md-6">
          <label htmlFor="fatherName" className="form-label">Father's Name</label>
          <input type="text" className="form-control" id="fatherName" name="fatherName" onChange={handleChange} />
        </div>
        {usertype && usertype === "Recipient" && (
        <div className="col-12">
          <label htmlFor="medicalCondition" className="form-label">Medical Condition</label>
          <textarea
            className="form-control"
            id="medicalCondition"
            name="medicalCondition"
            value={formData.medicalCondition}
            onChange={handleChange}
            rows="3"
          ></textarea>
        </div>
      )}

      {usertype && usertype === "Donor" && (
        <div className="col-12">
          <div className="form-check">
            <input
              className="form-check-input"
              type="checkbox"
              id="gridCheck"
              name="available"
              checked={formData.available}
              onChange={e => setFormData({ ...formData, available: e.target.checked })}
            />
            <label className="form-check-label" htmlFor="gridCheck">
              Available for blood donation
            </label>
          </div>
        </div>
      )}
        <div className="col-12">
          <button type="submit" className="btn btn-primary">Register</button>
        </div>
      </form>
    </div>
  );
}

export default Register;
