import React, { useState } from "react";
import "./App.css";
import ProbForm from "./components/ProbForm";
import ProbRes from "./components/ProbRes";
import { ProbData } from "./interfaces/ProbData";

function App() {
  const ApiBaseUrl = process.env.REACT_APP_API_URL;
  const [result, setResult] = useState<number | null>(null);

  const calculate = (formData: ProbData) => {
    callApi(formData);
  };

  const clearResult = () => setResult(null);

  const callApi = (formData: ProbData) => {
    fetch(`${ApiBaseUrl}calc`, {
      method: "POST",
      mode: "cors",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(formData),
    })
      .then((response) => response.json())
      .then((data) => {
        console.debug("Calculation Result:", data);
        setResult(data.result);
      })
      .catch((error) => {
        console.error("Error during calculation:", error);
        alert("An error occurred while calculating the result.");
        setResult(null);
      });
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
