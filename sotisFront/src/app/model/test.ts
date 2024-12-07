import { ConnectQuestion } from "./connectQuestion";
import { DefQuestion } from "./defQuestion";
import { Field } from "./field";

export interface Test{
    id: number,
    Name: string,
    field: Field,
    connectQUestions: ConnectQuestion[],
    definitionQuestions: DefQuestion[],
}