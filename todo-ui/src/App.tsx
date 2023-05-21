import { Container } from "@mui/material";
import { TodoServiceContextProvider } from "src/Contexts/TodoServiceContext.tsx";
import Todos from "src/Components/Todos.tsx";

export function App() {
  return (
    <TodoServiceContextProvider>
      <Container>
        <Todos />
      </Container>
    </TodoServiceContextProvider>
  );
}
