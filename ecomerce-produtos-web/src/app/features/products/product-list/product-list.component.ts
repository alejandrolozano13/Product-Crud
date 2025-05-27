import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; // <-- importante
import { Product } from '../../../domain/models/product.model';
import { ProductService } from '../../../core/services/productService';
import { DepartmentService, Department } from '../../../core/services/departmentService';
import * as bootstrap from 'bootstrap';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];
  departamentos: Department[] = [];

  isEditando: boolean = false;

  erro: string = '';

  novoProduto: Product = {
    code: '',
    description: '',
    departmentCode: '',
    price: 0,
    active: true,
    removed: false
  };

  constructor(
    private productService: ProductService,
    private departmentService: DepartmentService
  ) { }

  ngOnInit(): void {
    this.productService.getAll().subscribe({
      next: (res) => this.products = res,
      error: (err) => {
        this.erro = 'Erro ao carregar produtos';
        console.error('Erro ao carregar produtos:', err);
      },
    });
  }

  carregarDepartamentos(): void {
    this.departamentos = []; // limpa para evitar dados antigos
    this.departmentService.getAll().subscribe({
      next: (res) => {
        this.departamentos = res;
      },
      error: (err) => {
        console.error('Erro ao carregar departamentos:', err);
        this.departamentos = [];
      }
    });
  }


  formatarPreco(event: any) {
    let valor = event.target.value;

    valor = valor.replace(/[^\d,.-]/g, '');
    valor = valor.replace(',', '.');

    const valorNumerico = parseFloat(valor);

    if (!isNaN(valorNumerico)) {
      this.novoProduto.price = valorNumerico;
      event.target.value = valorNumerico.toLocaleString('pt-BR', {
        style: 'currency',
        currency: 'BRL',
      });
    } else {
      this.novoProduto.price = 0;
      event.target.value = '';
    }
  }

  adicionarProduto(): void {
    this.novoProduto = {
      code: '',
      description: '',
      departmentCode: '',
      price: 0,
      active: true,
      removed: false
    };
    this.isEditando = false;

    this.carregarDepartamentos();

    const modalEl = document.getElementById('modalAdicionarProduto')!;
    const modal = new bootstrap.Modal(modalEl);
    modal.show();
  }

  salvarProduto(): void {
    if (this.isEditando) {
      // Atualizar produto existente
      this.productService.update(this.novoProduto).subscribe({
        next: () => {
          console.log('Produto atualizado com sucesso!');
          this.fecharModal();
          this.productService.getAll().subscribe(res => this.products = res);
        },
        error: (err) => {
          console.error('Erro ao atualizar produto:', err);
        }
      });
    } else {
      // Criar novo produto
      this.productService.add(this.novoProduto).subscribe({
        next: () => {
          console.log('Produto salvo com sucesso!');
          this.fecharModal();
          this.productService.getAll().subscribe(res => this.products = res);
        },
        error: (err) => {
          console.error('Erro ao salvar produto:', err);
        }
      });
    }
  }

  private fecharModal(): void {
    const modalEl = document.getElementById('modalAdicionarProduto')!;
    const modal = bootstrap.Modal.getInstance(modalEl);
    modal?.hide();
  }

  editarProduto(produto: Product): void {
    this.isEditando = true;
    this.novoProduto = { ...produto };

    this.carregarDepartamentos();

    const modalEl = document.getElementById('modalAdicionarProduto')!;
    const modal = new bootstrap.Modal(modalEl);
    modal.show();
  }

  removerProduto(produto: Product): void {
    const confirmacao = confirm(`Deseja realmente excluir o produto "${produto.description}"?`);
    if (!confirmacao) return;

    this.productService.delete(produto.id!).subscribe({
      next: () => {
        console.log('Produto removido com sucesso!');
        this.productService.getAll().subscribe(res => this.products = res);
      },
      error: (err) => {
        console.error('Erro ao remover produto:', err);
      }
    });
  }
}