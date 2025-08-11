import React, { useState } from "react";
import { ProbData } from "../interfaces/ProbData";
import { ProbFormProps } from "../interfaces/ProbFormProps";

interface ErrorMessageState {
  prob1: string;
  prob2: string;
  probFun: string;
}

const ProbForm = ({ onSubmit, clearResult }: ProbFormProps) => {
  const [formData, setFormData] = useState({
    prob1: 0,
    prob2: 0,
    probFun: "",
  } as ProbData);
  const [errors, setErrors] = useState({
    prob1: "",
    prob2: "",
    probFun: "",
  } as ErrorMessageState);

  const funcLabels = {
    Combined: "CombinedWith",
    Either: "Either",
  };

  const formIsValid = () =>
    errors.prob1 === "" &&
    errors.prob2 === "" &&
    errors.probFun === "" &&
    formData.prob1 &&
    formData.prob2 &&
    formData.probFun;

  const handleSubmit = () => (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    if (!formIsValid()) {
      alert("Please complete the form");
      return;
    }
    console.debug("Form Data:", formData);
    onSubmit && onSubmit(formData);
  };

  const handleFieldChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name } = e.target;
    const setValue = handleValidation(e.target);
    setFormData({ ...formData, [name]: setValue });
    clearResult && clearResult();
  };

  const updateErrorState = (
    field: keyof ErrorMessageState,
    message: string
  ) => {
    setErrors((prevErrors) => ({
      ...prevErrors,
      [field]: message,
    }));
  };

  const isValidProb = (value: any): boolean =>
    value && !isNaN(value) && parseFloat(value) > 0 && parseFloat(value) <= 1;

  const handleValidation = (target: EventTarget): any => {
    const input = target as HTMLInputElement;
    const { name, value } = input;

    switch (name) {
      case "prob1":
        if (!isValidProb(value)) {
          updateErrorState(
            "prob1",
            "Probability must be greater than 0 and less than 1."
          );
        } else {
          updateErrorState("prob1", "");
        }
        return parseFloat(value);
      case "prob2":
        if (!isValidProb(value)) {
          updateErrorState(
            "prob2",
            "Probability must be greater than 0 and less than 1."
          );
        } else {
          updateErrorState("prob2", "");
        }
        return parseFloat(value);
      default:
        return value;
    }
  };

  return (
    <div className="ProbForm">
      <form onSubmit={handleSubmit()} aria-label="Probability Form">
        <fieldset data-testid="probFun-fields" className="Probform-fieldset">
          <legend className="Probform-legend">Function</legend>
          {Object.entries(funcLabels).map(([value, label]) => (
            <label key={value} className="Probform-radio-label">
              <input
                type="radio"
                data-testid={`probFun-${value}`}
                name="probFun"
                value={value}
                checked={formData.probFun === value}
                className="ProbForm-radio-input"
                onChange={handleFieldChange}
              ></input>
              {label}
            </label>
          ))}
        </fieldset>
        <br />
        <label className="ProbForm-label">
          Probability 1:
          <input
            type="text"
            inputMode="numeric"
            placeholder="fractional probability e.g. 0.5"
            name="prob1"
            aria-label="Probability 1:"
            id="prob1"
            className="ProbForm-input"
            onChange={handleFieldChange}
          />
        </label>
        {errors.prob1 && (
          <p
            role="alert"
            aria-label="probability 1 alert"
            className="Probform-alert"
          >
            {errors.prob1}
          </p>
        )}
        <label className="ProbForm-label">
          Probability 2:
          <input
            type="text"
            inputMode="numeric"
            placeholder="fractional probability e.g. 0.5"
            name="prob2"
            aria-label="Probability 2:"
            id="prob2"
            className="ProbForm-input"
            onChange={handleFieldChange}
          />
        </label>
        {errors.prob2 && (
          <p
            role="alert"
            aria-label="probability 2 alert"
            className="Probform-alert"
          >
            {errors.prob2}
          </p>
        )}
        <br />
        <button type="submit" className="ProbForm-button">
          Calculate
        </button>
      </form>
    </div>
  );
};

export default ProbForm;
