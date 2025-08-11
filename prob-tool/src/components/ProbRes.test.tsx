import React from "react";
import { render, screen } from "@testing-library/react";
import ProbRes from "./ProbRes";

test("renders result when not null", () => {
  let result = 0.5;
  render(<ProbRes result={result} />);
  const headerElement = screen.getByRole("heading", {
    level: 2,
    name: /result/i,
  });
  expect(headerElement).toBeInTheDocument();
});

test("does not render result when null", () => {
  let result = null;
  render(<ProbRes result={result} />);
  const headerElement = screen.queryByRole("heading", {
    level: 2,
    name: /result/i,
  });
  expect(headerElement).not.toBeInTheDocument();
});
