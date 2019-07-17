import { Injectable, OnDestroy, Host } from '@angular/core';
import { ProjectDto, CommentDto, QuestionDto } from '..';
import { HttpClient, HttpClientModule, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';



@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  constructor(private httpClient: HttpClient) { }

  getAllProjects(): Observable<ProjectDto[]> {
    return this.httpClient.get<ProjectDto[]>('http://localhost:54091/api/projects/');
  }

  getProject(projectId): Observable<ProjectDto> {
    return this.httpClient.get<ProjectDto>('http://localhost:54091/api/projects?projectId=' + projectId);
  }
}
