export const changeStorageType = (storageType) => ({
    type: 'CHANGE_STORAGE_TYPE',
    payload: storageType,
});

export const getTasks = () => ({
    type: 'GET_TASKS',
    payload: { },
});

export const getTasksSuccess = (tasks) => ({
    type: 'GET_TASKS_SUCCESS',
    payload: tasks,
});

export const getCategories = () => ({
    type: 'GET_CATEGORIES',
    payload: { },
});

export const getCategoriesSuccess = (categories) => ({
    type: 'GET_CATEGORIES_SUCCESS',
    payload: categories,
});

export const addTask = (task) => ({
    type: 'ADD_TASK',
    payload: task,
});

export const addTaskSuccess = (task) => ({
    type: 'ADD_TASK_SUCCESS',
    payload: task,
});

export const addCategory = (category) => ({
    type: 'ADD_CATEGORY',
    payload: category,
});

export const addCategorySuccess = (category) => ({
    type: 'ADD_CATEGORY_SUCCESS',
    payload: category,
});

export const taskCompleted = (taskId) => ({
    type: 'TASK_COMLPETED',
    payload: taskId,
});

export const taskCompletedSuccess = (taskId, completed) => ({
    type: 'TASK_COMLPETED_SUCCESS',
    payload: {taskId, completed},
});

export const removeTask = (taskId) => ({
    type: 'REMOVE_TASK',
    payload: taskId,
});

export const removeTaskSuccess = (taskId) => ({
    type: 'REMOVE_TASK_SUCCESS',
    payload: taskId,
});

export const failed = (error) => ({
    type: 'FAILED',
    payload: error,
});