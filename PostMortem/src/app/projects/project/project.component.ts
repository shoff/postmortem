import { Component, OnInit } from '@angular/core';
import { ProjectService, ProjectDto } from 'src/app/core';
import { Observable } from 'rxjs';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.scss']
})
export class ProjectComponent implements OnInit {
  projectId: string;
  project$: Observable<ProjectDto>;
  constructor(
    private activatedRoute: ActivatedRoute,
    private projectService: ProjectService) {
    this.activatedRoute.queryParams.subscribe(params => {
      this.projectId = params.projectId;
    });
  }

  ngOnInit() {
    let proj;
    this.projectService.getProject(this.projectId).subscribe(p => proj = p);
    console.log(proj);
  }

}
