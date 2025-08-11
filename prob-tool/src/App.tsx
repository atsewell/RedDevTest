import React from "react";
import "./App.css";
import ProbForm from "./components/ProbForm";

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <h1>Probability Tool</h1>
      </header>
      <div className="container">
        <ProbForm />
      </div>
    </div>
  );
}

export default App;
