import { ConnectQuestion } from './connectQuestion';
import { DefQuestion } from './defQuestion';
import { Field } from './field';
import { Student } from './user';

export interface Test {
  id: number;
  name: string;
  field: Field;
  connectQuestions: ConnectQuestion[];
  definitionQuestions: DefQuestion[];
}

export interface Pair {
  belongTerm: string;
  belongingTerm: string;
  isCorrect: boolean;
}

export interface DefinitionAnswerDTO {
  id: number;
  term: string;
  defQuestionId: number;
  answeredDefinition?: string;
  isCorrect: boolean;
  testSampleId: number;
}

export interface ConnectAnswerDTO {
  id: number;
  connectQuestionId: number;
  connectedPairs: Pair[];
  testSampleId: number;
}

export interface TestSampleDTO {
  id: number;
  takenOn: Date;
  studentId: number;
  testId: number;
  definitionAnswers: DefinitionAnswerDTO[];
  connectAnswers: ConnectAnswerDTO[];
}

export interface DefinitionAnswer {
  id: number;
  question: DefQuestion;
  answeredDefinition: string;
  isCorrect: boolean;
}

export interface ConnectAnswer {
  id: number;
  question: ConnectQuestion;
  connectedPairs: Pair[];
}

export interface TestSample {
  id: number;
  takenOn: Date;
  student: Student;
  test: Test;
  definitionAnswers: DefinitionAnswer[];
  connectAnswers: ConnectAnswer[];
}
