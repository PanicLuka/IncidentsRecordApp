import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, Subject } from 'rxjs';
import { Category } from 'src/app/models/category.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http: HttpClient) {

  }
  public getCategories(): Observable<Category[]> {
    return (this.http.get<Category[]>(`${environment.gateway}/gateway/get/categories`));

  }

  public getCategory(id: string): Observable<Category> {
    return this.http.get<Category>(`${environment.gateway}/gateway/get/categories/${id}`)
  }
}
