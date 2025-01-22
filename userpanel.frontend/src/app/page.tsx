"use client"

import Image from "next/image";
import {RefObject, useRef, useState} from "react";

export default function Home() {
    const formRef: RefObject<HTMLFormElement | null> = useRef<HTMLFormElement>(null);
    const [isDetailsInvalid, setIsDetailsInvalid] = useState<boolean>(false); 
    const [errorMessage, setErrorMessage] = useState<string>("");
    const handleFormSubmit = (e: React.FormEvent ) => {
        e.preventDefault();
        setIsDetailsInvalid(false); //Clear current errors 
        
        if (formRef.current) {
            const formData = new FormData(formRef.current);

            //Check that passwords match, if not display an error
            const password1 : FormDataEntryValue | null = formData.get("password1");
            const password2 : FormDataEntryValue | null = formData.get("password2");
            if (password1 !== password2) {
                setErrorMessage("Passwords must match");
                setIsDetailsInvalid(true);
                return;
            }
            
            //Create a newUser object from the form data 
            const newUser = {
                name: formData.get("username"),
                email: formData.get("email"),
                password: formData.get("password1")
            }
        }
    }
    return (
        <div className="container">
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
                            <p className="text-red-500">{errorMessage}</p>
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
                        <label htmlFor="password1">Password</label>
                        <input className="w-full bg-gray-100" type="password" name="password1" required/>
                    </div>
                    <div className="py-1">
                        <label htmlFor="password2">Repeat password</label>
                        <input className="w-full bg-gray-100" type="password" name="password2" required/>
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
