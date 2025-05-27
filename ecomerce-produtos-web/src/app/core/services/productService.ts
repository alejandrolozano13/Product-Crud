import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Product } from '../../domain/models/product.model';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class ProductService {
    private apiUrl = 'https://localhost:7244/product';

    constructor(private http: HttpClient) { }

    getAll(): Observable<Product[]> {
        return this.http.get<Product[]>(this.apiUrl);
    }

    add(product: Product): Observable<void> {
        return this.http.post<void>('https://localhost:7244/Product', product);
    }

    update(produto: Product): Observable<void> {
        return this.http.put<void>('https://localhost:7244/Product', produto);
    }

    delete(id: string): Observable<void> {
        return this.http.delete<void>(`https://localhost:7244/Product/${id}`);
    }
}