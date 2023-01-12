import { Book } from "./book";

export class Author {
  id: number;
  no: number;
  firstName: string;
  lastName: string;
  fullName: string;
  country: string;
  city: string;
  dateOfBirth: Date;
  imageUrl: string;
  isFavourite: boolean;
  gender: number;
  introduction: string;
  books: Book[] = [];
  age: number;
  genderName: string;
}
