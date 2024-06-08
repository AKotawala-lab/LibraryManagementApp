import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from '../../environments/environment'
import { Author } from '../models/author';
import { AuthorResponse, AuthorsResponse } from '../models/author-response.model';

@Injectable({
  providedIn: 'root'
})
export class AuthorServiceResponseReultRender {
  private apiUrl = environment.apiUrl + '/author';

  constructor(private http: HttpClient) { }

  getAuthors(): Observable<Author[]> {
    return this.http.get<AuthorsResponse>(this.apiUrl).pipe(
      map((response: AuthorsResponse) => response.result),
      catchError(this.handleError<Author[]>('getAuthors', []))
    );
  }

  getAuthorById(id: number): Observable<Author> {
    return this.http.get<AuthorResponse>(`${this.apiUrl}/${id}`).pipe(
      map((response: AuthorResponse) => response.result),
      catchError(this.handleError<Author>('getAuthorById'))
    );
  }

  addAuthor(author: Author): Observable<Author> {
    return this.http.post<Author>(this.apiUrl, author).pipe(
      catchError(this.handleError<Author>('addAuthor'))
    );
  }

  updateAuthor(author: Author): Observable<any> {
    return this.http.put(`${this.apiUrl}/${author.authorId}`, author).pipe(
      catchError(this.handleError<any>('updateAuthor'))
    );
  }

  deleteAuthor(id: number): Observable<Author> {
    return this.http.delete<Author>(`${this.apiUrl}/${id}`).pipe(
      catchError(this.handleError<Author>('deleteAuthor'))
    );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(`${operation} failed: ${error.message}`);
      return of(result as T);
    };
  }
}
