"use client"
function TestTaskList(){
    const MYTASKS = [
        {"id":0,"title":"first", "description":"task1", "dueDate": Date.now(),"priority":0,"isCompleted":false},
        {"id":1,"title":"second", "description":"task2", "dueDate": Date.now(),"priority":1,"isCompleted":false},
        {"id":2,"title":"third", "description":"task3", "dueDate": Date.now(),"priority":2,"isCompleted":false},
        {"id":3,"title":"fourth", "description":"task4", "dueDate": Date.now(),"priority":3,"isCompleted":false},
        {"id":4,"title":"fifth", "description":"task5", "dueDate": Date.now(),"priority":4,"isCompleted":false},
    ]
    return (
        <div>
            {MYTASKS && MYTASKS.map((task) => (
                <div className="card outline outline-green-400 text-slate-200 w-96 my-8" key={task['id']}>
                    <div className="card-body">
                    {/* <script>{console.log(task)}</script> */}
                        <h1 className="card-title bg-red-400">{task.title} <div className="badge badge-secondary">{task.priority}</div></h1>
                        <p>{task.description}</p>
                    </div>
                </div>
            ))}
        </div>
    )
}
export default TestTaskList;