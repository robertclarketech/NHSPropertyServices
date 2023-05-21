import { ITodoService, Todo } from "src/Services/ITodoService.ts";

export class TodoService implements ITodoService {
  private readonly url: string;
  constructor() {
    this.url = import.meta.env.VITE_API_URL;
  }

  get = async (): Promise<Todo[]> => {
    const result = await fetch(`${[this.url, "todo", "list"].join("/")}`);
    return await result.json();
  };
}
