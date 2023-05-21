export interface ITodoService {
  get: () => Promise<Todo[]>;
}

export interface Todo {
  id: string;
  created: string;
  text: string;
  completed?: string;
}
