const initialState = {
    activeStorage: localStorage.getItem('activeStorage') || 'SQL',
    tasks: [],
    categories: [],
    errors: [],
};

const rootReducer = (state = initialState, action) => {
    switch (action.type) {
        case 'CHANGE_STORAGE_TYPE':
            localStorage.setItem('activeStorage', action.payload);
            return {
                ...state,
                activeStorage: action.payload,
            };
        case 'GET_TASKS_SUCCESS':
            const sortedTasks = action.payload.sort((a, b) => {
                if (a.completed === b.completed) {
                    return b.id - a.id;
                }
                return a.completed - b.completed;
            });
            return {
                ...state,
                tasks: sortedTasks,
            };
        case 'GET_CATEGORIES_SUCCESS':
            return {
                ...state,
                    categories: action.payload,
            };
        case 'ADD_TASK_SUCCESS':
            const addTasks = [...state.tasks, action.payload];
            addTasks.sort((a, b) => {
                if (a.completed === b.completed) {
                    return b.id - a.id;
                }
                return a.completed - b.completed;
            });
            return {
                ...state,
                tasks: addTasks,
            };
        case 'ADD_CATEGORY_SUCCESS':
            return {
                ...state,
                categories: [...state.categories, action.payload],
            };
        case 'TASK_COMLPETED_SUCCESS':
            return {
                ...state,
                tasks: state.tasks.map(task =>
                    task.id == action.payload.taskId ? { ...task, completed: action.payload.completed } : task
                ).sort((a, b) => {
                    if (a.completed === b.completed) {
                        return b.id - a.id;
                    }
                    return a.completed - b.completed;
                }),
            };
        case 'REMOVE_TASK_SUCCESS':
            const updatedTasks = state.tasks.filter(task => task.id != action.payload);
            return {
                ...state,
                tasks: updatedTasks,
            };
        case 'FAILED':
            return {
                ...state,
                errors: action.payload,
            };
        default:
            return state;
    }
};

export default rootReducer;