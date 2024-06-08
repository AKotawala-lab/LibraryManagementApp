import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookAddEditComponent } from './book-add-edit.component';

describe('BookAddEditComponent', () => {
  let component: BookAddEditComponent;
  let fixture: ComponentFixture<BookAddEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BookAddEditComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BookAddEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
