import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Author } from '../../models/author';
import { AuthorService } from '../../services/author.service';
import { Book } from '../../models/book';
import { BookService } from '../../services/book.service';

@Component({
  selector: 'app-authors',
  templateUrl: './authors.component.html'
})
export class AuthorsComponent implements OnInit {
  authors: Author[] = [];
  selectedAuthor: Author | null = null;
  booksWritten: Book[] = [];

  constructor(private authorService: AuthorService, private bookService: BookService, private router: Router) { }

  ngOnInit(): void {
    this.loadAuthors();
  }

  loadAuthors() {
    this.authorService.getAuthors().subscribe(authors => this.authors = authors);
  }

  onSelectAuthor(author: Author) {
    this.selectedAuthor = author;
    this.bookService.getBooksByAuthor(author.authorId).subscribe(authorBooks => this.booksWritten = authorBooks);
  }

  goToEditAuthor(author: Author) {
    this.router.navigate(['/authors/edit', author.authorId]);
  }

  confirmDelete(author: Author) {
    if (confirm(`Are you sure you want to delete ${author.name}?`)) {
      this.deleteAuthor(author.authorId);
    }
  }

  private deleteAuthor(id: number) {
    this.authorService.deleteAuthor(id).subscribe(() => {
      this.authors = this.authors.filter(a => a.authorId !== id);
      this.selectedAuthor = null;
    });
  }


}
