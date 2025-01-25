import RegistrationDetails from "@/interfaces/registrationDetails";
import User from "@/interfaces/user";
import LoginDetails from "@/interfaces/loginDetails";

export function extractFormDate(form: FormData): RegistrationDetails {
    const forename: string | undefined = form.get('forename')?.toString();
    const surname: string | undefined = form.get('surname')?.toString();
    const email: string | undefined = form.get('email')?.toString();
    const password: string | undefined = form.get('password')?.toString();
    const confirmPassword: string | undefined = form.get('confirmPassword')?.toString();

    return {forename, surname, email, password, confirmPassword} as RegistrationDetails
}

export function validateRegistrationForm(formData: FormData): Error[] | null {
    const details: RegistrationDetails = extractFormDate(formData);
    let errors: Error[] = [];

    //Check if forename is supplied and is between 1 and 20 characters
    if (!details.forename) {
        errors.push({name: "InvalidForename", message: "Please enter your first name"});
    } else if (details.forename.length < 5 || details.forename.length > 20) {
        errors.push({name: "InvalidForenameLength", message: "First name must be between 1 and 20 characters"});
    }

    //Check if surname is supplied as is between 1 and 20 characters
    if (!details.surname) {
        errors.push({name: "InvalidSurname", message: "Please enter your surname"});
    } else if (details.surname.length < 5 || details.surname.length > 20) {
        errors.push({name: "InvalidSurnameLength", message: "Surname must be between 1 and 20 characters"});
    }

    //Check if email is supplied and is between 5 and 254 characters
    if (!details.email) {
        errors.push({name: "InvalidEmail", message: "Please enter your email"});
    } else if (details.email.length < 5 || details.email.length > 254) {
        errors.push({name: "InvalidEmailLength", message: "Email must be between 5 and 254 characters"});
    }
    //Check that passwords match 
    //and that the password is between 5 and 15 characters
    if (!details.password && !details.confirmPassword) {
        errors.push({name: "InvalidPassword", message: "Please enter a password"});
    }
        else if (details.password !== details.confirmPassword) {
            errors.push({name: "PasswordMismatch", message: "Passwords do not match"});
        }
        else if (details.password.length < 5 || details.password.length > 15) {
            errors.push({name: "InvalidPasswordLength", message: "Password must be between 5 and 15 characters"});
        }
    
    //Return any errors 
    if (errors.length > 0) {
        return errors;
    }

    return null
}

export function validateLoginForm(formData: FormData): Error[] | null {
    const details: RegistrationDetails = extractFormDate(formData);
    let errors: Error[] = [];

    //Check if an email has been entered
    if (!details.email) {
        errors.push({name: "InvalidEmail", message: "Please enter your email"});
    }

    //Check if a password has been entered
    if (!details.password) {
        errors.push({name: "InvalidPassword", message: "Please enter your password"});
    }

    //Return any errors 
    if (errors.length > 0) {
        return errors;
    }

    return null
}

export async function createUser(formData: FormData): Promise<User | null> {
    const registrationDetails: RegistrationDetails = extractFormDate(formData);

    try {
        const url: string = "https://localhost:44378/Register"

        const response: Response = await fetch(url, {
            method: "POST",
            body: JSON.stringify(registrationDetails),
            headers: {"Content-Type": "application/json"},
        });

        if (!response.ok) {
            const errorData = await response.json();
            throw new Error(errorData.message);
        }
        return response.json();
    } catch (error) {
        if (error instanceof Error) {
            console.error(error);
            throw new Error(error.message);
        }
        return null;
    }
}

export async function login(formData: FormData): Promise<User | null> {
    const loginDetails: LoginDetails = extractFormDate(formData);

    try {
        const url: string = "https://localhost:44378/Login"

        const response: Response = await fetch(url, {
            method: "POST",
            body: JSON.stringify(loginDetails),
            headers: {"Content-Type": "application/json"},
        });

        if (!response.ok) {
            const errorData = await response.json();
            throw new Error(errorData.message);
        }
        return response.json();
    } catch (error) {
        if (error instanceof Error) {
            console.error(error);
            throw new Error(error.message);
        }
        return null;
    }
}