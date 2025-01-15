"use client";
import TaskList from "../components/TaskList";
import React, { useState } from "react";
import { useAuth } from "../hooks/useAuth";
import { logout } from "../lib/auth";
import Login from "../components/Login";


export default function TasksPage() {
  const isAuthenticated = useAuth();
  const [userId, setUserId] = useState<string>(()=>localStorage.getItem("userId")||'');
  console.log(userId);
  return (
    <div className="min-h-screen flex flex-col items-center justify-center bg-gray-100 dark:bg-slate-900 text-gray-800 ">
      <h1 className="text-3xl font-bold mb-6 text-indigo-600">Welcome to Your Productivity Dashboard</h1>

      {!isAuthenticated ? (
        <div className="text-center text-[#8CFFDA]">
          <p className="mb-4">Please log in to access your tasks and manage your productivity.</p>
          {/* <a href="/login" className="btn btn-primary">Log In</a> */}
          <Login setUserId={setUserId}/>
        </div>
      ) : (
        <div className="w-full max-w-2xl">
          <div className="flex justify-between items-center mb-4">
            <h2 className="text-2xl font-semibold text-emerald-600">Your Tasks</h2>
            <button onClick={logout} className="btn btn-outline btn-sm text-red-600 btn-error">Logout</button>
          </div>
          <TaskList userId={userId}/>
        </div>
      )}
    </div>
  );
}