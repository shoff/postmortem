import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProjectListComponent } from './projects/project-list/project-list.component';
import { ProjectComponent } from './projects/project/project.component';

const routes: Routes = [
    { path: '', component: ProjectListComponent },
    { path: 'project', component: ProjectComponent }
//   { path: '', component: PM, children: [
//   { path: '', loadChildren: '../dashboard/dashboard.module#DashboardModule'},
//   { path: 'organizations', loadChildren: '../organizations/organizations.module#OrganizationsModule' },
//   { path: 'events', loadChildren: '../events/events.module#EventsModule' },
//   { path: 'hosts', loadChildren: '../hosts/hosts.module#HostsModule' },
//   { path: 'taikai', loadChildren: '../taikai/taikai.module#TaikaiModule' },
// ]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
