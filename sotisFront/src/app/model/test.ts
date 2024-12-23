import { ConnectQuestion } from "./connectQuestion";
import { DefQuestion } from "./defQuestion";
import { Field } from "./field";

export interface Test{
    id: number,
    name: string,
    field: Field,
    connectQuestions: ConnectQuestion[],
    definitionQuestions: DefQuestion[],
}