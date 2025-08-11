import React from "react";
import { render, screen, fireEvent } from "@testing-library/react";
import ProbForm from "./ProbForm";

test("renders probability form", () => {
  render(<ProbForm />);
  const formElement = screen.getByRole("form", { name: /probability form/i });
  expect(formElement).toBeInTheDocument();
});

test("renders probFun element", () => {
  render(<ProbForm />);
  const probFunElement = screen.getByTestId("probFun-fields");
  expect(probFunElement).toBeInTheDocument();
});

test("selecting CombinedWith checks ProbFun-Combined", () => {
  render(<ProbForm />);
  const combinedWithRadio = screen.getByLabelText(/CombinedWith/i);
  fireEvent.click(combinedWithRadio);
  const probFunCombined = screen.getByTestId("probFun-Combined");
  expect(probFunCombined).toBeChecked();
  const probFunEither = screen.getByTestId("probFun-Either");
  expect(probFunEither).not.toBeChecked();
});

test("selecting Either checks ProbFun-Either", () => {
  render(<ProbForm />);
  const combinedWithRadio = screen.getByLabelText(/Either/i);
  fireEvent.click(combinedWithRadio);
  const probFunCombined = screen.getByTestId("probFun-Either");
  expect(probFunCombined).toBeChecked();
  const probFunEither = screen.getByTestId("probFun-Combined");
  expect(probFunEither).not.toBeChecked();
});

test("shows error message when prob1 is invalid", () => {
  render(<ProbForm />);
  const prob1Input = screen.getByLabelText(/Probability 1:/i);
  fireEvent.change(prob1Input, { target: { value: "2" } }); // invalid (>1)
  const alertElement = screen.getByRole("alert", {
    name: /probability 1 alert/i,
  });
  expect(alertElement).toHaveTextContent(/probability must be/i);
});

test("does not show error message when prob1 is valid", () => {
  render(<ProbForm />);
  const prob1Input = screen.getByLabelText(/Probability 1:/i);
  fireEvent.change(prob1Input, { target: { value: "0.5" } });
  expect(screen.queryByRole("alert")).not.toBeInTheDocument();
});

test("shows error message when prob2 is invalid", () => {
  render(<ProbForm />);
  const prob2Input = screen.getByLabelText(/Probability 2:/i);
  fireEvent.change(prob2Input, { target: { value: "-0.1" } }); // invalid (<0)
  const errorMsg = screen.getByRole("alert", {
    name: /probability 2 alert/i,
  });
  expect(errorMsg).toHaveTextContent(/probability must be/i);
});

test("calls onSubmit when form is filled and submitted", () => {
  const defaultAlert = window.alert;
  window.alert = jest.fn();
  const onSubmit = jest.fn();
  render(<ProbForm onSubmit={onSubmit} />);
  const probFunCombined = screen.getByTestId("probFun-Combined");
  const prob1Input = screen.getByLabelText(/Probability 1:/i);
  const prob2Input = screen.getByLabelText(/Probability 2:/i);
  fireEvent.click(probFunCombined);
  fireEvent.change(prob1Input, { target: { value: "0.4" } });
  fireEvent.change(prob2Input, { target: { value: "0.6" } });

  const submitButton = screen.getByRole("button", { name: /calculate/i });
  fireEvent.click(submitButton);

  expect(onSubmit).toHaveBeenCalled();

  window.alert = defaultAlert;
});

test("alerts and does not call onSubmit when form is submitted incomplete", () => {
  const defaultAlert = window.alert;
  window.alert = jest.fn();
  const onSubmit = jest.fn();
  render(<ProbForm onSubmit={onSubmit} />);
  // No function chosen
  const prob1Input = screen.getByLabelText(/Probability 1:/i);
  const prob2Input = screen.getByLabelText(/Probability 2:/i);
  fireEvent.change(prob1Input, { target: { value: "0.4" } });
  fireEvent.change(prob2Input, { target: { value: "0.6" } });

  const submitButton = screen.getByRole("button", { name: /calculate/i });
  fireEvent.click(submitButton);

  expect(window.alert).toHaveBeenCalled();
  expect(onSubmit).not.toHaveBeenCalled();

  window.alert = defaultAlert;
});
