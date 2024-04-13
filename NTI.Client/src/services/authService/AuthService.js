import baseHttpRequest from "../baseService/baseHttpRequest";

export class AuthService {

    signup = async (inputModel) => {
        const result = await baseHttpRequest.post("Auth/signup", inputModel);
        return result;
    }

    login = async (inputModel) => {
        const result= await baseHttpRequest.post("Auth/login", inputModel);
        if(result.isSuccessfulWithNoErrors){
            localStorage.setItem('token', result.payload.token);
        }
        console.log(localStorage.getItem("token"))
        return result;
    }

    logout = async () => {
        localStorage.removeItem('token');
    }
}