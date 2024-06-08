import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Author } from '../../../../models/author';
import { Book } from '../../../../models/book';
import { AuthorService } from '../../../../services/author.service';
import { BookService } from '../../../../services/book.service';

@Component({
  selector: 'app-author-add-edit',
  templateUrl: './author-add-edit.component.html',
  styleUrls: ['./author-add-edit.component.css']
})
export class AuthorAddEditComponent implements OnInit {
  authorForm: FormGroup;
  books: Book[] = [];
  selectedBooks: Book[] = [];

  constructor(
    private fb: FormBuilder,
    private authorService: AuthorService,
    private bookService: BookService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.authorForm = this.fb.group({
      authorId: [ 0],
      name: ['', Validators.required],
      description: [''],
      selectedBooks: [[]]
    });
  }

  ngOnInit(): void {
    this.loadBooks();
    this.route.params.subscribe(params => {
      console.info(params['id']);
      if (params['id']) {
        this.loadAuthor(params['id']);
      }
    });
  }

  loadBooks() {
    this.bookService.getBooks().subscribe(books => this.books = books);
  }

  loadAuthor(id: number) {
    this.authorService.getAuthorById(id).subscribe(author => {
      this.authorForm.patchValue({
        authorId: author.authorId,
        name: author.name,
        description: author.description
      });
      //this.selectedBooks = author.books? author.books : [];
      this.bookService.getBooksByAuthor(id).subscribe(authorBooks => this.selectedBooks = authorBooks);
    });
  }

  saveAuthor() {
    if (this.authorForm.valid) {
      
      const authorData: Author = {
        ...this.authorForm.value,
        books: this.selectedBooks
      };

      if (this.route.snapshot.params['id']) {
        // Editing existing author
        console.info(authorData);
        this.authorService.updateAuthor(authorData).subscribe(() => {
          this.router.navigate(['/authors']);
        });
      } else {
        // Adding new author
        this.authorService.addAuthor(authorData).subscribe(() => {
          this.router.navigate(['/authors']);
        });
      }
    }
  }

  toggleBookSelection(book: Book) {
    const index = this.selectedBooks.findIndex(b => b.bookId === book.bookId);
    if (index !== -1) {
      this.selectedBooks.splice(index, 1);
    } else {
      this.selectedBooks.push(book);
    }
  }

  isSelected(book: Book): boolean {
    return this.selectedBooks.some(selectedBook => selectedBook.bookId === book.bookId);
  }

  cancel() {
    this.router.navigate(['/authors']);
  }
}
