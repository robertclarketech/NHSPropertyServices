import { FunctionComponent } from "react";
import { Todo } from "src/Services/ITodoService.ts";
import { Box, Checkbox, Paper, Stack, Typography } from "@mui/material";

const TodoView: FunctionComponent<{
  todo: Todo;
  onComplete: (id: string) => Promise<void>;
}> = (props) => {
  return (
    <Paper
      sx={{
        padding: 2,
        textDecoration:
          props.todo.completed !== undefined ? "line-through" : undefined,
      }}
    >
      <label for={props.todo.id}>
        <Stack>
          <Stack direction={"row"} gap={2}>
            <Box sx={{ my: "auto" }}>
              <Typography variant={"h6"}>{props.todo.text}</Typography>
            </Box>
            <Checkbox
              id={props.todo.id}
              sx={{ ml: "auto" }}
              disabled={
                props.todo.completed !== null &&
                props.todo.completed !== undefined
              }
              onChange={() => props.onComplete(props.todo.id)}
            />
          </Stack>
          <Box>
            <Typography variant={"subtitle1"}>
              Created {new Date(props.todo.created).toLocaleString()}
            </Typography>
          </Box>
        </Stack>
      </label>
    </Paper>
  );
};

export default TodoView;
