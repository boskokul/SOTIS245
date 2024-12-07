import { NumPair } from "./numPair";

export interface TestForm{
    fieldName: string,
    name: string,
    defQuestionsNum: number,
    conQuestionsNum: NumPair[],
}