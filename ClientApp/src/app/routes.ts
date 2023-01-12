import { Routes } from "@angular/router";
import { NotFoundComponent } from "./components/authorization/not-found/not-found.component";
import { RegisterComponent } from "./components/authorization/register/register.component";
import { LoginComponent } from "./components/authorization/login/login.component";
import { HomeComponent } from "./components/pages/home/home.component";
import { AuthGuard } from "./shared/guards/auth-guard";
import { AuthorListComponent } from "./components/pages/authors/author-list/author-list.component";
import { BookListComponent } from "./components/pages/books/book-list/book-list.component";
import { UserListComponent } from "./components/pages/users/user-list/user-list.component";
import { UserDetailsComponent } from "./components/pages/users/user-details/user-details.component";
import { UserEditComponent } from "./components/pages/users/user-edit/user-edit.component";
import { BookDetailsComponent } from "./components/pages/books/book-details/book-details.component";
import { AuthorDetailsComponent } from "./components/pages/authors/author-details/author-details.component";
import { UserDetailsResolver } from "./shared/resolvers/user-details.resolver";
import { UserEditResolver } from "./shared/resolvers/user-edit.resolver";
import { BookDetailsResolver } from "./shared/resolvers/book-details.resolver";
import { AuthorDetailsResolver } from "./shared/resolvers/author-details.resolver";
import { MenuLayoutComponent } from "./shared/layout/menu-layout/menu-layout.component";
import { DashboardLayoutComponent } from "./shared/layout/dashboard-layout/dashboard-layout.component";
import { BookDashboardComponent } from "./components/templates/dashboard/book-dashboard/book-dashboard.component";
import { AuthorDashboardComponent } from "./components/templates/dashboard/author-dashboard/author-dashboard.component";
import { UserDashboardComponent } from "./components/templates/dashboard/user-dashboard/user-dashboard.component";
import { HomeDashboardComponent } from "./components/templates/dashboard/home-dashboard.component";
import { RoleGuard } from "./shared/guards/role-guard";

export const appRoutes: Routes = [
  {
    path: '', component: MenuLayoutComponent, data: { title: 'Empty Routes' }, children:
      [
        { path: '', component: HomeComponent },
        { path: 'home', component: HomeComponent },
        { path: 'register', component: RegisterComponent },
        { path: 'login', component: LoginComponent },
        { path: 'authors', component: AuthorListComponent, canActivate: [AuthGuard] },
        { path: 'authors/:id', component: AuthorDetailsComponent, resolve: { author: AuthorDetailsResolver }, canActivate: [AuthGuard] },
        { path: 'books', component: BookListComponent, canActivate: [AuthGuard] },
        { path: 'books/:id', component: BookDetailsComponent, resolve: { book: BookDetailsResolver }, canActivate: [AuthGuard] },
        { path: 'user/edit', component: UserEditComponent, resolve: { user: UserEditResolver }, canActivate: [AuthGuard] },
        { path: 'users', component: UserListComponent, canActivate: [AuthGuard] },
        { path: 'users/:id', component: UserDetailsComponent, resolve: { user: UserDetailsResolver }, canActivate: [AuthGuard] },
      ]
  },
  {
    path: 'dashboard', component: DashboardLayoutComponent, canActivate: [AuthGuard, RoleGuard], data: { title: 'Dashboard' }, children:
      [
        { path: '', component: HomeDashboardComponent, canActivate: [AuthGuard, RoleGuard] },
        { path: 'home', component: HomeDashboardComponent, canActivate: [AuthGuard, RoleGuard] },
        { path: 'books', component: BookDashboardComponent, canActivate: [AuthGuard, RoleGuard] },
        { path: 'authors', component: AuthorDashboardComponent, canActivate: [AuthGuard, RoleGuard] },
        { path: 'users', component: UserDashboardComponent, canActivate: [AuthGuard, RoleGuard] },
        { path: '**', component: NotFoundComponent }
      ]
  },
  { path: '**', component: NotFoundComponent }
];
