import React, { useContext, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import BloodDonationContext from '../context/Contexts';
import Navbar from './Navbar';

const Profile = () => {
    const { userProfile,UpdateInfoForDonor,GetInfoForDonor } = useContext(BloodDonationContext); // Assuming the context provides user profile data and an update function
    const navigate = useNavigate();

    const [formState, setFormState] = useState({
        name: '',
       
        dob: '',
        bloodType: '',
        gender: '',
        fatherName: '',
        state: '',
        district: '',
        pincode: '',
        address: '',
        available: false
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

    const [isFetched, setIsFetched] = useState(false);

    useEffect(() => {
        if (!isFetched) {
            GetInfoForDonor();
            setIsFetched(true);
        } else if (userProfile) {
            setFormState({
                name: userProfile.name,
                // contactNumber: userProfile.contactNumber,
                dob: userProfile.dob,
                bloodType: userProfile.bloodType,
                gender: userProfile.gender,
                fatherName: userProfile.father_Name,
                state: userProfile.state,
                district: userProfile.district,
                pincode: userProfile.pincode,
                address: userProfile.address,
                available: userProfile.available
            });
        }
    }, [userProfile, isFetched]);
    
    const handleChange = (event) => {
        const { name, value, type, checked } = event.target;
        setFormState((prevState) => ({
            ...prevState,
            [name]: type === 'checkbox' ? checked : value
        }));
    };

    const handleSubmit = async () => {
        try {
            // Remove name and contact number from the formState before sending it to the backend
            const { name, contactNumber, ...updateData } = formState;
            console.log(formState);
            await UpdateInfoForDonor(formState);
            alert('Profile updated successfully!');
            navigate('/Donor'); // Redirect to the profile page
        } catch (error) {
            console.error('Error updating profile:', error);
            alert('Failed to update profile.');
        }
    };

    return (
        <>
      <Navbar/>
        <div className="Edit_profile_form">
            <div
                className="rounded shadow-lg p-5 mb-5 bg-body"
                style={{
                    width: '60%',
                    padding: '5%',
                    backgroundColor: 'white',
                    marginTop: '5%'
                }}
            >
                <div className="mb-3">
                    <span>Basic Info</span> <br />
                    <h3 style={{ fontFamily: 'Playfair Display, serif' }}>Edit My Profile</h3>
                </div>
                <form id="profileForm" className="row g-3">
                    <div className="col-md-6">
                        <label htmlFor="inputName" className="form-label">Name</label>
                        <input
                            type="text"
                            className="form-control"
                            id="inputName"
                            name="name"
                            value={formState.name}
                            disabled
                        />
                    </div>
                
                    <div className="col-md-6">
                        <label htmlFor="inputDOB" className="form-label">Date of Birth</label>
                        <input
                            type="date"
                            className="form-control"
                            id="inputDOB"
                            name="dob"
                            value={formState.dob}
                            onChange={handleChange}
                        />
                    </div>
                    {/* <div className="col-md-6">
                        <label htmlFor="inputBloodType" className="form-label">Blood Type</label>
                        <input
                            type="text"
                            className="form-control"
                            id="inputBloodType"
                            name="bloodType"
                            value={formState.bloodType}
                            onChange={handleChange}
                        />
                    </div> */}
                        <div className="col-md-4">
                                    <label htmlFor="inputBloodType" className="form-label">Blood Type</label>
                                    <select
                                        id="inputBloodType"
                                        className="form-select"
                                        name="bloodType"
                                        value={formState.bloodType}
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
                        <label htmlFor="inputGender" className="form-label">Gender</label>
                        <select
                            id="inputGender"
                            className="form-select"
                            name="gender"
                            value={formState.gender}
                            onChange={handleChange}
                        >
                            <option value="" disabled>Choose...</option>
                            <option value="Male">Male</option>
                            <option value="Female">Female</option>
                        </select>
                    </div>
                    <div className="col-md-6">
                        <label htmlFor="inputFatherName" className="form-label">Father's Name</label>
                        <input
                            type="text"
                            className="form-control"
                            id="inputFatherName"
                            name="fatherName"
                            value={formState.fatherName}
                            onChange={handleChange}
                        />
                    </div>
                    <div className="col-md-6">
                    <label htmlFor="inputState" className="form-label">State</label>
                           
                            <select name="State"  className="form-select" value={formState.state} onChange={handleChange}>
                                {/* Map over states array to create options */}
                                {states.map(state => (
                                    <option key={state.name} value={state.name}>{state.name}</option>
                                ))}
                            </select>
                      


                    </div>
                    <div className="col-md-6">
                    <label htmlFor="inputDistrict" className="form-label">District</label>
                            <select name="District" className="form-select" value={formState.district} onChange={handleChange}>
                                {/* Find selected state and map over its districts */}
                                {states.find(state => state.name === formState.state)?.districts.map(district => (
                                    <option key={district} value={district}>{district}</option>
                                ))}
                            </select>
                      
                    </div>
                    <div className="col-md-6">
                        <label htmlFor="inputPincode" className="form-label">Pincode</label>
                        <input
                            type="text"
                            className="form-control"
                            id="inputPincode"
                            name="pincode"
                            value={formState.pincode}
                            onChange={handleChange}
                        />
                    </div>
                    <div className="col-12">
                        <label htmlFor="inputAddress" className="form-label">Address</label>
                        <input
                            type="text"
                            className="form-control"
                            id="inputAddress"
                            name="address"
                            value={formState.address}
                            onChange={handleChange}
                        />
                    </div>
                    <div className="col-12">
                        <div className="form-check">
                            <input
                                className="form-check-input"
                                type="checkbox"
                                id="inputAvailable"
                                name="available"
                                checked={formState.available}
                                onChange={handleChange}
                            />
                            <label className="form-check-label" htmlFor="inputAvailable">
                                Available
                            </label>
                        </div>
                    </div>
                    <div className="d-grid">
                        <button className="btn btn-secondary" type="button" onClick={handleSubmit}>
                            Submit
                        </button>
                    </div>
                </form>
            </div>
        </div>
        </>
    );
};

export default Profile;
