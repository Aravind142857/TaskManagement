import React, {useState} from "react";
import { registerUser } from "../lib/auth";

const Register: React.FC = () => {
    const [username, setUsername] = useState<string>("");
    const [email, setEmail] = useState<string>("");
    const [password, setPassword] = useState<string>("");
    const [error, setError] = useState<string | null>(null);

    const handleRegister = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            await registerUser(username, email, password);
            alert("Registration successful");
        } catch (err) {
            setError("Registration failed. Please try again.");
        }
    };
    return (
        <form onSubmit={handleRegister}>
            <input className="" type="text" placeholder="Username" value={username} onChange={(e)=> setUsername(e.target.value)} required/>
            <input className="" type="email" placeholder="Email" value={email} onChange={(e)=> setEmail(e.target.value)} required />
            <input className="" type="password" placeholder="Password" value={password} onChange={(e)=> setPassword(e.target.value)} required />
            <button className="btn btn-outline btn-primary" type="submit">Register</button>
            {error && <p>{error}</p>}
        </form>
    );
};
export default Register;