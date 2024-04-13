import { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

const useRequireAuthHook = () => {
    const navigate = useNavigate();

    useEffect(() => {
        const token = localStorage.getItem('token');
        if (!token) {
            navigate('/');
        }
    }, [navigate]);
};

export default useRequireAuthHook;