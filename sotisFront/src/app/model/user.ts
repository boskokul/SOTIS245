export interface User {
  id: number;
  username: string;
  role: string;
}

export interface Student {
  id: number;
  name: string;
  surname: string;
  user: User;
}
