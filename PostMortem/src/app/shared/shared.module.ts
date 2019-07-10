import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PmHeaderComponent } from './pm-header/pm-header.component';
import { MaterialModule } from '../material/material.module';

@NgModule({
  imports: [
    CommonModule,
    MaterialModule
  ],
  declarations: [PmHeaderComponent],
  exports: [PmHeaderComponent]
})
export class SharedModule { }
