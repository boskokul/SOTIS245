import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TestForm } from '../model/testForm';
import { Test } from '../model/test';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private apiUrl = 'http://localhost:5265/api';

  constructor(private http: HttpClient) {}

  getData(): Observable<any> {
    return this.http.get(`${this.apiUrl}/python/acmPart`);
  }

  createTest(testDto: TestForm): Observable<any> {
    return this.http.post(`${this.apiUrl}/python/createTest`,testDto);
  }

  // Add other methods as needed
}
