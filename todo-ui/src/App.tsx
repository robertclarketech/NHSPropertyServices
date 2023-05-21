import { Container, Paper, Stack } from "@mui/material";
import { TodoServiceContextProvider } from "src/Contexts/TodoServiceContext.tsx";
import TodosList from "src/Components/TodosList.tsx";

export function App() {
  return (
    <TodoServiceContextProvider>
      <Container>
        <Stack gap={2}>
          <Paper>asd</Paper>
          <TodosList />
        </Stack>
      </Container>
    </TodoServiceContextProvider>
  );
}
