"use client"

import Link from "next/link";
import { useEffect, useState } from "react";
import { getUser, userLogout } from "@/services/userService";
import { useRouter } from "next/navigation";
import { useUser } from "@/contexts/userContext";

export default function HomePage() {
    const { user, logout, loading }  = useUser();
    const router = useRouter();
    
    //Handler to logout user when the logout button is pressed.
    const handleLogout = async () => {
        await logout();
        
        if(!loading)
        router.push("/login");
    }
    
    return (
        <div className="container m-auto">
            <div className="flex h-screen">
                <div className="m-auto flex flex-col w-3/4 max-w-96 gap-2">
                    {!user && !loading ? (
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