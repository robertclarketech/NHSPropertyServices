export interface ITodoService {
  get: (showComplete: boolean) => Promise<Todo[]>;
  complete: (id: string) => Promise<void>;
  create: (newTodo: string) => Promise<void>;
}

export interface Todo {
  id: string;
  created: string;
  text: string;
  completed?: string;
}
