import axios from 'axios';


export const axiosInstance = axios.create({
    baseURL: "https://localhost:44378/api/"
});

