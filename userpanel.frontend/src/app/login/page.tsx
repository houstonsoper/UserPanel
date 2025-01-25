﻿"use client"

import {RefObject, useRef, useState} from "react";
import Link from "next/link";
import {login, validateLoginForm, validateRegistrationForm} from "@/services/userService";
import User from "@/interfaces/user";

export default function LoginPage() {
    const formRef : RefObject<HTMLFormElement | null>= useRef<HTMLFormElement>(null);
    const [areDetailsInvalid, setAreDetailsInvalid] = useState<boolean>(false);
    const [errors, setErrors] = useState<Error[]>([]);
   
    const handleFormSubmit = async (e : React.FormEvent) => {
        e.preventDefault();
        setAreDetailsInvalid(false); //Clear current errors 
        
        if (formRef.current) {
            const formData = new FormData(formRef.current);

            //Check form data is valid and return any errors
            const formErrors: Error[] | null = validateLoginForm(formData);
            if (formErrors) {
                setErrors(formErrors);
                setAreDetailsInvalid(true);
                return;
            }
            
            //Attempt to log the user in 
            try{
                const user : User | null = await login(formData);
                console.log(user);
            } catch (error){
                if (error instanceof Error) {
                    setErrors([{name: "UserLoginError", message: error.message}]);
                    setAreDetailsInvalid(true)
                }
            }
        }
    }
    return (
        <div className="container m-auto">
            <div className="flex h-screen">
                <div className="m-auto">
                    <form
                        className="border border-gray-300 p-6 rounded-2xl m-auto"
                        onSubmit={handleFormSubmit}
                        ref={formRef}
                    >
                        <div className="pb-3">
                            <h1 className="text-2xl ">Login</h1>
                            <p className="text-gray-600">Please enter your details</p>
                        </div>
                        <div>
                            {areDetailsInvalid ? (
                                errors.map(e => (
                                    <p className="text-red-500" key={e.name}> {e.message}</p>
                                ))
                            ) : null}
                        </div>
                        <div className="py-1">
                            <label htmlFor="email">Email address</label>
                            <input className="w-full bg-gray-100" type="email" name="email"/>
                        </div>
                        <div className="py-1">
                            <label htmlFor="password">Password</label>
                            <input className="w-full bg-gray-100" type="password" name="password"/>
                        </div>
                        <div className="flex justify-center pt-4">
                            <button type="submit" className="bg-blue-500 text-white font-semibold p-2 w-full">
                                Login
                            </button>
                        </div>
                        <div className="py-3 text-center">
                            <p>Don't have an account?</p>
                        </div>
                        <div className="flex justify-center">
                            <Link href="/register" className="w-full">
                                <button className="bg-blue-300 text-white font-semibold p-2 w-full">
                                    Register
                                </button>
                            </Link>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    )
}