import { ofType } from 'redux-observable';
import { from, of } from 'rxjs';
import { map, switchMap, catchError, withLatestFrom } from 'rxjs/operators';
import { getTasksSuccess, getCategoriesSuccess, taskCompletedSuccess, removeTaskSuccess, addCategorySuccess, addTaskSuccess, failed } from './actions';

export const BASE_URL = "https://localhost:7258/graphql";

const createGraphqlEpic = (actionType, query, successActionCreator) => {
    return (action$, state$) =>
        action$.pipe(
            ofType(actionType),
            withLatestFrom(state$),
            switchMap(([action, state]) => {
                const body = JSON.stringify({ query: typeof query === 'function' ? query(action) : query });
                return from(
                    fetch(BASE_URL, {
                        method: "POST",
                        headers: {
                            "Access-Control-Allow-Origin": "*",
                            "Accept": "application/json",
                            "Content-Type": "application/json",
                            "Storage": state.activeStorage,
                        },
                        body,
                    }).then(response => response.json())
                ).pipe(
                    map(response => {
                        console.log(response);
                        if (response.errors) {
                            throw new Error(response.errors[0].message);
                        }
                        return successActionCreator(response.data);
                    }),
                    catchError(error => {
                        return of(failed(error.message));
                    })
                );
            })
        );
};

export const getTasksEpic = createGraphqlEpic(
    'GET_TASKS',
    `
    {
      tasks {
        id
        description
        completed
        deadLine
        categoryId
      }
    }
  `,
    data => getTasksSuccess(data.tasks)
);

export const getCategoriesEpic = createGraphqlEpic(
    'GET_CATEGORIES',
    `
    {
      categories {
        id
        name
      }
    }
  `,
    data => getCategoriesSuccess(data.categories)
);

export const removeTaskEpic = createGraphqlEpic(
    'REMOVE_TASK',
    (action) =>`
    mutation {
      taskMutation {
        delete(id: ${action.payload})
      }
    }
  `,
    data => removeTaskSuccess(data.taskMutation.delete)
);

export const taskCompletedEpic = createGraphqlEpic(
    'TASK_COMLPETED',
    (action) =>`
    mutation{
        taskMutation{
            completed(id: ${action.payload}){
            id
            completed
            }
        }
    }
  `,
    data => taskCompletedSuccess(data.taskMutation.completed.id, data.taskMutation.completed.completed)
);

export const addCategoryEpic = createGraphqlEpic(
    'ADD_CATEGORY',
    (action) =>`
    mutation{
      categoryMutation{
        add(category: {name: "${action.payload.name}"}){
        id
        name
        }
      }
    }
  `,
    data => addCategorySuccess(data.categoryMutation.add)
);

export const addTaskEpic = createGraphqlEpic(
    'ADD_TASK',
    (action) => {
        const { description, deadLine, categoryId } = action.payload;
        let taskFields = `
            description: "${description}",
            categoryId: ${categoryId || null},
            completed: false
        `;
        if (deadLine) {
            taskFields += `, deadLine: "${deadLine}"`;
        }
        return `
        mutation {
          taskMutation {
            add(task: {${taskFields}}) {
              id
              description
              completed
              deadLine
              categoryId
            }
          }
        }
      `;
    },
    data => addTaskSuccess(data.taskMutation.add)
);