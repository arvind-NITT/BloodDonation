import React, { useContext, useEffect, useState } from 'react';

import Navbar from '../components/Navbar';
import BloodDonationContext from '../context/Contexts';

const DonationCenter = () => {

    const { CancelmyAppointment, ReschedulemyAppointment, BloodbankAllAppointment,
         AllBloodbankAppointments, AppointmentCencelledByBloodBank
        , AppointmentUpdateByBloodBank ,RescheduleAppointmentByBloodBank,AvailabilityCheck,
        AvailableBlood} = useContext(BloodDonationContext);

        const [reappointmentdate, setreappointmentdate] = useState('');
        const [recenterid, setrecenterid] = useState('');

    useEffect(() => {
        BloodbankAllAppointment();
        AvailabilityCheck();
    }, []);

    const handleredateChange = (e) => {
        setreappointmentdate(e.target.value);
        console.log(e.target.value);
    };
    const ReschedulemyAppointment1 =()=>{
        console.log(`date: ${reappointmentdate},centerid: ${recenterid}`);
        RescheduleAppointmentByBloodBank({ centerid: recenterid, date: reappointmentdate });
    }

    return (
        <>
             <Navbar/>
            <section>
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
                            <h2>Your upcoming scheduled appointments</h2>
                            <table class="table">
                                <thead>
                                    <tr>
                                        <th scope="col">No.</th>

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
                                            <td>{item.appointmentDate}</td>
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
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {
                                            AvailableBlood.length > 0  && AvailableBlood.map((item,index)=>(
                                                <tr key={index}>
                                            <td>{index+1}</td>
                                            <td>{item.bloodType}</td>
                                            <td>{item.quantity}</td>
                                            {/* <td>{item.status}</td> */}
                                        </tr>
                                            ))
                                        }
                                        <tr>
                                            <td>A+</td>
                                            <td>A+ AB+</td>
                                            <td>A+ A- O+ O-</td>
                                        </tr>
                                        <tr>
                                            <td>O+</td>
                                            <td>O+ A+ B+ AB+</td>
                                            <td>O+ O-</td>
                                        </tr>
                                        <tr>
                                            <td>B+</td>
                                            <td>B+ AB+</td>
                                            <td>B+ B- O+ O-</td>
                                        </tr>
                                        <tr>
                                            <td>AB+</td>
                                            <td>AB+</td>
                                            <td>Everyone</td>
                                        </tr>
                                        <tr>
                                            <td>A-</td>
                                            <td>A+ A- AB+ AB-</td>
                                            <td>A- O-</td>
                                        </tr>
                                        <tr>
                                            <td>O-</td>
                                            <td>Everyone</td>
                                            <td>O-</td>
                                        </tr>
                                        <tr>
                                            <td>B-</td>
                                            <td>B+ B- AB+ AB-</td>
                                            <td>B- O-</td>
                                        </tr>
                                        <tr>
                                            <td>AB-</td>
                                            <td>AB+ AB-</td>
                                            <td>AB- A- B- O-</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                </div>
            </section>
        </>
    );


}
export default DonationCenter;