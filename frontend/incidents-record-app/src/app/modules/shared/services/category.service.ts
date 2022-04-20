import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable} from "rxjs";
import { environment } from "src/environments/environment";
import { Category } from "../../../models/category.model";

@Injectable()
export class CategoryService {
    constructor(private http: HttpClient) { }

    public getCategories(pageSize: number, pageNumber: number): Observable<Category[]> {
        return this.http.get<Category[]>(`${environment.gateway}/gateway/get/categories`, {
            params: {
                pageSize: pageSize,
                pageNumber: pageNumber
            }
        })
    }

    public getCategoriesCount(): Observable<number> {
        return this.http.get<number>(`${environment.gateway}/gateway/get/categories/count`)
    }

    public getCategoryById(categoryId: string): Observable<Category> {
        return this.http.get<Category>(`${environment.gateway}/gateway/get/${categoryId}`)
    }

    public createCategory(category: Category): Observable<Category> {
        return this.http.post<Category>(`${environment.gateway}/gateway/post/categories`, category)
    }

    public updateCategory(category: Category, categoryId: string): Observable<Category> {
        return this.http.put<Category>(`${environment.gateway}/gateway/put/categories/${categoryId}`, category)
    }

    public deleteCategory(categoryId: string): Observable<Category> {
        return this.http.delete<Category>(`${environment.gateway}/gateway/delete/categories/${categoryId}`)
    }
}