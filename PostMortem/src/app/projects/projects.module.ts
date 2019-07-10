import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MaterialModule } from '../material/material.module';
import { ProjectsRoutingModule } from './projects-routing.module';
import { ProjectListComponent } from './project-list/project-list.component';
import { ProjectComponent } from './project/project.component';
import { QuestionsComponent } from './questions/questions.component';
import { CommentsComponent } from './comments/comments.component';

@NgModule({
  declarations: [ProjectListComponent, ProjectComponent, QuestionsComponent, CommentsComponent],
  imports: [
    CommonModule,
    MaterialModule,
    ProjectsRoutingModule
  ], 
  exports: [ ProjectListComponent, ProjectComponent, QuestionsComponent, CommentsComponent ]
})
export class ProjectsModule { }
