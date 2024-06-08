import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorAddEditComponent } from './author-add-edit.component';

describe('AuthorAddEditComponent', () => {
  let component: AuthorAddEditComponent;
  let fixture: ComponentFixture<AuthorAddEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AuthorAddEditComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AuthorAddEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
