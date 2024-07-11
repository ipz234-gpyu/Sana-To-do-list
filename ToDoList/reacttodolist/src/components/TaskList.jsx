import React, { useEffect } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { getCategories, getTasks, removeTask, taskCompleted } from '../redux/actions';


const TaskList = () => {
    const tasks = useSelector(state => state.tasks);
    const categories = useSelector(state => state.categories);
    const dispatch = useDispatch();

    useEffect(() => {
        dispatch(getTasks());
        dispatch(getCategories());
    }, [dispatch]);

    const handleTaskCompleted = (id) => {
        dispatch(taskCompleted(id));
    };

    const handleRemoveTask = (taskId) => {
        dispatch(removeTask(taskId));
    };


    return (
        <table className="table">
            <thead>
                <tr className="text-center">
                    <th>Completed</th>
                    <th>Description</th>
                    <th>DeadLine</th>
                    <th>Category</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                {tasks?.map((task) => {
                    return (
                        <tr
                            key={task.id}
                            className={`text-center align-middle ${task.completed ? 'completed-task' : ''}`}
                        >
                            <td>
                                <input
                                    type="checkbox"
                                    checked={task.completed}
                                    onChange={() => handleTaskCompleted(task.id)}
                                    className="form-check-input"
                                />
                            </td>
                            <td>{task.description}</td>
                            <td>{task.deadLine}</td>
                            <td>{categories?.find((category) => category.id == task.categoryId)?.name}</td>
                            <td>
                                <button onClick={() => handleRemoveTask(task.id)} className="btn btn-danger">Delete</button>
                            </td>
                        </tr>
                    );
                })}
            </tbody>
        </table>
    );
};

export default TaskList;