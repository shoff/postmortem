import { Component, OnInit } from '@angular/core';
import { ProjectService, ProjectDto } from 'src/app/core';
import { Observable } from 'rxjs';
import { DataSource } from '@angular/cdk/collections';

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.scss']
})
export class ProjectListComponent implements OnInit {

  dataSource: ProjectDataSource = new ProjectDataSource(this.projectService);
  displayedColumns = ['projectName', 'startDate', 'endDate'];
  
  constructor(private projectService: ProjectService) { }

  ngOnInit() {
  }

}

export class ProjectDataSource extends DataSource<any> {
  constructor(private projectService: ProjectService) {
    super();
  }
  connect(): Observable<ProjectDto[]> {
    return this.projectService.getAllProjects();
  }
  disconnect() {}
}
