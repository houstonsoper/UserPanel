"use client"

import Link from "next/link";
import { useEffect, useState } from "react";
import User from "@/interfaces/user";
import {getUser, userLogout} from "@/services/userService";
import { useRouter } from "next/navigation";

export default function HomePage() {
    const [user, setUser] = useState(null);
    const router = useRouter();
    
    //Fetch the details of the user if they are logged in
    useEffect(() => {
        const fetchUser = async () => {
            const user = await getUser();
            
            if(!user) {
                router.push("/login");
            }
            setUser(user);
        }
        fetchUser();
    }, []);
    
    const handleLogout = async () => {
        await userLogout();
        router.push("/login");
    }
    
    return (
        <div className="container m-auto">
            <div className="flex h-screen">
                <div className="m-auto flex flex-col w-3/4 max-w-96 gap-2">
                    {!user ? (
                        <div>
                            <Link href="/login">
                                <button className="bg-blue-300 text-white font-semibold p-2 w-full">
                                    Login
                                </button>
                            </Link>
                            <Link href="/register">
                                <button className="bg-blue-500 text-white font-semibold p-2 w-full">
                                    Register
                                </button>
                            </Link>
                        </div>
                    ) : (
                        <div>
                        <h1 className="text-center text-2xl">You are logged in</h1>
                            <button onClick={handleLogout} className="bg-blue-300 text-white font-semibold p-2 my-3 w-full">
                                Logout
                            </button>
                        </div>
                    )}
                </div>
            </div>
        </div>
    )
}