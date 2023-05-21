import { createContext, FunctionComponent } from "preact";
import { useContext, useMemo } from "react";
import { ITodoService } from "src/Services/ITodoService.ts";
import { TodoService } from "src/Services/TodoService.ts";

export const TodoServiceContext = createContext<ITodoService | undefined>(
  undefined
);

export const TodoServiceContextProvider: FunctionComponent = (props) => {
  const service = useMemo(() => new TodoService(), []);

  return (
    <TodoServiceContext.Provider value={service}>
      {props.children}
    </TodoServiceContext.Provider>
  );
};

export const useTodoService = (): ITodoService => {
  const context = useContext(TodoServiceContext);
  if (context) {
    return context;
  }
  throw new Error("No provider for context");
};

export default TodoServiceContext;
