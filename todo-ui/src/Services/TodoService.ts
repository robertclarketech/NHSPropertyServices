import { ITodoService, Todo } from "src/Services/ITodoService.ts";

export class TodoService implements ITodoService {
  private readonly url: string;
  constructor() {
    this.url = import.meta.env.VITE_API_URL;
  }

  get = async (showComplete: boolean): Promise<Todo[]> => {
    const url = new URL(`${[this.url, "todo"].join("/")}`);
    url.searchParams.set("showCompleted", showComplete.toString());
    const result = await fetch(url);
    if (result.ok) {
      return await result.json();
    } else {
      throw new Error(await result.text());
    }
  };

  complete = async (id: string): Promise<void> => {
    const result = await fetch(
      `${[this.url, "todo", id, "complete"].join("/")}`,
      { method: "POST" }
    );
    if (result.ok) {
      return;
    } else {
      throw new Error(await result.text());
    }
  };

  create = async (newTodo: string): Promise<void> => {
    const result = await fetch(`${[this.url, "todo"].join("/")}`, {
      method: "POST",
      body: JSON.stringify({ text: newTodo }),
      headers: {
        "Content-Type": "application/json",
      },
    });
    if (result.ok) {
      return;
    } else {
      throw new Error(await result.text());
    }
  };
}
