import React from "react";
import { render, screen } from "@testing-library/react";
import ProbForm from "./ProbForm";

test("renders probability form", async () => {
  render(<ProbForm />);
  const formElement = screen.getByRole("form");
  expect(formElement).toBeInTheDocument();
});
