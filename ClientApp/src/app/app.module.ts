import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/authorization/login/login.component';
import { RegisterComponent } from './components/authorization/register/register.component';
import { HomeDashboardComponent } from './components/templates/dashboard/home-dashboard.component';
import { FooterComponent } from './components/templates/footer/footer.component';
import { HeaderComponent } from './components/templates/header/header.component';
import { RouterModule } from '@angular/router';
import { appRoutes } from './routes';
import { NgxLoadingModule, ngxLoadingAnimationTypes } from 'ngx-loading';
import { NotFoundComponent } from './components/authorization/not-found/not-found.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PieChartComponent } from './components/charts/pie-chart/pie-chart.component';
import { BarChartComponent } from './components/charts/bar-chart/bar-chart.component';
import { DoughnutChartComponent } from './components/charts/doughnut-chart/doughnut-chart.component';
import { LineChartComponent } from './components/charts/line-chart/line-chart.component';
import { HomeComponent } from './components/pages/home/home.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TimeagoModule } from 'ngx-timeago';
import { JwtModule } from '@auth0/angular-jwt';
import { AuthGuard } from './shared/guards/auth-guard';
import { ErrorInterceptor } from './shared/interceptors/error.interceptor';
import { UserListComponent } from './components/pages/users/user-list/user-list.component';
import { UserEditComponent } from './components/pages/users/user-edit/user-edit.component';
import { UserDetailsComponent } from './components/pages/users/user-details/user-details.component';
import { AuthorDetailsComponent } from './components/pages/authors/author-details/author-details.component';
import { BookDetailsComponent } from './components/pages/books/book-details/book-details.component';
import { BookListComponent } from './components/pages/books/book-list/book-list.component';
import { AuthorListComponent } from './components/pages/authors/author-list/author-list.component';
import { UserEditResolver } from './shared/resolvers/user-edit.resolver';
import { UserDetailsResolver } from './shared/resolvers/user-details.resolver';
import { JwPaginationModule } from 'jw-angular-pagination';
import { AlifeRatingStarModule } from 'alife-rating-star';
import { BookDetailsResolver } from './shared/resolvers/book-details.resolver';
import { AuthorDetailsResolver } from './shared/resolvers/author-details.resolver';
import { SidebarComponent } from './components/templates/sidebar/sidebar.component';
import { MenuLayoutComponent } from './shared/layout/menu-layout/menu-layout.component';
import { DashboardLayoutComponent } from './shared/layout/dashboard-layout/dashboard-layout.component';
import { BookDashboardComponent } from './components/templates/dashboard/book-dashboard/book-dashboard.component';
import { UserDashboardComponent } from './components/templates/dashboard/user-dashboard/user-dashboard.component';
import { AuthorDashboardComponent } from './components/templates/dashboard/author-dashboard/author-dashboard.component';
import { DeleteUserComponent } from './components/templates/dashboard/user-dashboard/delete-user/delete-user.component';
import { AddUserComponent } from './components/templates/dashboard/user-dashboard/add-user/add-user.component';
import { UpdateUserComponent } from './components/templates/dashboard/user-dashboard/update-user/update-user.component';
import { DeleteBookComponent } from './components/templates/dashboard/book-dashboard/delete-book/delete-book.component';
import { UpdateBookComponent } from './components/templates/dashboard/book-dashboard/update-book/update-book.component';
import { AddBookComponent } from './components/templates/dashboard/book-dashboard/add-book/add-book.component';
import { AddAuthorComponent } from './components/templates/dashboard/author-dashboard/add-author/add-author.component';
import { DeleteAuthorComponent } from './components/templates/dashboard/author-dashboard/delete-author/delete-author.component';
import { UpdateAuthorComponent } from './components/templates/dashboard/author-dashboard/update-author/update-author.component';
import { AddPublisherComponent } from './components/templates/dashboard/author-dashboard/add-publisher/add-publisher.component';
import { ValidatorComponent } from './components/templates/validator/validator.component';
import { NumberDirective } from './shared/directives/only-number.directive';
import { TextDirective } from './shared/directives/only-text.directive';
import { TextOrNumberDirective } from './shared/directives/only-text-or-number.directive';
import './shared/extensions/form-group';
import { UppercaseturkishPipe } from './shared/pipes/uppercase-turkish.pipe';
import { GlobalVariables } from './shared/constants/global-variables';
import { DatePipe } from '@angular/common';

export function tokenGetter() {
  return localStorage.getItem("token");
}

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    HomeDashboardComponent,
    FooterComponent,
    HeaderComponent,
    NotFoundComponent,
    PieChartComponent,
    BarChartComponent,
    DoughnutChartComponent,
    LineChartComponent,
    HomeComponent,
    UserListComponent,
    UserEditComponent,
    UserDetailsComponent,
    AuthorDetailsComponent,
    BookDetailsComponent,
    BookListComponent,
    AuthorListComponent,
    SidebarComponent,
    MenuLayoutComponent,
    DashboardLayoutComponent,
    BookDashboardComponent,
    UserDashboardComponent,
    AuthorDashboardComponent,
    DeleteUserComponent,
    AddUserComponent,
    UpdateUserComponent,
    DeleteBookComponent,
    UpdateBookComponent,
    AddBookComponent,
    AddAuthorComponent,
    DeleteAuthorComponent,
    UpdateAuthorComponent,
    AddPublisherComponent,
    ValidatorComponent,
    NumberDirective,
    TextDirective,
    TextOrNumberDirective,
    UppercaseturkishPipe,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NgbModule,
    FormsModule,
    JwPaginationModule,
    AlifeRatingStarModule,
    TimeagoModule.forRoot(),
    NgxLoadingModule.forRoot({
      animationType: ngxLoadingAnimationTypes.circleSwish,
      backdropBackgroundColour: 'rgba(0,0,0,0.1)',
      backdropBorderRadius: '4px',
      primaryColour: '#ffffff',
      secondaryColour: '#ffffff',
      tertiaryColour: '#ffffff'
    }), JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: ["localhost:5057"],
        blacklistedRoutes: ["localhost:5057/api/Auth"],
      },
    }),
    RouterModule.forRoot(appRoutes),
    ReactiveFormsModule,
  ],
  providers: [
    AuthGuard, {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true,
    },
    UserEditResolver,
    UserDetailsResolver,
    BookDetailsResolver,
    AuthorDetailsResolver,
    GlobalVariables,
    DatePipe
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
