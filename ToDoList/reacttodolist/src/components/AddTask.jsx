import React, { useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { addTask } from '../redux/actions';

const AddTask = () => {
    const [description, setDescription] = useState('');
    const [deadLine, setDeadline] = useState('');
    const [categoryId, setCategoryId] = useState('');
    const dispatch = useDispatch();
    const categories = useSelector(state => state.categories);

    const handleSubmit = (event) => {
        event.preventDefault();
        const newTask = {
            description,
            deadLine,
            categoryId: categoryId || null,
            completed: false,
        };
        if (description != '')
            dispatch(addTask(newTask));
        setDescription('');
        setDeadline('');
        setCategoryId('');
    };

    return (
        <form onSubmit={handleSubmit} className="mb-3">
            <div className="row">
                <div className="form-group">
                    <label>Description:</label>
                    <input value={description} onChange={(e) => setDescription(e.target.value)} className="form-control" />
                </div>
                <div className="form-group">
                    <label>Deadline:</label>
                    <input value={deadLine} onChange={(e) => setDeadline(e.target.value)} type="date" className="form-control" />
                </div>
                <div className="form-group">
                    <label>Category:</label>
                    <select value={categoryId} onChange={(e) => setCategoryId(e.target.value)} className="form-select">
                        <option value=""></option>
                        {categories.map((category) => (
                            <option key={category.id} value={category.id}>{category.name}</option>
                        ))}
                    </select>
                </div>
            </div>
            <button type="submit" className="btn btn-primary mt-3">Add task</button>
        </form>
    );
};

export default AddTask;
