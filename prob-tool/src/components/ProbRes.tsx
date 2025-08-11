import React from "react";
import { ProbResProps } from "../interfaces/ProbResProps";

const ProbRes = ({ result }: ProbResProps) => {
  if (result === null) {
    return null;
  }

  return (
    <div className="ProbRes">
      <h2>Result</h2>
      <div className="ProbRes-result">{result ?? "no result"}</div>
    </div>
  );
};

export default ProbRes;
