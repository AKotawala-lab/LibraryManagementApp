import { Book } from "./book";

export interface Author {
  authorId: number;
  name: string;
  description: string;
  books: Book[];
}
