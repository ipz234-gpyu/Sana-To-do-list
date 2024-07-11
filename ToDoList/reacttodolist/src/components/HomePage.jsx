import React from 'react';
import TaskList from './TaskList';
import StorageSelector from './StorageSelector';
import AddTask from './AddTask';
import AddCategory from './AddCategory';

const HomePage = () => {
    return (
        <main>
            <article className="mt-2">
                <h1 className="text-center">To Do List</h1>
                <div className="mb-3 d-flex flex-column flex-xl-row align-items-center justify-content-around">
                    <StorageSelector/>
                </div>
            </article>
            <article className="d-flex justify-content-around flex-column flex-sm-row">
                <section className="mt-2 col-sm-5">
                    <AddTask />
                </section>
                <section className="mt-2 col-sm-5">
                    <AddCategory />
                </section>
            </article>
            <article className="mt-2">
                <h2>Task List</h2>
                <TaskList />
            </article>
        </main>
    );
};

export default HomePage;