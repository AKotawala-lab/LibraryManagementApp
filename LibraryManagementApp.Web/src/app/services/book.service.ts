import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, map, of } from 'rxjs';
import { Book } from '../models/book';
import { environment } from '../../environments/environment'
import { Author } from '../models/author';
import { BookResponse, BooksResponse } from '../models/book-response.model';
import { AuthorsResponse } from '../models/author-response.model';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  private apiUrl = environment.apiUrl + '/book';

  constructor(private http: HttpClient) { }

  getBooks(): Observable<Book[]> {
    return this.http.get<Book[]>(this.apiUrl).pipe(
      catchError(this.handleError<Book[]>('getBooks', []))
    );
  }

  getBook(id: number): Observable<Book> {
    return this.http.get<Book>(`${this.apiUrl}/${id}`).pipe(
      catchError(this.handleError<Book>('getBook'))
    );
  }

  getAuthorsByBook(id: number): Observable<Author[]> {
    return this.http.get<Author[]>(`${this.apiUrl}/${id}/authors`).pipe(
      catchError(this.handleError<Author[]>('getAuthors', []))
    );
  }

  getBooksByAuthor(authId: number): Observable<Book[]> {
    return this.http.get<Book[]>(`${this.apiUrl}/author/${authId}`).pipe(
      catchError(this.handleError<Book[]>('getBooksByAuthor', []))
    );
  }  

  addBook(book: Book): Observable<Book> {
    return this.http.post<Book>(this.apiUrl, book).pipe(
      catchError(this.handleError<Book>('addBook'))
    );
  }

  updateBook(id: number, book: Book): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, book).pipe(
      catchError(this.handleError<any>('updateBook'))
    );
  }

  deleteBook(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      catchError(this.handleError<any>('deleteBook'))
    );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(`${operation} failed: ${error.message}`);
      return of(result as T);
    };
  }  
}
