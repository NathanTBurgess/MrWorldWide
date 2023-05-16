import { ProblemDetails } from "../../domain/models";

export function isProblemDetails(candidate: object): candidate is ProblemDetails {
    return "type" in candidate && "status" in candidate && "detail" in candidate;
}
