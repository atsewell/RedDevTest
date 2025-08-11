import React, { useState } from "react";
import "./App.css";
import ProbForm from "./components/ProbForm";
import ProbRes from "./components/ProbRes";
import { ProbData } from "./interfaces/ProbData";

function App() {
  const [result, setResult] = useState<number | null>(null);

  const calculate = (formData: ProbData) => {
    dummyCalc(formData);
  };

  const clearResult = () => setResult(null);

  const dummyCalc = (formData: ProbData) => {
    const calculatedResult = formData.prob1 * formData.prob2; // Example calculation
    setResult(calculatedResult);
  };

  return (
    <div className="App">
      <header className="App-header">
        <h1>Probability Tool</h1>
      </header>
      <div className="container">
        <ProbForm onSubmit={calculate} clearResult={clearResult} />
        <ProbRes result={result} />
      </div>
    </div>
  );
}

export default App;
