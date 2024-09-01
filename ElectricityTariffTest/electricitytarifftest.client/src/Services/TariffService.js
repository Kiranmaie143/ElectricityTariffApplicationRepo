//import axios from 'axios';

//// Define the base URL for the API
//const API_BASE_URL = 'http://localhost:5062'; // Update with your API base URL

//// Create an Axios instance with default settings
//const api = axios.create({
//    baseURL: API_BASE_URL,
//    headers: {
//        'Content-Type': 'application/json',
//    },
//});

//// Define the service functions
//const TariffService = {
//    async getTariffs() {
//        try {
//            const response = await api.get('/api/tariffs');
//            return response.data;
//        } catch (error) {
//            console.error('Error fetching tariffs:', error);
//            throw error; // Re-throw the error to be handled by the caller
//        }
//    }
//};
import axios from 'axios';

const API_URL = 'http://localhost:5062/api/tariffs';

export const calculateCosts = async (consumption) => {
    try {
        const response = await axios.post(API_URL, { consumption: parseFloat(consumption) }, {
            headers: {
                'Content-Type': 'application/json'
            }
        });
        return response.data;
    } catch (error) {
        console.error('Error fetching tariffs:', error);
        throw error;
    }
};

