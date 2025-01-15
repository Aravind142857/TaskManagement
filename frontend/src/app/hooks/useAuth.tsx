import { useState, useEffect } from "react";

export function useAuth(): boolean {
    const [isAuthenticated, setIsAuthenticated] = useState<boolean>(false);
    useEffect(() => {
        const token = localStorage.getItem("authToken");
        setIsAuthenticated(!!token);
    }, []);
    return isAuthenticated;
}