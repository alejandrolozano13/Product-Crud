import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Product } from '../../domain/models/product.model';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class ProductService {
    private apiUrl = 'http://localhost:5000/product';

    constructor(private http: HttpClient) { }

    getAll(): Observable<Product[]> {
        return this.http.get<Product[]>(this.apiUrl);
    }

    add(product: Product): Observable<void> {
        return this.http.post<void>('http://localhost:5000/Product', product);
    }

    update(produto: Product): Observable<void> {
        return this.http.put<void>('http://localhost:5000/Product', produto);
    }

    delete(id: string): Observable<void> {
        return this.http.delete<void>(`http://localhost:5000/Product/${id}`);
    }
}