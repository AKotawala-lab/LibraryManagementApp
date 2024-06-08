import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BookService } from '../../../services/book.service';
import { AuthorService } from '../../../services/author.service';
import { Book } from '../../../models/book';
import { Author } from '../../../models/author';

@Component({
  selector: 'app-book-add-edit',
  templateUrl: './book-add-edit.component.html',
  styleUrls: ['./book-add-edit.component.css']
})
export class BookAddEditComponent implements OnInit {
  bookForm: FormGroup;
  authors: Author[] = [];
  selectedAuthor: Author | null = null;
  bookId: number | null = null;

  constructor(
    private fb: FormBuilder,
    private bookService: BookService,
    private authorService: AuthorService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.bookForm = this.fb.group({
      bookId: [0],
      title: ['', Validators.required],
      description: ['', Validators.required],
      authorId: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadAuthors();
    this.bookId = this.route.snapshot.params['id'];
    if (this.bookId) {
      this.loadBook(this.bookId);
    }
  }

  loadAuthors(): void {
    this.authorService.getAuthors().subscribe(authors => {
      this.authors = authors;
    });
  }

  loadAuthor(authorId: number): void {
    this.authorService.getAuthorById(authorId).subscribe(author => {
      this.selectedAuthor = author;
    });
  }

  loadBook(id: number): void {
    this.bookService.getBook(id).subscribe(book => {
      this.bookForm.patchValue({
        bookId: book.bookId,
        title: book.title,
        description: book.description,
        authorId: book.authorId
      });
      this.loadAuthor(book.authorId);
    });
  }

  onAuthorChange(event: any): void {
    const authorId = event.target.value;
    this.loadAuthor(authorId);
  }  

  onSubmit(): void {
    if (this.bookForm.valid) {

      const bookData: Book = {
        ...this.bookForm.value,
        author: this.selectedAuthor
      };

      if (this.bookId) {
        console.info(bookData);
        this.bookService.updateBook(this.bookId, bookData).subscribe(() => {
          this.router.navigate(['/books']);
        });
      } else {
        this.bookService.addBook(bookData).subscribe(() => {
          this.router.navigate(['/books']);
        });
      }
    }
  }

  cancel(): void {
    this.router.navigate(['/books']);
  }
}
