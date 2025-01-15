import { gql } from "@apollo/client";

export const GET_TASKS = gql`
    query getTasks($userId: String!) {
      tasks(userId: $userId) {
        id
        title
        description
        dueDate
        priority
        isCompleted
      }
    }
`;

export const CREATE_TASK = gql`
  mutation CreateTask($input: NewTaskInput!, $userId: String!) {
    createTask(input: $input, userId: $userId
    ) {
      id
    }
  }
`;

export const DELETE_TASK = gql`
  mutation DeleteTask($id: ID!) {
    deleteTask(id: $id)
  }
`;

export const UPDATE_TASK = gql`
mutation UpdateTask($id: ID!, $updatedTask: NewTaskInput!) {
  updateTask(id: $id, updatedTask: $updatedTask) {
    id
    title
    description
    dueDate
    priority
    isCompleted
  }
}
`;

export const TOGGLE_TASK_COMPLETION = gql`
mutation ToggleTaskCompletion($id: ID!, $isCompleted: Boolean!) {
  toggleTaskCompletion(id: $id, isCompleted: $isCompleted) {
    id
    isCompleted
  }
}
`;


