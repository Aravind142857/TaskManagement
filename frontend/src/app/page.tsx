// "use client"
import TestTaskList from "./components/TestTaskList"
export default function Home() {
  return (
    <div className="min-w-screen min-h-screen bg-black ">
      <main className="px-[10%] bg-gray-900"> 
        {/* <TestTaskList /> */}
        <h1>Visit <a href="localhost:3000/tasks">Tasks</a></h1>
      </main>
    </div>
  )
}
