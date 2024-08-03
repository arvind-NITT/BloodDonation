import React, { useContext, useEffect, useState } from 'react';

import Navbar from '../components/Navbar';
import BloodDonationContext from '../context/Contexts';
import Footer from '../components/Footer';
const formatDate = (dateString) => {
    const date = new Date(dateString);
    const day = String(date.getDate()).padStart(2, '0');
    const month = String(date.getMonth() + 1).padStart(2, '0'); // Months are zero-indexed
    const year = date.getFullYear();
    return `${day}-${month}-${year}`;
  };
const DonationCenter = () => {

    const { CancelmyAppointment, ReschedulemyAppointment, BloodbankAllAppointment,
         AllBloodbankAppointments, AppointmentCencelledByBloodBank
        , AppointmentUpdateByBloodBank ,RescheduleAppointmentByBloodBank,AvailabilityCheck,
        AvailableBlood,UpdateInventory,BloodBankwantstoseeAppointments} = useContext(BloodDonationContext);
        const [Unit, setUnit] = useState('');
        const [Inventoryid, setInventoryid] = useState('');
        const [reappointmentdate, setreappointmentdate] = useState('');
        const [recenterid, setrecenterid] = useState('');

    useEffect(() => {
        BloodbankAllAppointment();
        AvailabilityCheck();
    }, []);
    const handleUnitChange = (e) => {
        setUnit(e.target.value);
        console.log(e.target.value);
    };
    const handleredateChange = (e) => {
        setreappointmentdate(e.target.value);
        console.log(e.target.value);
    };
    const ReschedulemyAppointment1 =()=>{
        console.log(`date: ${reappointmentdate},centerid: ${recenterid}`);
        RescheduleAppointmentByBloodBank({ centerid: recenterid, date: reappointmentdate });
    }
    const handleSubmit = (e) => {
        e.preventDefault();
        // Handle form submission logic here
        console.log(`Unit: ${Unit}, Inventoryid: ${Inventoryid}`);
        UpdateInventory({Unit,Inventoryid})
    };


    return (
        <>
             <Navbar/>
            <section>
            <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalLabel">Enter Unit</h5>
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <div class="modal-body">
                                <div className="col-md-6">
                                    <label htmlFor="dateOfBirth" className="form-label"> Unit</label>
                                    <input type="number" className="form-control" id="Unit" name="Unit" onChange={handleUnitChange} />
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                                <button type="button" class="btn btn-primary" onClick={handleSubmit}>Save changes</button>
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

                <div className='container centerAppointments'>
                    <div className='heading-option'>
                        {/* <button type="button" class="btn btn-outline-secondary" onClick={changedivvisibilty} >New Requests</button>
                            <button type="button" class="btn btn-outline-success" onClick={changedivvisibilty}>Your Appointments</button> */}
                    </div>
                    <div className='centerAppointments-content'>
                        {<div className='content'>
                            <h2>Upcoming scheduled appointments</h2>
                            <table class="table">
                                <thead>
                                    <tr>
                                        <th scope="col">No.</th>
                                        <th scope="col">Name</th>
                                        <th scope="col">Contact</th>
                                        <th scope="col">Date</th>
                                        <th scope="col">Location</th>
                                        <th scope="col">Status</th>
                                        <th scope="col">Done</th>
                                        <th scope="col">Reschedule</th>
                                        <th scope="col">Cancel</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {AllBloodbankAppointments.length >0  && AllBloodbankAppointments.map((item, index) => (

                                      item.status==="Scheduled" &&  <tr>
                                            <th id={index} key={index} scope="row">{index + 1}</th>
                                            <td>{item.name}</td>
                                            <td>{item.phoneNumber}</td>
                                            <td>{formatDate(item.appointmentDate)}</td>
                                            <td>{item.location}</td>
                                            <td>{item.status}</td>
                                            <td><button type="button" class="btn btn-danger" onClick={() => AppointmentUpdateByBloodBank(item.appointmentId)}>Done</button></td>
                                            <td>
                                                <button type="button" class="btn btn-primary" data-bs-toggle="modal" onClick={() => setrecenterid(item.appointmentId)} data-bs-target="#exampleModal1">
                                                    Reschedule
                                                </button>
                                            </td>
                                            <td><button type="button" class="btn btn-danger" onClick={() => AppointmentCencelledByBloodBank(item.appointmentId)}>Cancel</button></td>
                                        </tr>
                                    ))}
                                </tbody>
                            </table>

                        </div>}
                    </div>
                </div>
            </section>

            <section>
                <div className='container  inventory'>
                <div className='col'>
                            <h2>Available Blood Type</h2>
                            <div className="table-responsive">
                                <table className="table table-bordered table-hover">
                                    <thead className="thead-dark">
                                        <tr>
                                            <th>S. No</th>
                                            <th>Blood Type</th>
                                            <th>Unit</th>
                                            <th>Update</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {
                                            AvailableBlood.length > 0  && AvailableBlood.map((item,index)=>(
                                                <tr key={index}>
                                            <td>{index+1}</td>
                                            <td>{item.bloodType}</td>
                                            <td>{item.quantity}</td>
                                            <td><button type="button" class="btn btn-primary" data-bs-toggle="modal" onClick={() => setInventoryid(item.inventoryId)} data-bs-target="#exampleModal">
                                                    Update
                                                </button></td>
                                            {/* <td>{item.status}</td> */}
                                        </tr>
                                            ))
                                        }
                     
                                    </tbody>
                                </table>
                            </div>
                        </div>
                </div>
            </section>


        {  BloodBankwantstoseeAppointments==true &&  <section>
            <div className='container centerAppointments'>
                    <div className='heading-option'>
                        {/* <button type="button" class="btn btn-outline-secondary" onClick={changedivvisibilty} >New Requests</button>
                            <button type="button" class="btn btn-outline-success" onClick={changedivvisibilty}>Your Appointments</button> */}
                    </div>
                    <div className='centerAppointments-content'>
                        {<div className='content'>
                            <h2> Appointments</h2>
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
                                    {AllBloodbankAppointments.length >0  && AllBloodbankAppointments.map((item, index) => (

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
            </section>
}

  <Footer/>
        </>
    );


}
export default DonationCenter;