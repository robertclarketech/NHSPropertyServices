import { render } from 'preact'
import { App } from './App.tsx'
import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';


const div = document.createElement("div");
document.body.appendChild(div);
render(<App />, div)
