// src/app/core/services/department.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Department {
  code: string;
  description: string;
}

@Injectable({
  providedIn: 'root',
})
export class DepartmentService {
  private apiUrl = 'https://localhost:7244/Department';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Department[]> {
    return this.http.get<Department[]>(this.apiUrl);
  }
}