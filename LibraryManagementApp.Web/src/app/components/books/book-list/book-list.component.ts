import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Book } from '../../../models/book';
import { BookService } from '../../../services/book.service';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css']
})
export class BookListComponent implements OnInit {
  books: Book[] = [];
  selectedBook: Book | null = null;

  constructor(private bookService: BookService, private router: Router) {}

  ngOnInit(): void {
    this.loadBooks();
  }

  loadBooks(): void {
    this.bookService.getBooks().subscribe(books => {
      this.books = books;
    });
  }

  selectBook(book: Book): void {
    this.selectedBook = book;
  }

  navigateToAddBook(): void {
    this.router.navigate(['/books/add']);
  }

  navigateToEditBook(id: number): void {
    this.router.navigate([`/books/edit/${id}`]);
  }

  confirmDelete(id: number): void {
    if (confirm('Are you sure you want to delete this book?')) {
      this.bookService.deleteBook(id).subscribe(() => {
        this.loadBooks();
        this.selectedBook = null;
      });
    }
  }
}
