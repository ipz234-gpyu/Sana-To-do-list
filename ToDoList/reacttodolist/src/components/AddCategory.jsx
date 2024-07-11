import React, { useState } from 'react';
import { useDispatch } from 'react-redux';
import { addCategory } from '../redux/actions';

const AddCategory = () => {
    const [name, setName] = useState('');
    const dispatch = useDispatch();

    const handleSubmit = (event) => {
        event.preventDefault();
        const newCategory = {
            name,
        };
        if (name != '')
            dispatch(addCategory(newCategory));
        setName('');
    };

    return (
        <form onSubmit={handleSubmit} className="mb-3">
            <div className="row">
                <div className="form-group">
                    <label>Name Category:</label>
                    <input value={name} onChange={(e) => setName(e.target.value)} className="form-control" />
                </div>
            </div>
            <button type="submit" className="btn btn-primary mt-3">Add category</button>
        </form>
    );
};

export default AddCategory;