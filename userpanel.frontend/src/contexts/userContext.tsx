"use client"

import {createContext, ReactNode, useContext, useEffect, useMemo, useState} from "react";
import User from "@/interfaces/user";
import {
    getUser,
    userLogout,
} from "@/services/userService";
import {get} from "@jridgewell/set-array";

interface UserContextType {
    user: User | null,
    setUser: (user: User | null) => void,
    logout: () => Promise<void>,
    loading: boolean,
}

const UserContext = createContext<UserContextType | undefined>(undefined);

export const UserProvider = ({children}: { children: ReactNode }) => {
    const [user, setUser] = useState<User | null>(null);
    const [loading, setLoading] = useState(true);

    //Fetch user data on mount
    useEffect(() => {
        const fetchUser = async () => {
            try {
                const fetchedUser : User | null = await getUser();
                setUser(fetchedUser);
            } catch (error) {
                console.error("Failed to fetch user", error);
            } finally {
                setLoading(false); 
            }
        };
        fetchUser();
    }, []);
    
    //Logout function, clears user state and calls the logout service
    const logout = async () => {
        try {
            setLoading(true);
            setUser(null);
            await userLogout();
        } catch (error) {
            console.error("Logout failed", error);
        } finally {
            setLoading(false);
        }
    }

    //Memorize context value to prevent re-renders
    const value: UserContextType = useMemo((): UserContextType => ({
        user,
        setUser,
        logout,
        loading,
    }), [user]);

    return (<UserContext.Provider value={value}>{children}</UserContext.Provider>);
}

export const useUser = (): UserContextType => {
    const context: UserContextType | undefined = useContext(UserContext);

    if (!context) {
        throw new Error("useUser must be used within the context");
    }

    return context;
}

