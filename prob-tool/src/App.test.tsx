import React from "react";
import { render, screen } from "@testing-library/react";
import App from "./App";

test("renders app component", () => {
  render(<App />);
  const headingElement = screen.getByText(/probability tool/i);
  expect(headingElement).toBeInTheDocument();
});

test("renders probability form element", () => {
  render(<App />);
  const headingElement = screen.getByRole("form", { name: "Probability Form" });
  expect(headingElement).toBeInTheDocument();
});
