import { NgModule } from '@angular/core';
import { MatButtonModule, MatCheckboxModule, MatCardModule } from '@angular/material';
import { MatInputModule } from '@angular/material/input';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatListModule } from '@angular/material/list';

@NgModule({
  imports: [ReactiveFormsModule],
  exports: [
    MatInputModule, MatButtonModule, MatCheckboxModule, MatListModule, MatCardModule
  ]
})
export class SharedMaterialModule { }
