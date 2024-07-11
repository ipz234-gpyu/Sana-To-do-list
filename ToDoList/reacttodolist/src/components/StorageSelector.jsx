import React, { useEffect } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { changeStorageType, getTasks, getCategories } from '../redux/actions';

const StorageSelector = () => {
    const storageTypeSelected = useSelector(state => state.activeStorage);
    const dispatch = useDispatch();

    const handleChange = (event) => {
        dispatch(changeStorageType(event.target.value));
        dispatch(getTasks());
        dispatch(getCategories());
    };

    return (
        <div className="d-flex align-items-center gap-5">
            <div>
                <h3>Current storage: <span style={{ fontWeight: 'normal' }}>{storageTypeSelected}</span></h3>
            </div>
            <div className="d-flex align-items-center gap-5">
                <h2>Repository change:</h2>
                <form>
                    <select name="storageType" className="form-select" onChange={handleChange}>
                        <option hidden>Select a repository:</option>
                        <option value="SQL">SQL</option>
                        <option value="XML">XML</option>
                    </select>
                </form>
            </div>
        </div>
    );
};

export default StorageSelector;