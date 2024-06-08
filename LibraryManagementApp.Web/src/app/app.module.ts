import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms'; // Import FormsModule
import { HttpClientModule } from '@angular/common/http'; // Import HttpClientModule
import { ReactiveFormsModule } from '@angular/forms'; // Import ReactiveFormsModule
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { AuthorsComponent } from './components/authors/authors.component';
import { AuthorAddEditComponent } from './components/authors/add-edit/author-add-edit/author-add-edit.component';
import { AuthorService } from './services/author.service';
import { BookListComponent } from './components/books/book-list/book-list.component';
import { BookAddEditComponent } from './components/books/book-add-edit/book-add-edit.component';

const routes: Routes = [
    { path: 'authors', component: AuthorsComponent },
    { path: 'authors/add', component: AuthorAddEditComponent },
    { path: 'authors/edit/:id', component: AuthorAddEditComponent },
    { path: 'books', component: BookListComponent },
    { path: 'books/add', component: BookAddEditComponent },
    { path: 'books/edit/:id', component: BookAddEditComponent },    

    { path: '', redirectTo: '/authors', pathMatch: 'full' },
    { path: '**', redirectTo: '/authors' }
  ];

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    AuthorsComponent,
    AuthorAddEditComponent,
    BookListComponent,
    BookAddEditComponent
  ],
  imports: [
    BrowserModule,
    FormsModule, // Add FormsModule to imports array
    ReactiveFormsModule,
    HttpClientModule, // Add HttpClientModule here
    RouterModule.forRoot(routes) // Add RouterModule with empty routes array
  ],
  exports: [RouterModule],
  providers: [AuthorService],
  bootstrap: [AppComponent]
})
export class AppModule { }
