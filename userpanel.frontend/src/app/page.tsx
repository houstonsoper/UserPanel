"use client"

import Image from "next/image";
import {RefObject, useRef, useState} from "react";
import {createUser, validateRegistrationForm} from "@/services/userService";
import User from "@/interfaces/user";

export default function Home() {
    const formRef: RefObject<HTMLFormElement | null> = useRef<HTMLFormElement>(null);
    const [isDetailsInvalid, setIsDetailsInvalid] = useState<boolean>(false); 
    const [errors, setErrors] = useState<Error[]>([]);
    
    const handleFormSubmit = (e: React.FormEvent ) => {
        e.preventDefault();
        setIsDetailsInvalid(false); //Clear current errors 
        
        if (formRef.current) {
            const formData = new FormData(formRef.current);
            
            //Check form data is valid and return any errors
            const formErrors : Error[] | null = validateRegistrationForm (formData);
            if (formErrors) {
                setErrors(formErrors);
                setIsDetailsInvalid(true);
                return;
            }
            
            //Create user
            const user : User = createUser(formData);
            console.log(user);
        }
    }
    return (
        <div className="container m-auto">
            <div className="flex justify-center h-screen">
                <form
                    className="border border-gray-300 p-6 rounded-2xl m-auto"
                    onSubmit={handleFormSubmit}
                    ref={formRef}
                >
                    <div className="pb-3">
                        <h1 className="text-2xl ">Create an account</h1>
                        <p className="text-gray-600">Please enter your details</p>
                    </div>
                    <div>
                        {isDetailsInvalid ? (
                            errors.map(e => (
                                <p className="text-red-500" key={e.name}>{e.message}</p>
                            ))
                        ) : null}
                    </div>
                    <div className="py-1">
                        <label htmlFor="username">Username</label>
                        <input className="w-full bg-gray-100" type="text" name="username" required/>
                    </div>
                    <div className="py-1">
                        <label htmlFor="email">Email</label>
                        <input className="w-full bg-gray-100" type="email" name="email" required/>
                    </div>
                    <div className="py-1">
                        <label htmlFor="password">Password</label>
                        <input className="w-full bg-gray-100" type="password" name="password" required/>
                    </div>
                    <div className="py-1">
                        <label htmlFor="confirmPassword">Confirm password</label>
                        <input className="w-full bg-gray-100" type="password" name="confirmPassword" required/>
                    </div>
                    <div className="flex justify-center pt-4">
                        <button type="submit" className="bg-blue-500 text-white font-semibold p-2 w-full">
                            Register
                        </button>
                    </div>
                    <div className="py-3 text-center">
                        <p>Already have an account?</p>
                    </div>
                    <div className="flex justify-center">
                        <button className="bg-blue-300 text-white font-semibold p-2 w-full">
                            Login
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
}
