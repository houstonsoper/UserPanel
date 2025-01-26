"use client"

import Link from "next/link";
import {useRef, useState} from "react";
import {validateForgotPasswordForm, validateRegistrationForm} from "@/services/userService";

export default function ForgotPasswordPage() {
    const formRef = useRef<HTMLFormElement | null>(null);
    const [errors, setErrors] = useState<Error[] | null>(null);
    
    const handleFormSubmit = (e : React.FormEvent) => {
        e.preventDefault();
        setErrors(null); //Clear errors
        
        if (formRef.current) {
            const formData = new FormData(formRef.current);
            
            //Check form data is valid and return any errors
            const formErrors: Error[] | null = validateForgotPasswordForm(formData);
            if (formErrors) {
                setErrors(formErrors);
                return;
            }
            
            //Send password reset if validation passes
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
                        <h1 className="text-2xl ">Reset password</h1>
                        <p className="text-gray-600">Please enter your email to reset your password</p>
                    </div>
                    <div>
                        {errors ? (
                            errors.map(e => (
                                <p className="text-red-500" key={e.name}> {e.message}</p>
                            ))
                        ) : null}
                    </div>
                    <div>
                    </div>
                    <div className="py-1">
                        <label htmlFor="email">Email address</label>
                        <input className="w-full bg-gray-100" type="email" name="email"/>
                    </div>
                    <div className="flex justify-center pt-4">
                        <button type="submit" className="bg-blue-500 text-white font-semibold p-2 w-full">
                            Reset password
                        </button>
                    </div>
                    <div className="flex justify-center mt-3">
                        <Link href="/login" className="w-full">
                            <button className="bg-gray-700 text-white font-semibold p-2 w-full">
                                Cancel
                            </button>
                        </Link>
                    </div>
                </form>
            </div>
        </div>
    </div>
    )
}