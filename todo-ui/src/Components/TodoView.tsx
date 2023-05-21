import { FunctionComponent } from "react";
import { Todo } from "src/Services/ITodoService.ts";
import { Box, Checkbox, Stack } from "@mui/material";

const TodoView: FunctionComponent<{ todo: Todo }> = (props) => {
  return (
    <Stack direction={"row"} gap={2}>
      <Box sx={{ my: "auto" }}>{props.todo.text}</Box>
      <Checkbox sx={{ ml: "auto" }} />
    </Stack>
  );
};

export default TodoView;
