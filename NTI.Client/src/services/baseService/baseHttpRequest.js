import axios from "axios"


const api = axios.create({
    baseURL: import.meta.env.VITE_REACT_API_URL
})


const get = async (url, params = {}) => {
    try {
        const response = await api.get(url, { params })
        return response.data
    }
    catch (error) {
        console.error(error)
        throw error
    }
}



const post = async (url, data) => {
    try {
        const response = await api.post(url, data)
        return response.data
    }
    catch (error) {
        console.error(`POST request to ${url} failed:`, error);
        throw error
    }
}

const put = async (url, data = {}) => {
    try {
        const response = await api.put(url, data);
        return response.data;
    } catch (error) {
        console.error(`PUT request to ${url} failed:`, error);
        throw error;
    }
};


const remove = async (url) => {
    try {
        const response = await api.delete(url);
        return response.data;
    } catch (error) {
        console.error(`DELETE request to ${url} failed:`, error);
        throw error;
    }
};

export default {
    get,
    post,
    put,
    delete: remove,
  };
  