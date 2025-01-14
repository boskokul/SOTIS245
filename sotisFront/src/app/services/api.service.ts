import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { TestForm } from '../model/testForm';
import { Test, TestSampleDTO } from '../model/test';
import { Login } from '../model/login';
import { User } from '../model/user';
import { Router } from '@angular/router';
import { TokenStorage } from '../auth/jwt/token.service';
import { AuthenticationResponse } from '../model/authentication-response';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private apiUrl = 'http://localhost:5265/api';

  constructor(
    private http: HttpClient,
    private tokenStorage: TokenStorage,
    private router: Router
  ) {}
  user$ = new BehaviorSubject<User>({
    username: '',
    id: 0,
    role: '',
  });

  getData(): Observable<any> {
    return this.http.get(`${this.apiUrl}/python/acmPart`);
  }

  createTest(testDto: TestForm): Observable<any> {
    return this.http.post(`${this.apiUrl}/python/createTest`, testDto);
  }

  postTestSample(testDto: TestSampleDTO): Observable<any> {
    return this.http.post(`${this.apiUrl}/tests/createTestSample`, testDto);
  }
  getTestsByField(field: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/tests/` + field);
  }

  getTestsByFieldTestSamples(field: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/tests/TestSamples/` + field);
  }

  login(credentials: Login): Observable<any> {
    return this.http
      .post<AuthenticationResponse>(this.apiUrl + '/users/login', credentials)
      .pipe(
        tap((authenticationResponse) => {
          this.tokenStorage.saveAccessToken(authenticationResponse.accessToken);
          this.setUser();
        })
      );
  }

  logout(): void {
    this.tokenStorage.clear();
    this.router.navigate(['']);
    this.user$.next({ username: '', id: 0, role: '' });
  }

  checkIfUserExists(): void {
    const accessToken = this.tokenStorage.getAccessToken();
    if (accessToken == null) {
      return;
    }
    this.setUser();
  }

  private setUser(): void {
    const jwtHelperService = new JwtHelperService();
    const accessToken = this.tokenStorage.getAccessToken() || '';
    const user: User = {
      id: +jwtHelperService.decodeToken(accessToken).id,
      username: jwtHelperService.decodeToken(accessToken).username,
      role: jwtHelperService.decodeToken(accessToken)[
        'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
      ],
    };
    console.log(user);
    this.user$.next(user);
  }
  getUser(): User {
    const jwtHelperService = new JwtHelperService();
    const accessToken = this.tokenStorage.getAccessToken() || '';
    if(accessToken != ''){
      const user: User = {
        id: +jwtHelperService.decodeToken(accessToken).id,
        username: jwtHelperService.decodeToken(accessToken).username,
        role: jwtHelperService.decodeToken(accessToken)[
          'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
        ],
      };
      return user;
    }else{
      const user: User = {
        id: 0,
        username: '',
        role: '',
      };
      return user;
    }
    
  }
}
