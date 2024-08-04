import React, { useContext, useEffect, useState } from 'react';

import Navbar from '../components/Navbar';
import BloodDonationContext from '../context/Contexts';
import Footer from '../components/Footer';
import '../components/styles.css'
const formatDate = (dateString) => {
    const date = new Date(dateString);
    const day = String(date.getDate()).padStart(2, '0');
    const month = String(date.getMonth() + 1).padStart(2, '0'); // Months are zero-indexed
    const year = date.getFullYear();
    return `${day}-${month}-${year}`;
  };
const Donor = () => {

    const { SearchDonationCenter, DonationCenter, ScheduleAppointmentforme, donorViewRequest, donorViewAppointment,
        RequestInmyDistrict, AllAppointments,CancelmyAppointment,ReschedulemyAppointment,
          DonorwantstoseeAppointments,setDonorwantstoseeAppointments,GetInfoForDonor
    } = useContext(BloodDonationContext);

    useEffect(() => {
        donorViewAppointment();
        donorViewRequest();
        GetInfoForDonor();
    }, []);
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
    
    const [selectedState, setSelectedState] = useState('');
    const [viewrequestdiv, setviewrequestdiv] = useState(true);
    const [viewappointmentdiv, setviewappointmentdiv] = useState(false);
    const [districts, setDistricts] = useState([]);
    const [selectedDistrict, setSelectedDistrict] = useState('');
    const [bloodType, setBloodType] = useState('');
    const [appointmentdate, setappointmentdate] = useState('');
    const [centerid, setcenterid] = useState('');
    const [reappointmentdate, setreappointmentdate] = useState('');
    const [recenterid, setrecenterid] = useState('');
    const [searching, setsearching] = useState(false);
    useEffect(() => {
        if (selectedState) {
            const stateData = states.find(state => state.name === selectedState);
            if (stateData) {
                setDistricts(stateData.districts);
            } else {
                setDistricts([]);
            }
        } else {
            setDistricts([]);
        }
    }, [selectedState, states]);

    const handledateChange = (e) => {
        setappointmentdate(e.target.value);
        console.log(e.target.value);
    };
    const handleredateChange = (e) => {
        setreappointmentdate(e.target.value);
        console.log(e.target.value);
    };
    const handleStateChange = (e) => {
        setSelectedState(e.target.value);
        setSelectedDistrict(''); // Reset district selection when state changes
    };

    const handleDistrictChange = (e) => {
        setSelectedDistrict(e.target.value);
    };

     const changedivvisibilty =()=>{
        if(viewrequestdiv==true){
            setviewappointmentdiv(true);
            setviewrequestdiv(false);
        }
        else {
            setviewappointmentdiv(false);
            setviewrequestdiv(true);
        }
     }
    const handleSubmit = (e) => {
        e.preventDefault();
        // Handle form submission logic here
        console.log(`State: ${selectedState}, District: ${selectedDistrict}`);
        SearchDonationCenter({ state: selectedState, district: selectedDistrict })
        setsearching(true);
        console.log(DonationCenter);

    };
    const ScheduleAppointment = (e) => {
        console.log(`date: ${appointmentdate},centerid: ${centerid}`);
        ScheduleAppointmentforme({ centerid: centerid, date: appointmentdate });
    };

    const ReschedulemyAppointment1 =()=>{
        console.log(`date: ${reappointmentdate},centerid: ${recenterid}`);
        ReschedulemyAppointment({ centerid: recenterid, date: reappointmentdate });
    }

    // const formatDate = (dateString) => {
    //     const date = new Date(dateString);
    //     const day = String(date.getDate()).padStart(2, '0');
    //     const month = String(date.getMonth() + 1).padStart(2, '0'); // Months are zero-indexed
    //     const year = date.getFullYear();
    //     return `${day}-${month}-${year}`;
    //   };
    return (
        <>
            <Navbar />
            <div className='container'>
                <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalLabel">Date Of Appointment</h5>
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <div class="modal-body">
                                <div className="col-md-6">
                                    <label htmlFor="dateOfBirth" className="form-label">maximum allowed date (180 days from today)</label>
                                    <input type="date" className="form-control" id="dateOfBirth" name="dateOfAppointment" onChange={handledateChange} />
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                                <button type="button" class="btn btn-primary" onClick={ScheduleAppointment}>Save changes</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal fade" id="exampleModal1" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalLabel">Reschedule Date</h5>
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <div class="modal-body">
                                <div className="col-md-6">
                                    <label htmlFor="dateOfBirth" className="form-label">maximum allowed date (180 days from today)</label>
                                    <input type="date" className="form-control" id="dateOfBirth" name="dateOfAppointment" onChange={handleredateChange} />
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                                <button type="button" class="btn btn-primary" onClick={ReschedulemyAppointment1}>Save changes</button>
                            </div>
                        </div>
                    </div>
                </div>
                
                <section>
                    <h2>Looking for Donation</h2>
                    <div className='container donationcenters'>
                        <section>
                            <div className='find-donor container'>
                                <form className="row g-3" onSubmit={handleSubmit}>
                                    <div className="col-md-4">
                                        <label htmlFor="inputState" className="form-label">State</label>
                                        <select id="inputState" className="form-select" value={selectedState} onChange={handleStateChange}>
                                            <option value="" disabled>Choose...</option>
                                            {states.map((state, index) => (
                                                <option key={index} value={state.name}>{state.name}</option>
                                            ))}
                                        </select>
                                    </div>

                                    <div className="col-md-4">
                                        <label htmlFor="inputDistrict" className="form-label">District</label>
                                        <select id="inputDistrict" className="form-select" value={selectedDistrict} onChange={handleDistrictChange} disabled={!selectedState}>
                                            <option value="" disabled>Choose...</option>
                                            {districts.map((district, index) => (
                                                <option key={index} value={district}>{district}</option>
                                            ))}
                                        </select>
                                    </div>
                                    <div className="col-12">
                                        <button type="submit" className="btn btn-primary">Search Donation Centers</button>
                                    </div>
                                </form>
                            </div>
                        </section>

                        <section>
                          {searching &&  <div className='container'>
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th scope="col">No.</th>
                                            <th scope="col">Name</th>
                                            <th scope="col">Contact Number</th>
                                            <th scope="col">Address</th>
                                            <th scope="col">Open </th>
                                            <th scope="col">more </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {DonationCenter && DonationCenter.map((item, index) => (
                                            <tr key={index}>
                                                <th id={index} scope="row">{index + 1}</th>
                                                <td>{item.name}</td>
                                                <td>{item.contactInfo}</td>
                                                <td>{item.address}</td>
                                                <td>{item.operatingHours}</td>
                                                <td><button type="button" class="btn btn-primary" data-bs-toggle="modal" onClick={() => setcenterid(item.centerId)} data-bs-target="#exampleModal">
                                                    Schedule Appointment
                                                </button></td>
                                            </tr>
                                        ))}

                                    </tbody>
                                </table>
                            </div>}
                        </section>
                    </div>
                </section>

                <section>
                    <div className='container mt-5'>
                        <div className='heading-option d-flex justify-content-around'>
                            <button type="button" class="btn btn-outline-secondary" onClick={changedivvisibilty} >New Requests</button>
                            <button type="button" class="btn btn-outline-success" onClick={changedivvisibilty}>Your Appointments</button>
                        </div>
                        <div className='donor-content'>
                        { viewrequestdiv &&    <div className='content'>
                                <h2>New Blood Requests</h2>
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th scope="col">No.</th>
                                            <th scope="col">Name</th>
                                            <th scope="col">Contact</th>
                                            <th scope="col">BloodType</th>
                                            <th scope="col">State</th>
                                            <th scope="col">District</th>
                                            <th scope="col">Quantity</th>
                                            <th scope="col">RequestDate</th>
                                            <th scope="col">IsUrgent</th>
                                            <th scope="col">Status</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {RequestInmyDistrict && RequestInmyDistrict
                                        .filter(item =>  item.status==="Pending")
                                        .map((item, index) => (
                                          <tr>
                                                <th id={index} key={index} scope="row">{index + 1}</th>
                                                <td>{item.name}</td>
                                                <td>{item.contact}</td>
                                                <td>{item.bloodType}</td>
                                                <td>{item.state}</td>
                                                <td>{item.district}</td>
                                                <td>{item.quantity}</td>
                                                <td>{formatDate(item.requestDate)}</td>
                                                <td>{item.isUrgent == true ? "Yes" : " No"}</td>
                                                <td>{item.status}</td>
                                            </tr>
                                        ))}
                                        

                                    </tbody>
                                </table>

                            </div>}
                          {viewappointmentdiv &&   <div className='content'>
                                <h2>Your upcoming Appointments</h2>
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th scope="col">No.</th>
                                           
                                            <th scope="col">Date</th>
                                            <th scope="col">Location</th>
                                            <th scope="col">Status</th>
                                            <th scope="col">Reschedule</th>
                                            <th scope="col">Cancel</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {AllAppointments && AllAppointments
                                        .filter(item => item.status==="Scheduled")
                                        .map((item, index) => (
                                            <tr>
                                                <th id={index} key={index} scope="row">{index + 1}</th>
                                                <td>{formatDate(item.appointmentDate)}</td>
                                                <td>{item.location}</td>
                                                 <td>{item.status}</td>
                                                 <td>
                                                 <button type="button" class="btn btn-primary" data-bs-toggle="modal" onClick={() => setrecenterid(item.appointmentId)} data-bs-target="#exampleModal1">
                                                    Reschedule
                                                </button>
                                                </td>
                                                 <td><button type="button" class="btn btn-danger" onClick={()=> CancelmyAppointment(item.appointmentId)}>Cancel</button></td>
                                            </tr>
                                        ))}
                                    </tbody>
                                </table>

                            </div>}
                        </div>
                    </div>
                </section>
           { DonorwantstoseeAppointments===true &&    <section>
                    <div className='container'>
                       
                        <div className='donor-content'>

                          {  <div className='content'>
                                <h2>All Appointments</h2>
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th scope="col">No.</th>
                                           
                                            <th scope="col">Date</th>
                                            <th scope="col">Location</th>
                                            <th scope="col">Status</th>
                                       
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {AllAppointments && AllAppointments.map((item, index) => (
                                           <tr>
                                                <th id={index} key={index} scope="row">{index + 1}</th>
                                                <td>{formatDate(item.appointmentDate)}</td>
                                                <td>{item.location}</td>
                                                 <td>{item.status}</td>
                                             </tr>
                                        ))}
                                    </tbody>
                                </table>

                            </div>}
                        </div>
                    </div>
                </section>}
            </div>

     <Footer/>
        </>
    );
}


export default Donor;