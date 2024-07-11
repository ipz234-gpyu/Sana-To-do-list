import { createStore, applyMiddleware } from 'redux';
import { createEpicMiddleware, combineEpics } from 'redux-observable';
import rootReducer from './reducers';
import { getTasksEpic, getCategoriesEpic, taskCompletedEpic, removeTaskEpic, addCategoryEpic, addTaskEpic } from './epic';

const epicMiddleware = createEpicMiddleware();

const rootEpic = combineEpics(
    getTasksEpic,
    getCategoriesEpic,
    taskCompletedEpic,
    addCategoryEpic,
    addTaskEpic,
    removeTaskEpic,
);

const store = createStore(
    rootReducer,
    applyMiddleware(epicMiddleware)
);

epicMiddleware.run(rootEpic);

export default store;