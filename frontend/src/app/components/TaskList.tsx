"use client";
import {gql, useQuery, useMutation} from '@apollo/client';
import { AiOutlinePlus, AiOutlineEdit, AiOutlineDelete } from "react-icons/ai";
import React, {useState} from "react";
import { useAuth } from '../hooks/useAuth';
import { GET_TASKS, CREATE_TASK, DELETE_TASK, UPDATE_TASK, TOGGLE_TASK_COMPLETION } from '../lib/crud';

const TaskList: React.FC<{ userId?: string }> = ({userId}) => {
    const isAuthenticated = useAuth();
    console.log(userId);
    const { loading, error, data, refetch } = useQuery(GET_TASKS, {variables: {userId: userId}, skip: !userId});
    const [createTask, {}] = useMutation(CREATE_TASK);
    const [deleteTask] = useMutation(DELETE_TASK);
    const [updateTask] = useMutation(UPDATE_TASK);
    const [toggleTaskCompletion] = useMutation(TOGGLE_TASK_COMPLETION);
    const [isModalOpen, setIsModalOpen] = useState(isAuthenticated?false:null);
    const [isEditModalOpen, setIsEditModalOpen] = useState(isAuthenticated?false:null);
    const [selectedTask, setSelectedTask] = useState(null);
    const [taskTitle, setTaskTitle] = useState("");
    const [taskDescription, setTaskDescription] = useState("");
    const [taskDueDate, setTaskDueDate] = useState("");
    const [taskPriority, setTaskPriority] = useState(1);
  
    const openEditModal = (task: any) => {
      setSelectedTask(task);
      setTaskTitle(task.title);
      setTaskDescription(task.description);
      setTaskDueDate(task.dueDate);
      setTaskPriority(task.priority);
      setIsEditModalOpen(true);
    }
    const handleToggleCompletion = async (task: any) => {
      try {
        await toggleTaskCompletion({
          variables: {
            id: task.id,
            isCompleted: !task.isCompleted,
          },
        });
        refetch();
      } catch (error) {
        console.error("Error toggling task completion:", error);
      }
    };
    const handleDeleteTask = async (taskId: any) => {
      console.log(taskId);
      try {
        await deleteTask({ variables: { id: taskId } });
        console.log("deleted");
        refetch();
      } catch (error) {
        console.error("Error deleting task:", error);
      }
    };
    const handleAddTask = async (e: any) => {
      e.preventDefault();
      try {
        await createTask({
          variables: { 
            input: {
            title: taskTitle,
            description: taskDescription,
            dueDate: taskDueDate,
            priority: taskPriority,
            isCompleted: false,
            },
            userId: userId,
          },
        });
        refetch();
        setIsModalOpen(false);
        setTaskTitle("");
        setTaskDescription("");
        setTaskDueDate("");
        setTaskPriority(1);
      } catch(error) {
        console.error("error adding task:", error);
      }
    };
    function CreateTask() {
      return (
        <div className="modal modal-open">
          <div className="modal-box text-white">
            <h3 className="font-bold text-lg text-amber-600">Add Task</h3>
            <form onSubmit={handleAddTask}>
              <input 
                type="text"
                placeholder="Title"
                className="input input-bordered w-full mb-2"
                required
                onChange={(e)=> setTaskTitle(e.target.value)}
              />
              <input
                type="text"
                placeholder="Description"
                className="input input-bordered w-full mb-2"
                required
                onChange={(e)=> setTaskDescription(e.target.value)}
              />
              <input
                type="date"
                className="input input-bordered w-full mb-2"
                required
                onChange={(e)=> setTaskDueDate(e.target.value)}
              />
              <select
                className="select select-bordered w-full mb-4"
                onChange={(e)=> setTaskPriority(parseInt(e.target.value))}
              >
                <option value="1">Low</option>
                <option value="2">Medium</option>
                <option value="3">High</option>
              </select>
              <div className="modal-action">
                <button type="submit" className="btn btn-primary">
                  Save
                </button>
                <button onClick={() => setIsModalOpen(false)} className="btn">
                  Cancel
                </button>
              </div>
            </form>
          </div>
        </div>
      )
    };
    const handleEditTask = async (e: any) => {
      e.preventDefault();
      try {
        
        await updateTask({
          variables: {
            // @ts-ignore
            id: selectedTask.id,
            updatedTask: {
              title: taskTitle,
              description: taskDescription,
              dueDate: taskDueDate,
              // @ts-ignore
              priority: parseInt(taskPriority),
              // @ts-ignore
              isCompleted: selectedTask.isCompleted,
            },
          },
        });
        refetch();
        setIsEditModalOpen(false);
      } catch(error) {
        console.error("Error updating task:", error);
      }
    };


    if (loading) return <p>Loading ...</p>;
    if (error) return <p>Error: {error.message}</p>;
    if (!isAuthenticated) {
      return <p>You need to login to view your tasks.</p>;
    }
    if (!data) return <p>No tasks found</p>;
    return (
      <div className="bg-black text-black p-4 rounded-lg shadow-lg w-full max-w-lg mx-auto">
      <div className="flex items-center justify-between mb-4">
        <h1 className="text-xl font-bold">Tasks</h1>
        {/* <div className="flex gap-2"> */}
          {/* Add Task Button */}
          <button
            className="btn btn-primary btn-sm flex items-center gap-1"
            onClick={() => setIsModalOpen(true)}
          >
            <AiOutlinePlus /> Add
          </button>
        {/* </div> */}
      </div>

      {/* Task List */}
      <div className="space-y-3">
        {data.tasks?.map((task: any) => (
          <div key={task.id} className="flex items-center justify-between p-2 rounded-md outline outline-indigo-500 brightness-150">
            <div>
              <h3 className={`${task.isCompleted ? "line-through text-gray-400" : "text-indigo-500"}`}>
                {task.title}
              </h3>
              <p className="text-sm text-gray-400">{task.description}</p>
            </div>
            <div className="flex gap-2">
              {/* Toggle Complete */}
              <input
                type="checkbox"
                checked={task.isCompleted}
                onChange={() => handleToggleCompletion(task)}
                className="toggle toggle-primary"
              />
              {/* Modify and Delete Buttons */}
              <button className="btn btn-outline btn-sm" onClick={() => openEditModal(task)}>
                <AiOutlineEdit />
              </button>
              <button className="btn btn-outline btn-sm text-red-600" onClick={() => handleDeleteTask(task.id)}>
                <AiOutlineDelete />
              </button>
            </div>
          </div>
        ))}
      </div>

      {/* Add/Edit Task Modal */}
      {isModalOpen && CreateTask()}
      {isEditModalOpen && (
        <div className="modal modal-open">
          <div className="modal-box text-white">
            <h3 className="font-bold text-lg text-amber-600">Edit Task</h3>
            <form onSubmit={handleEditTask}>
              <input
                type="text"
                placeholder="Title"
                className="input input-bordered w-full mb-2"
                value={taskTitle}
                onChange={(e) => setTaskTitle(e.target.value)}
                required
              />
              <input
                type="text"
                placeholder="Description"
                className="input input-bordered w-full mb-2"
                value={taskDescription}
                onChange={(e) => setTaskDescription(e.target.value)}
                required
              />
              <input
                type="date"
                className="input input-bordered w-full mb-2"
                value={taskDueDate}
                onChange={(e) => setTaskDueDate(e.target.value)}
                required
              />
              <select
                className="select select-bordered w-full mb-4"
                value={taskPriority}
                onChange={(e) => setTaskPriority(parseInt(e.target.value))}
              >
                <option value="1">Low</option>
                <option value="2">Medium</option>
                <option value="3">High</option>
              </select>
              <div className="modal-action">
                <button type="submit" className="btn btn-primary">
                  Save Changes
                </button>
                <button onClick={() => setIsEditModalOpen(false)} className="btn">
                  Cancel
                </button>
              </div>
            </form>
          </div>
        </div>
      )}
    </div>
  );
}
export default TaskList;

//TODO: updateTask: Object { message: "The argument `input` does not exist.", locations: […], extensions: {…} }
// TODO: delete: Mutation: The field `id` does not exist on the type `Mutation`.