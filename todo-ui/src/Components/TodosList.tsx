import { FunctionComponent } from "preact";
import { useTodoService } from "src/Contexts/TodoServiceContext.tsx";
import { useEffect, useState } from "preact/compat";
import { Todo } from "src/Services/ITodoService.ts";
import { Alert, CircularProgress, Paper, Stack } from "@mui/material";
import { useMemo } from "react";
import TodoView from "src/Components/TodoView.tsx";

type State =
  | { type: "Loading" }
  | { type: "Loaded"; todos: Todo[] }
  | { type: "Error"; error: string };

const TodosList: FunctionComponent = () => {
  const todosService = useTodoService();
  const [state, setState] = useState<State>({ type: "Loading" });

  const el = useMemo(() => {
    switch (state.type) {
      case "Loading":
        return <CircularProgress />;
      case "Error":
        return <Alert severity="error">{state.error}</Alert>;
      case "Loaded":
        return (
          <Stack gap={2}>
            {state.todos.map((todo) => (
              <TodoView key={todo.id} todo={todo} />
            ))}
          </Stack>
        );
    }
  }, [state]);

  useEffect(() => {
    (async () => {
      try {
        setState({ type: "Loaded", todos: await todosService.get() });
      } catch (e) {
        setState({ type: "Error", error: (e as Error).message });
      }
    })();
  });

  return <Paper sx={{ padding: 2 }}>{el}</Paper>;
};

export default TodosList;
