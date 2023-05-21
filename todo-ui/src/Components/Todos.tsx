import { FunctionComponent } from "preact";
import { useTodoService } from "src/Contexts/TodoServiceContext.tsx";
import { useEffect, useMemo, useState } from "react";
import {
  Alert,
  Box,
  Checkbox,
  CircularProgress,
  FormControl,
  IconButton,
  InputAdornment,
  InputLabel,
  OutlinedInput,
  Paper,
  Stack,
} from "@mui/material";
import TodoView from "src/Components/TodoView.tsx";
import AddIcon from "@mui/icons-material/Add";
import { Todo } from "src/Services/ITodoService.ts";

type State =
  | { type: "Loading" }
  | { type: "Loaded"; todos: Todo[] }
  | { type: "Error"; error: string };
const Todos: FunctionComponent = () => {
  const todosService = useTodoService();
  const [state, setState] = useState<State>({ type: "Loading" });
  const [showComplete, setShowComplete] = useState(false);
  const [newTodo, setNewTodo] = useState("");

  const onComplete = async (id: string) => {
    setState({ type: "Loading" });
    try {
      await todosService.complete(id);
      setState({ type: "Loaded", todos: await todosService.get(showComplete) });
    } catch (e) {
      setState({ type: "Error", error: (e as Error).message });
    }
  };

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
              <TodoView key={todo.id} todo={todo} onComplete={onComplete} />
            ))}
          </Stack>
        );
    }
  }, [state]);

  useEffect(() => {
    (async () => {
      try {
        setState({
          type: "Loaded",
          todos: await todosService.get(showComplete),
        });
      } catch (e) {
        setState({ type: "Error", error: (e as Error).message });
      }
    })();
  }, [showComplete]);

  const onSubmit = async (e: Event) => {
    e.preventDefault();

    if (state.type === "Loading") {
      return;
    }

    setState({ type: "Loading" });
    try {
      await todosService.create(newTodo);
      setNewTodo("");
      setState({
        type: "Loaded",
        todos: await todosService.get(showComplete),
      });
    } catch (e) {
      setState({ type: "Error", error: (e as Error).message });
    }
  };

  return (
    <Stack gap={2}>
      <Box as={"form"} onSubmit={onSubmit}>
        <FormControl variant="outlined" sx={{ width: "100%" }}>
          <InputLabel htmlFor="add-todo">Add Todo</InputLabel>
          <OutlinedInput
            id="add-todo"
            endAdornment={
              <InputAdornment position="end">
                <IconButton
                  aria-label="toggle password visibility"
                  onClick={() => {}}
                  edge="end"
                  type={"submit"}
                  disabled={state.type === "Loading"}
                >
                  <AddIcon />
                </IconButton>
              </InputAdornment>
            }
            label="Add Todo"
            value={newTodo}
            onChange={(e) => setNewTodo(e.currentTarget.value)}
            disabled={state.type === "Loading"}
          />
        </FormControl>
      </Box>
      <Stack gap={2}>
        <Paper sx={{ padding: 2 }}>
          <Stack gap={1} direction={"row"}>
            <Box sx={{ my: "auto" }}>Show Complete</Box>
            <Checkbox onChange={() => setShowComplete((old) => !old)} />
          </Stack>
        </Paper>
        {el}
      </Stack>
    </Stack>
  );
};

export default Todos;
