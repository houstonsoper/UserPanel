import RegistrationDetails from "@/interfaces/registrationDetails";
import User from "@/interfaces/user";

export function extractFormDate (form : FormData) : RegistrationDetails {
    const username : string | undefined = form.get('username')?.toString();
    const email : string | undefined = form.get('email')?.toString();
    const password : string | undefined = form.get('password')?.toString();
    const confirmPassword : string | undefined = form.get('confirmPassword')?.toString();
    
    return {username, email, password, confirmPassword} as RegistrationDetails
}

export function validateRegistrationForm(form : FormData) : Error[] | null {
    const details : RegistrationDetails = extractFormDate(form);
    let errors : Error[] = [];

    //Check if username is between 5 and 15 characters
    if (details.username){
        if(details.username.length <= 5 || details.username.length > 15){
            errors.push({name: "InvalidUsernameLength", message: "Username must be between 5 and 15 characters"});
        }
    }
    //Check if email is between 5 and 254 characters
    if (details.email){
        if(details.email.length <= 5 || details.email.length > 254){
            errors.push({name: "InvalidEmailLength", message: "Email must be between 5 and 254 characters"});
        }
    }
    //Check that passwords match 
    //and that the password is between 5 and 15 characters
    if (details.password && details.confirmPassword){
        if(details.password !== details.confirmPassword){
            errors.push({name: "PasswordMismatch", message: "Passwords do not match"});
        }
        if(details.password.length <= 5  || details.password.length > 15){
            errors.push({name: "InvalidPasswordLength", message: "Password must be between 5 and 15 characters"});
        }
    }
    
    //Return any errors 
    if (errors.length > 0){
        return errors;
    }
    
    return null
}

export function createUser(form: FormData) : User {
    const details : RegistrationDetails = extractFormDate(form);
    
    const user : User = {
        username : details.username,
        email : details.email,
        password: details.password,
    }
    return user;
}