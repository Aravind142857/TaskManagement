"use client"
import React, {useState} from "react";
import { loginUser } from "../lib/auth";

const Login: React.FC<{ setUserId?:React.Dispatch<React.SetStateAction<string>>}> = ({setUserId}) => {
    const [email, setEmail] = useState<string>("");
    const [password, setPassword] = useState<string>("");
    const [error, setError] = useState<string | null>(null);

    const handleLogin = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            const [token, userId] = await loginUser(email, password);
            setUserId?setUserId(userId):null;
            setError(null);
            console.log(userId);
            alert("Login successful");
            window.location.reload();
        } catch (err) {
            setError("Login failed. Please check your credentials.");
            alert(err);
            setError(String(err));
        };
    };
    return (
        <form onSubmit={handleLogin} className="text-black">
            <input className="bg-transparent outline outline-2 text-black outline-primary pl-2 dark:text-white" type="email" placeholder="Email" value={email} onChange={(e) => setEmail(e.target.value)} required />
            <input className="bg-transparent outline outline-2 text-black outline-primary pl-2 dark:text-white" type="password" placeholder="Password" value={password} onChange={(e) => setPassword(e.target.value)} required />
            <button className="btn btn-outline btn-primary rounded-md" type="submit">Log In</button>
            {error && <p>{error}</p>}
        </form>
    );
};

export default Login;