import { ProbData } from "../interfaces/ProbData";

export interface ProbFormProps {
  onSubmit?: (data: ProbData) => void;
  clearResult?: () => void;
}
