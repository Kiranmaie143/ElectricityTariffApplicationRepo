//import React, { useState, useEffect } from 'react';
//import axios from 'axios';

//const TariffList = () => {
//    // State for storing the tariffs, loading state, and error state
//    const [tariffs, setTariffs] = useState([]);
//    const [loading, setLoading] = useState(true);
//    const [error, setError] = useState(null);

//    // Fetch tariff data when the component mounts
//    useEffect(() => {
//        axios.get('http://localhost:5062/api/tariffs')  // Replace with your actual API endpoint
//            .then((response) => {
//                setTariffs(response.data);  // Update tariffs with the response data
//                setLoading(false);          // Set loading to false
//            })
//            .catch((error) => {
//                setError(error.message);    // Capture any error message
//                setLoading(false);          // Set loading to false
//            });
//    }, []);

//    // If still loading, show a loading message
//    if (loading) return <p>Loading...</p>;

//    // If there's an error, display the error message
//    if (error) return <p>Error: {error}</p>;

//    // Render the list of tariffs
//    return (
//        <div>
//            <h1>Available Tariffs</h1>
//            <ul>
//                {tariffs.map((tariff) => (
//                    <li key={tariff.name}>
//                        <strong>{tariff.name}</strong>: {tariff.annualCost} €/year
//                    </li>
//                ))}
//            </ul>
//        </div>
//    );
//};
import { useState } from 'react';
import { calculateCosts } from '../Services/TariffService';
import './TariffList.css'; 

const TariffList = () => {
    const [consumption, setConsumption] = useState('');
    const [tariffs, setTariffs] = useState([]);
    const [error, setError] = useState(null);

    const fetchTariffs = async () => {
        try {
            const data = await calculateCosts(consumption);
            setTariffs(data);
            setError(null); // Clear error if successful
        } catch (err) {
            // Handle error and update state
            if (err.response) {
                // Server responded with a status other than 2xx
                setError(`Error: ${err.response.data.message || 'An error occurred'}`);
            } else if (err.request) {
                // Request was made but no response received
                setError('Error: No response received from server');
            } else {
                // Something else happened in setting up the request
                setError(`Error: ${err.message}`);
            }
        }
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        if (parseFloat(consumption) <= 0) {
            setError('Please enter a positive consumption value');
            return;
        }
        fetchTariffs();
    };

    return (
        <div className="tariff-container">
            <h1>Electricity Tariff Comparison</h1>
            <form onSubmit={handleSubmit}>
                <label>
                    Consumption (kWh/year):
                    <input
                        type="number"
                        value={consumption}
                        onChange={(e) => setConsumption(e.target.value)}
                        required
                    />
                </label>
                <button type="submit">Calculate</button>
            </form>
            {error && <p style={{ color: 'red' }}>{error}</p>}
            <ul className="tariff-list">
                {tariffs.length > 0 ? (
                    tariffs.map((tariff, index) => (
                        <li key={index}>
                            {tariff.tariffName}: &#x20AC;{(tariff.annualCost ?? 0).toFixed(2)}
                        </li>
                    ))
                ) : (
                    <li>No tariffs available</li>
                )}
            </ul>
        </div>
    );
};


export default TariffList;